using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GuessSecretNumber.Models
{
    public enum Outcome
    {
        Indefinite,
        Low,
        High,
        Right,
        NoMoreGuesses,
        OldGuess
    }
    //lagrar information om genomförda gissningar.
    public struct GuessedNumber
    {
        public int? Number; //innehåller en gissnings värde.
        public Outcome Outcome; //innehåller utfallet av en gissning.
    }

    public class SecretNumber
    {
        private List<GuessedNumber> _guessedNumbers;//En lista med alla gissade numer.
        private int? _number; //Innehåller en gissnigs värde.
        public const int MaxNumberOfGuesses = 7; //Antalet gissningar användaren har.


        //Kollar om man kan fortsätta gissa.
        public bool CanMakeGuessCheck
        {
            get
            {
                if (Count != 0)
                {
                    if (Count < MaxNumberOfGuesses && LastGuessedNumber.Value.Outcome != Outcome.Right)
                    {
                        return true;
                    }

                    else
                    {
                        return false;
                    }

                }
                return true;
            }
        }

        //Kollar hur många gissningar som har gjorts
        public int Count
        {
            get { return this._guessedNumbers.Count(); }
        }

        [Required(ErrorMessage = "Du måste skriva en siffra")]
        [Range(1.0, 100.0, ErrorMessage = "Ett tal mellan 1-100 måste anges")]
        [DisplayName("Skriv ett nummer:")]
        public int? Guess
        {
            get;
            set;
        }

        public IList<GuessedNumber> GuessedNumber
        {
            get { return _guessedNumbers.AsReadOnly(); }
        }

        public GuessedNumber? LastGuessedNumber
        {
            get
            {
                if (GuessedNumber.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return _guessedNumbers.Last();
                }
            }
        }

        //Bestämmer vad som ska hända när man har gissat 7 gånger.
        public int? Number
        {
            get
            {
                if (this.CanMakeGuessCheck)
                {
                    return null;
                }
                else
                {
                    return this._number;
                }
            }
            //Om det inte går att gissa mer visas numret.
            private set { this._number = value; }

        }

        //Metoder
        public void Initialize()
        {
            //Tömer listan på gissningar som gjorts.
            this._guessedNumbers.Clear();
            //Skapar ett nytt hemligt numer.
            Random randomNumber = new Random();
            this.Number = randomNumber.Next(1, 101);
            //Återställer gissningarna till noll.
            this.Guess = 0;
        }

        //Metod som används för att undersöka användarens gissningar och vad som ska hända.
        public Outcome MakeGuess()
        {
            Outcome outcome = Outcome.Indefinite;

            if (this._guessedNumbers.Count == MaxNumberOfGuesses)
            {
                outcome = Outcome.NoMoreGuesses;
            }
            else
            {
                if (this.Guess > 100 || this.Guess < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }
                // Kollar igenom alla tidigare nummer
                if (this._guessedNumbers.Any(n => n.Number == this.Guess))
                {
                    outcome = Outcome.OldGuess;
                    return outcome; // Gör att om de är en gammal gissning, så retunerars direkt, och inget sparas i arrayen för alla gissnigar.
                }
                else
                {
                    if (this.Guess < this._number)
                    {
                        outcome = Outcome.Low;
                    }
                    else if (this.Guess > this._number)
                    {
                        outcome = Outcome.High;
                    }
                    else
                    {
                        outcome = Outcome.Right;
                    }
                    // Skapar ny GuessedNumber
                    GuessedNumber newNumber;
                    // Sätter värdena
                    newNumber.Number = this.Guess;
                    newNumber.Outcome = outcome;
                    // Stoppar de i Listan
                    this._guessedNumbers.Add(newNumber);
                }
            }
            return outcome;
        }

        public SecretNumber()
        {
            _guessedNumbers = new List<GuessedNumber>();
            Initialize();
        }


    }
}