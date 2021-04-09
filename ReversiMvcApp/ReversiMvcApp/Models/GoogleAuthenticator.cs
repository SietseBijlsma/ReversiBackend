using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Authenticator;
using ReversiMvcApp.Controllers;
using ReversiRestApi.Models;

namespace ReversiMvcApp.Models
{
    public class GoogleAuthenticator
    {
        private PlayerController _playerController { get; }

        public GoogleAuthenticator(PlayerController playerController)
        {
            _playerController = playerController;
        }
        public AuthenticatorInfo GenerateQrCode(Player player)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode("ReversiMVC", player.Email, player.Key, false, 2);

            return new AuthenticatorInfo()
            {
                ManualEntryKey = FormatKey(setupInfo.ManualEntryKey),
                QrCodeSetupImage = setupInfo.QrCodeSetupImageUrl
            };
        }

        public bool ValidateAuthenticatorCode(Player player, string code)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            return tfa.ValidateTwoFactorPIN(player.Key, code);
        }

        public async Task SetTfKey(Player player)
        {
            var key = RandomStringGenerator.RandomString(12, true);
            player.Key = key;
            await _playerController.SavePlayer(player);
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        public class AuthenticatorInfo
        {
            public string QrCodeSetupImage { get; set; }
            public string ManualEntryKey { get; set; }
        }
    }
}
