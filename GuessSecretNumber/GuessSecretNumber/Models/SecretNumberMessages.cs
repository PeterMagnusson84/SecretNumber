using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuessSecretNumber.Models;

namespace GuessSecretNumber.Models
{
    public class SecretNumberMessages
    {
        //Hämtar ifrån Model->SecretNumber.cs
        public SecretNumber SecretNumber { get; set; }
        public Outcome Outcome { get; set; }

        //Meddelande efter hur många gissningar användaren gör.
        public string MessageCount
        {
            get 
            {
                if (Outcome != Outcome.OldGuess)
                {
                    if (SecretNumber.Count == 1)
                    {
                        return "Första gissningen";
                    }
                    if (SecretNumber.Count == 2)
                    {
                        return "Andra gissningen";
                    }
                    if (SecretNumber.Count == 3)
                    {
                        return "Tredje gissningen";
                    }
                    if (SecretNumber.Count == 4)
                    {
                        return "Fjärde gissningen";
                    }
                    if (SecretNumber.Count == 5)
                    {
                        return "Femte gissningen";
                    }
                    if (SecretNumber.Count == 6)
                    {
                        return "Sjätte gissningen";
                    }
                    if (SecretNumber.Count == 7)
                    {
                        return "Sjunde gissningen";
                    }
                    return null;
                }
                return null;
            }
        }

        //Meddelande efter hur användaren gissar.
        public string TheMessage(Outcome outcome)
        {
            string theStatus = null;

            if (outcome == Outcome.High)
            {
                theStatus = "Du gissade för högt";
            }
            if (outcome == Outcome.Low)
            {
                theStatus = "Du gissade för lågt";
            }
            if (outcome == Outcome.OldGuess)
            {
                theStatus = "Detta numer har du redan gissat på";
            }
            if (outcome == Outcome.NoMoreGuesses)
            {
                theStatus = "Nu har du gissat färdigt";
            }
            if (outcome == Outcome.Right)
            {
                theStatus = "Du har gissat på det rätta numret";
            }
            return theStatus;
        }

    }
}