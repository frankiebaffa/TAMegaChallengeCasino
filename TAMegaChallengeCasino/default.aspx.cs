using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TAMegaChallengeCasino
{
    public partial class _default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                pageInit();

                //  randomize images
                imageRandom();
                
                //  get playerMoney from ViewState
                double playerMoney = (double)ViewState["money"];

                // print playerMoney
                printStatus(playerMoney);
            }
        }

        protected void leverButton_Click(object sender, EventArgs e)
        {
            pullLever();
        }


        //  METHODS

        //  On page reload
        private void pageInit()
        {
            double playerMoney = 100;
            double betAmount = 0;
            double winAmount = 0;
            ViewState.Add("money", playerMoney);
            ViewState.Add("bet", betAmount);
            ViewState.Add("win", winAmount);
        }

        //  randomize three images
        private void imageRandom()
        {
            //  array of slot images
            string[] imageNames = { "Bar.png", "Bell.png","Cherry.png","Clover.png",
                "Diamond.png","HorseShoe.png","Lemon.png","Orange.png","Plum.png",
                "Seven.png","Strawberry.png","Watermelon.png"};

            // array of integers
            int[] randomInt = { 0, 0, 0 };

            //  randomize array of integers
            Random random = new Random();
            for (int i = 0; i < randomInt.Length; i++) randomInt[i] += random.Next(0, 11);

            imageAssign(imageNames, randomInt);
        }

        //  assign img names
        private void imageAssign(string[] imageNames, int[] randomInt)
        {
            oneImg.ImageUrl = String.Format("img/{0}", imageNames[randomInt[0]]);
            twoImg.ImageUrl = String.Format("img/{0}", imageNames[randomInt[1]]);
            threeImg.ImageUrl = String.Format("img/{0}", imageNames[randomInt[2]]);
        }

        //  lever pull
        private void pullLever()
        {
            if (!double.TryParse(betBox.Text.Trim(), out double betAmount)) betAmount = 0;

            ViewState["bet"] = betAmount;

            double playerMoney = (double)ViewState["money"];
                
            playerMoney -= betAmount;
            
            // randomize images
            imageRandom();

            //  check image results
            leverLogic(betAmount);

            resultLogic(playerMoney, betAmount);
        }

        private void resultLogic(double playerMoney, double betAmount)
        {
            double winAmount = (double)ViewState["win"];
            playerMoney += winAmount;
            ViewState["money"] = playerMoney;
            printer(playerMoney, betAmount);
        }

        private void leverLogic(double betAmount)
        {
            if (checkBar(betAmount))
                return;
            else if (checkJackpot(betAmount))
                return;
            else
            {
                doCherry(betAmount);
            }
        }

        //  check photos for Bar - return true or false
        private bool checkBar(double betAmount)
        {
            if (oneImg.ImageUrl == "img/Bar.png"
                || twoImg.ImageUrl == "img/Bar.png"
                || threeImg.ImageUrl == "img/Bar.png")
            {
                doBar(betAmount);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void doBar(double betAmount)
        {
            double winAmount = betAmount * 0;
            ViewState["win"] = winAmount;
            return;
        }

        //  check photos for Jackpot - return true or false
        private bool checkJackpot(double betAmount)
        {
            if (oneImg.ImageUrl == "img/Seven.png"
                && twoImg.ImageUrl == "img/Seven.png"
                && threeImg.ImageUrl == "img/Seven.png")
            {
                doJackpot(betAmount);
                return true;
            }
            else
                return false;
        }

        private void doJackpot(double betAmount)
        {
            double winAmount = betAmount * 100;
            ViewState["win"] = winAmount;
            return;
        }

        private void doCherry(double betAmount)
        {
            double winAmount = betAmount * checkCherry();
            ViewState["win"] = winAmount;
            return;
        }

        //  check photos for Cherries - return the multiplication factor
        private int checkCherry()
        {
            int cherryCount = 0;
            int cherryMultiplier = 0;

            if (oneImg.ImageUrl == "img/Cherry.png") cherryCount++;
            if (twoImg.ImageUrl == "img/Cherry.png") cherryCount++;
            if (threeImg.ImageUrl == "img/Cherry.png") cherryCount++;

            cherryMultiplier = cherryFactor(cherryCount);
            return cherryMultiplier;
        }

        private int cherryFactor(int cherryCount)
        {
            int cherryMultiplier = 0;
            if (cherryCount == 1) cherryMultiplier = 2;
            else if (cherryCount == 2) cherryMultiplier = 3;
            else if (cherryCount == 3) cherryMultiplier = 4;

            return cherryMultiplier;
        }

        private void printer(double playerMoney, double betAmount)
        {
            printResult(betAmount);
            printStatus(playerMoney);
        }

        //  take playerMoney from ViewState and display as currency
        //  in statusLabel
        private void printStatus(double playerMoney)
        {
            statusLabel.Text = String.Format(
                    "Player money: {0:C}",
                    playerMoney);
        }

        //  Print winnings, if any
        private void printResult(double betAmount)
        {
            double winAmount = (double)ViewState["win"];
            if (winAmount > 0)
            {
                resultLabel.Text = String.Format(
                    "You bet {0:C}<br />" +
                    "You won {1:C}",
                    betAmount,
                    winAmount);
            }
            else
            {
                resultLabel.Text = String.Format(
                    "You lost {0:C}",
                    betAmount);
            }
        }
    }
}