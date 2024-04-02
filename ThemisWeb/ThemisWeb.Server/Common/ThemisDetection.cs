using Newtonsoft.Json;

namespace ThemisWeb.Server.Common
{
    public class ThemisDetection
    {

        public IEnumerable<Input> ReadInputs(IFormFile file)
        {
            var reader = new StreamReader(file.OpenReadStream());
            string fileContent = reader.ReadToEnd();
            reader.Dispose();

            return JsonConvert.DeserializeObject<IEnumerable<Input>>(fileContent);
        }


        public TextData GetInputsTextData(IFormFile inputsFile)
        {
            TextData toReturn;
            toReturn.rawContent = "";

            IEnumerable<Input> inputs = ReadInputs(inputsFile);

            foreach (Input input in inputs)
            {
                switch (input._ActionType)
                {
                    case ActionType.ADDCHAR: toReturn.rawContent.Insert((int)input._Selection.SelectionStart, input.ActionContent); break;
                    case ActionType.DELETESELECTION: toReturn.rawContent.Remove((int)input._Selection.SelectionStart, (int)input._Selection.SelectionStart - (int)input._Selection.SelectionEnd); break;
                    case ActionType.PASTE: toReturn.rawContent.Insert((int)input._Selection.SelectionStart, input.ActionContent); break;
                }
            }
            toReturn.charCount = toReturn.rawContent.Length;
            toReturn.wordCount = toReturn.rawContent.Split(" ").Length;
            return toReturn;
        }



    }
}
