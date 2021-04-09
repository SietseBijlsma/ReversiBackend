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
        
        setTimeout(() => Game.Stats.init('gameChart'), 200);

        
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
        status,
    }

    const board = () => {
        _updateBoard();
    }

    const _init = (parentBoard, token, playerToken) => {
        stateMap.token = token;
        stateMap.$board = $(`#${parentBoard}`);
        stateMap.currentPlayerToken = playerToken;
        board();
    }

    const _placeFiche = async (row, col) => {
       if(stateMap.status !== "Finished") {
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
    }

    const _updateBoard = () => {
        stateMap.$board.html(Game.Templates.parseTemplate("game.board", {'board': stateMap.board, 'boardSize': configMap.boardSize}));
    }

    const _update = () => {
        Game.Data.get("game/" + stateMap.token).then(res => {
            stateMap.board = JSON.parse(res.board);
            stateMap.moving = res.moving;
            stateMap.status = res.status;
            _updateBoard();
            Game.Stats.updateStats(stateMap.board);
        }).catch(_ => {
           _update();
        })
    }

    return {
        init: _init,
        placeFiche: _placeFiche,
        update: _update,
    }

})()

Game.Templates = (() =>{
    const _getTemplate = (templateName) => {
        if(typeof spa_templates === 'undefined') {
            throw new Error('Templates have not been compiled yet. Run gulp build');
        }

        let templates = spa_templates.templates;

        templateName.split('.').forEach(element => {
            templates = templates[element];
        });

        return templates;
    }

    const _parseTemplate = (templateName, data) => {
        const template = _getTemplate(templateName);

        return template(data);
    }

    return {
        getTemplate: _getTemplate,
        parseTemplate: _parseTemplate,
    }
})()

Game.Stats = (() =>{
    let stateMap = {
        chart: undefined,
    }

    let configMap = {
        chart: {
            type: 'bar',
            data: {
                labels: ['None', 'White', 'Black'],
                datasets: [{
                    label: '# of Fiches',
                    data: [60, 2, 2],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    },
                    responsive: true
                }
            }
        },
        ctx: undefined,
    };
  
    const _updateChart = (total, black, white) => {
        if(stateMap.chart === undefined) {
            return;
        }
    
        let none = total - (black + white);
        stateMap.chart.data.datasets[0].data = [none, white, black];
        stateMap.chart.update();
    }
    
    const _updateStats = (board) => {
        let total = 0;
        let white = 0;
        let black = 0;

        board.forEach(x => {
            x.forEach(y => {
                total++;
                if(y === 1) {
                    white++;
                }
                else if(y === 2) {
                    black++;
                }
            });
        });
        setInterval(_updateChart(total, white, black), 100);
    }

    const _init = (chartId) => {
        var ctx = document.getElementById(chartId).getContext('2d');
        stateMap.chart = new Chart(ctx, configMap.chart);
        console.log(stateMap.chart);
    }
    
    return {
        init: _init,
        updateStats: _updateStats,
        updateChart: _updateChart,
    }
})()

Game.Status = (() => {
    
    let stateMap = {
        token: "",
        currentPlayerToken: "",
    }
    
    let configMap = {
    };  

    const _init = (token, playerToken) => {
        stateMap.token = token;
        stateMap.currentPlayerToken = playerToken;
    }

    const _updateStatus = (moving) => {
        if(stateMap.currentPlayerToken === moving) {
            //it is your turn (color)
        }
        else {
            //wait for your opponent to make a move (color)
        }
    }

    return {
        init: _init,
        updateStatus: _updateStatus,
    }
})()