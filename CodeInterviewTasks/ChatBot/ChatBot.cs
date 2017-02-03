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

        private static readonly List<string> ResponseList = new List<string>(MaxResp);

        public static bool Quite = false;

        private const int MaxInput = 1;
        private const int MaxResp = 4;

        public static void Hello()
        {
            HandleEvent("SIGNON**");
            SelectResponse();
            Console.WriteLine(_response);
        }

        public static void GetInput()
        {
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
            var rndIndex = new Random().Next(0, ResponseList.Count - 1);
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

            str = InsertSpace(str);

            _input = str;

            if (!IsSameEvent())
            {
                FindMatch();
            }

            _input = _inputBackup;
        }

        private static string InsertSpace(string str)
        {
            StringBuilder temp = new StringBuilder(str);
            temp.Insert(0, ' ');
            temp.Insert(temp.Length, ' ');
            return temp.ToString();
        }

        // TODO 
        // 1. Add several keywords
        // 2.Add transpose for some sentences, like You to me, we to us etc.., 
        // 3.Add word position concept, i.e. Who is? etc., 
        // 4.Add context aware
        // 5. Add ability to update db with new words
        // 6. Add a beter repetition handling algorithm
        // 7. Do a better DB location
        private static void FindMatch()
        {
            ResponseList.Clear();
            List<int> indexList = new List<int>(MaxResp);

            string bestKeyWord = "";
            for (int i = 0; i < DataSource.KnowledgeBase.Length; ++i)
            {
                string keyWord = DataSource.KnowledgeBase[i][0];
                keyWord = InsertSpace(keyWord);

                if (_input.IndexOf(keyWord, StringComparison.Ordinal) != -1)
                {
                    if (keyWord.Length > bestKeyWord.Length)
                    {
                        bestKeyWord = keyWord;
                        indexList.Clear();
                        indexList.Add(i);
                    }
                    else if (keyWord.Length == bestKeyWord.Length)
                    {
                        indexList.Add(i);
                    }
                }
            }

            if (!indexList.Any()) return;

            var respIndex = indexList[0];
            int responseSize = DataSource.KnowledgeBase[respIndex].Length - MaxInput;
            for (int j = MaxInput; j <= responseSize; ++j)
            {
                ResponseList.Add(DataSource.KnowledgeBase[respIndex][j]);
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
            _input = InsertSpace(_input);
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
