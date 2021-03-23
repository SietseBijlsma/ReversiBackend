class FeedbackWidget{
    constructor(elementId) 
    {
         this._elementId = elementId;
         this.$element = $("#" + this._elementId);
         this.$element.empty();
         this.$element.addClass("feedbackWidget")
         this.$element.append(`
                <div class="feedbackWidget-check">
                    <i class="fas fa-check fa-3x"></i>
                </div>
                
                <div class="feedbackWidget-times">
                    <i class="fas fa-times"></i>
                </div>
                <p class="feedbackWidget-p">something went wrong</p>
                <div class="feedbackWidget-buttons">
                    <button class="button">Akoord</button>
                    <button class="button">Weigeren</button>
                </div>
         `);

         this.$element.find(".feedbackWidget-times").click(function() {
            $("#test").removeClass("active");
          });
    }
    get elementId() 
    { 
         return this._elementId;
    }
    show(type //, message
        )
    {
        // this.$element.html(`<pre>${message}</pre>`);
        // this.$element.css("display", "block");
        if(type == "success") 
        {
            this.$element.removeClass("alert-danger");
            this.$element.addClass("alert-success");
        }
       else {
            this.$element.removeClass("alert-succes");
            this.$element.addClass("alert-danger");
       }   
       this.$element.addClass("active");
    }
    hide() {
        this.$element.removeClass("active");
    }
    //log({type:"success", message:"test"})
    log(message) {
        let logs = JSON.parse(localStorage.getItem('feedback_widget')) ?? [];
        logs.push(message);

        if(logs.length > 10) 
            logs.splice(0, 1);

        localStorage.setItem('feedback_widget', JSON.stringify(logs));
    }
    removeLog(key) {
        localStorage.removeItem(key);
    }
    history() {
        let logs = JSON.parse(localStorage.getItem('feedback_widget')) ?? [];
        let string = logs.map(x => "type " + x.type + " - " + x.message).join("\n");
        this.show("success", string);
    }
}
const Game = ((url) =>{

    let configMap = {
        apiUrl: url
    }

    const privateInit = (env, token, playerToken) =>{
       
        Game.Data.init(env);
        Game.Board.init('board', token, playerToken);
        // setInterval(() => {
        //     _getCurrentGamestate()
        // }, 2000)
        update();
    }

    const update = () => {
        Game.Board.update();
    }

    const _getCurrentGamestate = () => {
        Game.Model.getGameState();
    }

    return {
        init: privateInit,
        update: update
    }

})('/api/url')

Game.Data = (() =>{

    let stateMap = { 
        'environment' : 'developement',
        'baseUrl' : 'https://localhost:5001/api/'
    };

    let configMap = {
        mock:
            {
            url: 'api/Spel/Beurt',
            data: 0
            },
        apiKey: 'e75be00d6b404d31f5b679a85c9d00c1'
    };

    const getMockData = (url) => {
        const mockData = configMap.mock;
        return new Promise((resolve, reject) => {
        resolve(mockData);
        });
    }

    const putMockData = (url, data) => {
        //TODO: make this function
    }

    const get = (url) => {
        return stateMap.environment === 'development' ? getMockData(url) : 
        $.ajax({
            type: 'GET',
            crossDomain: true,
            url: stateMap.baseUrl + url
        });
    }

    const put = (url, data) => {
        return stateMap.environment === 'development' ? putMockData(url) : 
        $.ajax({
            url: stateMap.baseUrl + url,
            type: 'PUT',
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true
        });
    }

    const _privateInit = (environment) => {
        stateMap.environment = environment
        if(stateMap.environment === undefined)
            throw new Error("Undefined enviroment")
    }

    return {
        init: _privateInit,
        get: get,
        put: put
    }
})()

Game.Model = (() =>{
    
    let configMap = {

    }

///api/Spel/Beurt/ make variable not static
    const _getGameState = function(token){
        Game.Data.get('game/turn' + token).then(x => {
            switch(x.data) {
                case 0: 
                    //console.log("geen specifieke waarde")
                    break;
                case 1: 
                    //console.log("wit is aan zet")
                    break;
                case 2: 
                    //console.log("zwart is aan zet")
                    break;
                default:
                    throw new Error('unknown gamestate')
            }
        })
    }

    return {
        getGameState: _getGameState
        }
})()

Game.Board = (() =>{
    
    let configMap = {
        boardSize: 8,
    }

    //0 = white 
    //1 = black
    let stateMap = {
        token: "",
        moving : 1, 
        board: [],
        currentPlayerToken: "",
    }

    const board = () => {
        $boardTemplate = $(`<div class="board" style="
        grid-template-columns: repeat(${configMap.boardSize}, 4rem); 
        grid-template-rows: repeat(${configMap.boardSize}, 4rem);"
        ></div>`);

        for (let row = 0; row < configMap.boardSize; row++) {
            for (let col = 0; col < configMap.boardSize; col++) {
                $cell = $(`<div class="cell" data-row="${row}" data-col="${col}"><div class="fiche"></div></div>`);

                $cell.on('click', function () {
                    _placeFiche($(this).attr('data-row'), $(this).attr('data-col'));
                });
                $boardTemplate.append($cell);
            }
        }

        stateMap.$board.append($boardTemplate);
    }

    const _init = (parentBoard, token, playerToken) => {
        stateMap.token = token;
        stateMap.$board = $(`#${parentBoard}`);
        stateMap.currentPlayerToken = playerToken;
        board();
    }

    const _placeFiche = async (row, col) => {
       let result = await Game.Data.put(`game/${stateMap.token}/move`, {
            row: parseInt(row),
            col: parseInt(col),
            player: stateMap.currentPlayerToken
       }).then(res => res).catch(e => false);

       if(result) {
           _update();
           _updateBoard();
           stateMap.$board.trigger('updateBoard');
       }
    }

    const _updateBoard = () => {
        for (let row = 0; row < configMap.boardSize; row++) {
            for (let col = 0; col < configMap.boardSize; col++) {
                let color = "";
                if(stateMap.board[row][col] == 1) {
                    color = "white";
                }
                else if(stateMap.board[row][col] == 2) {
                    color = "black";
                }

                if(color == "white") {
                    _getCoords(row, col).find('.fiche').removeClass("black");
                }
                else if (color == "black") {
                    _getCoords(row, col).find('.fiche').removeClass("white");
                }
                _getCoords(row, col).find('.fiche').addClass(color);
            }
        }
    }

    const _update = () => {
        Game.Data.get("game/" + stateMap.token).then(res => {
            stateMap.board = JSON.parse(res.board);
            stateMap.moving = res.moving;
            _updateBoard();
        }).catch(_ => {
            
            _update();
        })
    }

    const _getCoords = (row, col) => {
        if(row >= 0 && row < configMap.boardSize && col >= 0 && col < configMap.boardSize) {
            return stateMap.$board.find(`.cell[data-row="${row}"][data-col="${col}"]`);
        }
        return false;
    }

    return {
        init: _init,
        placeFiche: _placeFiche,
        update: _update,
    }
})()
