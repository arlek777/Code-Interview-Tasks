using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public class ChatBot
    {
        private static string _input = "";
        private static string _prevInput;

        private static string _response = "";
        private static string _prevResponse = "";

        private static string _event = "";
        private static string _prevEvent = "";

        private static string _inputBackup = "";

        private const string Delimeters = "?!.;";

        private static readonly List<string> ResponseList = new List<string>(4);

        public static bool Quite = false;

        private const int MaxInput = 1;

        public static void GetInput()
        {
            Console.WriteLine("Enter: ");

            _prevInput = _input;
            _input = Console.ReadLine();

            PreprocessInput();
        }

        public static void Respond()
        {
            _prevResponse = _response;
            _event = "BOT UNDERSTAND**";

            if (IsNullInput())
            {
                HandleEvent("NULL INPUT**");
            }
            else if (IsNullInputRepetition())
            {
                HandleEvent("NULL INPUT REPETITION**");
            }
            else if (IsUserRepeat())
            {
                HandleUserRepeat();
            }
            else
            {
                FindMatch();
            }

            if (_input.IndexOf("BYE") != -1)
            {
                Quite = true;
            }

            if (!ResponseList.Any())
            {
                HandleEvent("BOT DONT UNDERSTAND**");
            }

            if (ResponseList.Any())
            {
                SelectResponse();

                if (IsBotRepeat())
                {
                    HandleRepetition();
                }
                if (_response.Any())
                {
                    Console.WriteLine(_response);
                }
            }
        }

        public static void HandleRepetition()
        {
            if (ResponseList.Any())
            {
                ResponseList.RemoveAt(0);
            }
            else
            {
                _inputBackup = _input;
                _input = _event;

                FindMatch();
                _input = _inputBackup;
            }
            SelectResponse();
        }

        private static bool IsBotRepeat()
        {
            return (_prevResponse.Length > 0 &&
                 _response == _prevResponse);
        }

        private static void SelectResponse()
        {
            var rndIndex = new Random().Next(0, ResponseList.Count-1);
            _response = ResponseList[rndIndex];
        }

        private static void HandleUserRepeat()
        {
            if (IsSameInput())
            {
                HandleEvent("REPETITION T1**");
            }
            else if (IsSimilarInput())
            {
                HandleEvent("REPETITION T2**");
            }
        }

        private static bool IsSameInput()
        {
            return (_input.Length > 0 && _input == _prevInput);
        }

        private static bool IsSimilarInput()
        {
            return (_input.Length > 0 &&
                 (_input.IndexOf(_prevInput) != -1 ||
                 _prevInput.IndexOf(_input) != -1));
        }

        private static bool IsNullInputRepetition()
        {
            return (_input.Length == 0 && _prevInput.Length == 0);
        }

        private static bool IsUserRepeat()
        {
            return (_prevInput.Length > 0 &&
                 ((_input == _prevInput) ||
                 (_input.IndexOf(_prevInput) != -1) ||
                 (_prevInput.IndexOf(_input) != -1)));
        }

        private static void HandleEvent(string str)
        {
            _prevEvent = _event;
            _event = str;
            _inputBackup = _input;
            _input = str;

            if (!IsSameEvent())
            {
                FindMatch();
            }

            _input = _inputBackup;
        }

        private static void FindMatch()
        {
            ResponseList.Clear();
            for (int i = 0; i < DataSource.KnowledgeBase.Length; ++i)
            {
                // there has been some improvements made in
                // here in order to make the matching process
                // a littlebit more flexible
                if (_input.IndexOf(DataSource.KnowledgeBase[i][0], StringComparison.Ordinal) != -1)
                {
                    int responseSize = DataSource.KnowledgeBase[i].Length - MaxInput;
                    for (int j = MaxInput; j <= responseSize; ++j)
                    {
                        ResponseList.Add(DataSource.KnowledgeBase[i][j]);
                    }
                    break;
                }
            }
        }

        private static bool IsSameEvent()
        {
            return (_event.Length > 0 && _event == _prevEvent);
        }

        private static bool IsNullInput()
        {
            return (_input.Length == 0 && _prevInput.Length != 0);
        }

        private static void PreprocessInput()
        {
            _input = RemovePunctuation(_input);
            _input = _input.ToUpperInvariant();
        }

        private static string RemovePunctuation(string str)
        {
            StringBuilder temp = new StringBuilder(str.Length);
            char prevChar = '0';
            for (int i = 0; i < str.Length; ++i) {
                if ((str[i] == ' ' && prevChar == ' ') || !IsPunctuation(str[i]))
                {
                    temp.Append(str[i]);
                    prevChar = str[i];
                }
                else if (prevChar != ' ' && !IsPunctuation(str[i]))
                {
                    temp.Append(' ');
                }

            }
            return temp.ToString();
        }

        private static bool IsPunctuation(char ch)
        {
            return Delimeters.IndexOf(ch) != -1;
        }
    }
}
