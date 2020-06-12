using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace WillowParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<QuestionAndResponse> Survey = new List<QuestionAndResponse>();//GLOBAL SURVEYYYYYYYY
        

        public StringBuilder RemoveWS(ref StringBuilder line)
        {
            
            while(line[0]== ' ')
            {
                line.Remove(0, 1);
            }
            return line;
        }




        public void createSurvey(string FilePath)
        {
            //fill the List Survey with all teh questions
            string line;
            string test = Console.ReadLine();
            label5.Text = test;


            StringBuilder CurrentLine = new StringBuilder();
            StringBuilder CheckNextLine = new StringBuilder();
            int LastLineResponse = 0;
            string Question = "";
            StringBuilder VarFile = new StringBuilder();
            List<String> EntireFile = new List<string>();

            using (var Reader = new System.IO.StreamReader(FilePath))
            {
                while ((line = Reader.ReadLine()) != null)
                {
                    EntireFile.Add(line);
                }
            }
            //VarFile contains the entire var file 
            for(int i=0; i<EntireFile.Count()-1;i++)
            {
                CurrentLine.Clear();
                CurrentLine.Append(EntireFile[i]);
                //Console.WriteLine(CurrentLine[7].ToString());
                if(CurrentLine[7]=='1')
                {
                    CheckNextLine.Clear();
                    CheckNextLine.Append(EntireFile[i + 1]);
                    if(CheckNextLine[7]=='2')//This is the question
                    {
                        CheckNextLine.Remove(0, 10);
                        CheckNextLine = RemoveWS(ref CheckNextLine);

                        //Console.WriteLine(CheckNextLine.ToString());
                        QuestionAndResponse newQuestion = new QuestionAndResponse();
                        newQuestion.Question = CheckNextLine.ToString();
                        newQuestion.QuestionType = int.Parse(CurrentLine[15].ToString());
                        StringBuilder NextLineQCheck = new StringBuilder();
                        NextLineQCheck.Clear();
                        NextLineQCheck.Append(EntireFile[i + 2]);
                        if(NextLineQCheck[7]=='2')
                        {
                            NextLineQCheck.Remove(0, 10);
                            NextLineQCheck = RemoveWS(ref NextLineQCheck);
                            newQuestion.Question += NextLineQCheck.ToString();
                            i++;
                        }
                        //Get Number of spaces this question takes up
                        if(CurrentLine[15]=='1' || CurrentLine[15]=='4') 
                        {
                            string CollumnLength = "";
                            int ji =0;
                            while(CurrentLine[24+ji]!= ' ')
                            {
                                CollumnLength += CurrentLine[24 + ji].ToString();
                                ji++;
                            }
                            newQuestion.NumberOfSpaces = int.Parse(CollumnLength.ToString());
                            newQuestion.EachQuestionSpace = newQuestion.NumberOfSpaces;
                        }
                        else
                        {
                            int num1, num2, jm =0;
                            string tempNum="";
                            CurrentLine = RemoveWS(ref CurrentLine);
                            CurrentLine.Remove(0, 8);
                            CurrentLine = RemoveWS(ref CurrentLine);
                            CurrentLine.Remove(0, 1);
                            CurrentLine = RemoveWS(ref CurrentLine);
                            num1 = int.Parse(CurrentLine[0].ToString());
                            CurrentLine.Remove(0, 1);
                            CurrentLine = RemoveWS(ref CurrentLine);
                            while(CurrentLine[0+jm]!= ' ')
                            {
                                tempNum += CurrentLine[0 + jm].ToString();
                                jm++;
                            }
                            num2 = int.Parse(tempNum);
                            newQuestion.NumberOfSpaces = num1 * num2;
                            newQuestion.NumberOfSpaces = num1;
                        }
                        //Get Respones ready
                        int j = 2;
                        List<Responses> Answers = new List<Responses>();
                        while(EntireFile[i+j][7] == '3' && i+j<EntireFile.Count()-1)
                        {
                            StringBuilder ansLine = new StringBuilder();
                            ansLine.Append(EntireFile[i + j]);
                            ansLine.Remove(0, 10);
                            ansLine = RemoveWS(ref ansLine);
                            string cStart = "";
                            while (ansLine[0]!=' ')
                            {
                                cStart += ansLine[0].ToString();
                                ansLine.Remove(0, 1);
                            }
                            newQuestion.CollumnStart =int.Parse(cStart);
                            //newQuestion.CollumnStart = -1;
                            ansLine.Remove(0, 1);
                            Responses thisResponse = new Responses();
                            string tempRNumber = "";
                            while(ansLine[0]!= ' ')
                            {
                                tempRNumber += ansLine[0].ToString();
                                ansLine.Remove(0, 1);
                            }
                            thisResponse.ResponeNumber = tempRNumber;
                            ansLine.Remove(0, 1);
                            ansLine = RemoveWS(ref ansLine);
                            thisResponse.AnswerText = ansLine.ToString();
                            Answers.Add(thisResponse);
                            j++;
                        }
                        i += j-1;
                        newQuestion.Answers = Answers;
                        Survey.Add(newQuestion);

                    }
                    else//Current line is hte question
                    {
                        QuestionAndResponse newQuestion = new QuestionAndResponse();
                        newQuestion.QuestionType = int.Parse(CurrentLine[15].ToString());

                        //Get Number of spaces this question takes up
                        if (CurrentLine[15] == '1' || CurrentLine[15] == '4')
                        {
                            string CollumnLength = "";
                            int ja = 0;
                            while (CurrentLine[24 + ja] != ' ')
                            {
                                CollumnLength += CurrentLine[24 + ja].ToString();
                                ja++;
                            }
                            newQuestion.NumberOfSpaces = int.Parse(CollumnLength.ToString());
                            newQuestion.EachQuestionSpace = newQuestion.NumberOfSpaces;
                        }
                        else
                        {
                            int num1, num2, jx = 0;
                            string tempNum = "";
                            num1 = int.Parse(CurrentLine[18].ToString());
                            while (CurrentLine[29 + jx] != ' ')
                            {
                                tempNum += CurrentLine[29 + jx].ToString();
                                jx++;
                            }
                            num2 = int.Parse(tempNum);
                            newQuestion.NumberOfSpaces = num1 * num2;
                            newQuestion.EachQuestionSpace = num1;
                        }
                        //Set up question
                        CurrentLine.Remove(0, 10);
                        CurrentLine = RemoveWS(ref CurrentLine);
                        CurrentLine.Remove(0, 16);
                        CurrentLine = RemoveWS(ref CurrentLine);
                        newQuestion.Question = CurrentLine.ToString();
                        Console.WriteLine(CurrentLine.ToString());
                        //Get Respones ready
                        int j = 1;
                        List<Responses> Answers = new List<Responses>();
                        while (EntireFile[i + j][7] == '3')
                        {
                            StringBuilder ansLine = new StringBuilder();
                            ansLine.Append(EntireFile[i + j]);
                            ansLine.Remove(0, 10);
                            ansLine = RemoveWS(ref ansLine);
                            string cStart = "";
                            while (ansLine[0] != ' ')
                            {
                                cStart += ansLine[0].ToString();
                                ansLine.Remove(0, 1);
                            }
                            newQuestion.CollumnStart = int.Parse(cStart);
                            ansLine.Remove(0, 1);
                            Responses thisResponse = new Responses();
                            string tempRNumber = "";
                            while (ansLine[0] != ' ')
                            {
                                tempRNumber += ansLine[0].ToString();
                                ansLine.Remove(0, 1);
                            }
                            thisResponse.ResponeNumber = tempRNumber;
                            ansLine.Remove(0, 1);
                            ansLine = RemoveWS(ref ansLine);
                            thisResponse.AnswerText = ansLine.ToString();
                            Answers.Add(thisResponse);
                            j++;
                        }
                        i += j-1;
                        newQuestion.Answers = Answers;
                        Survey.Add(newQuestion);
                    }
                    
                }
                else if(CurrentLine[7]=='2')
                {
                    QuestionAndResponse anq = new QuestionAndResponse();
                    anq.Question = "Test";
                    anq.CollumnStart = -1;
                    anq.NumberOfSpaces = -1;
                    Survey.Add(anq);
                }
                else
                {
                    QuestionAndResponse anq = new QuestionAndResponse();
                    anq.Question = "Test";
                    anq.CollumnStart = -1;
                    anq.NumberOfSpaces = -1;
                    Survey.Add(anq);
                }
            }


        }


        public void ParseDatFile(string DatFile, int length)
        {
            List<string> AllDatSurveys = new List<string>();
            string ENTIREDATFILE = "";
            string temp;
            StringBuilder csv = new StringBuilder();
            using (var Reader = new System.IO.StreamReader(DatFile))
            {
                ENTIREDATFILE = Reader.ReadLine();
            }
            StringBuilder EDF = new StringBuilder();
            EDF.Append(ENTIREDATFILE);
            Console.WriteLine(ENTIREDATFILE.Length % length);
            for (int i = 0; i < ENTIREDATFILE.Length - 1; i+=length)
            {
                temp = ENTIREDATFILE.Substring(i, length);
                Console.WriteLine(temp.Length);
                AllDatSurveys.Add(temp);
                temp = "";
            }
            List<QuestionAndResponse> sorted = Survey.OrderBy(s => s.CollumnStart).ToList();
            for(int i = 0; i<AllDatSurveys.Count();i++)
            {
                string newSurvey = "";
                int currentSpace = 0;
                for (int j = 0; j < 7; j++) { newSurvey += AllDatSurveys[i][j]; }
                newSurvey += ",";
                int questionNum = 1;
                for (int index= 0; index<sorted.Count();index++)
                {
                    //loop through each question
                    newSurvey += questionNum.ToString() + ","+sorted[index].Question+",";
                    questionNum++;
                    int numberOfSpaces = sorted[index].NumberOfSpaces;


                    string currentAns = "";
                    if(sorted[index].NumberOfSpaces==1)
                    {
                        if (sorted[index].QuestionType == 4)
                        {
                            currentAns += AllDatSurveys[i].ToString() + ",";
                        }
                        else if (sorted[index].QuestionType==2)
                        {
                            for(int j = 0;j<sorted[index].NumberOfSpaces;j++)
                            {
                                string CurrentCollum = AllDatSurveys[i][sorted[index].CollumnStart + j].ToString();
                                if (AllDatSurveys[i][sorted[index].CollumnStart + j] =='0')
                                {
                                    break;
                                }
                                else
                                {
                                    if (sorted[index].Answers.Where(a => a.ResponeNumber == CurrentCollum).FirstOrDefault() != null)
                                    {
                                        newSurvey += sorted[index].Answers.Where(a => a.ResponeNumber == CurrentCollum).FirstOrDefault().AnswerText;
                                        newSurvey += ",";
                                    }
                                }
                            }
                        }
                        else
                        {
                            currentAns += AllDatSurveys[i][sorted[index].CollumnStart-1];

                            string tempResponse;

                            //if (AllDatSurveys[i][sorted[index].CollumnStart] != '0')
                            //{
                                if (sorted[index].Answers.Where(a => a.ResponeNumber == currentAns).FirstOrDefault() == null)
                                {
                                    newSurvey += sorted[index + 1].Answers.Where(a => a.ResponeNumber == currentAns).FirstOrDefault().AnswerText;
                                }
                                else
                                {
                                    newSurvey += sorted[index].Answers.Where(a => a.ResponeNumber == currentAns).FirstOrDefault().AnswerText;
                                }
                            //}

                            newSurvey += ",";
                        }
                    }
                    else
                    {
                        continue;
                    }




                    /*Get the total amount of space that is taken up by this question
                     * loop that goes totalspaces startign at column space -1 incrimenting by space taken
                     * 
                     * 
                     * Get the number of columns each response takes up
                     * use that number to get the string of said response text
                     * comapre to the list of resposes in this question
                     * output that
                     * loop till all have been filled
                     * if multi line question with zeros skip
                     */




                    //if (sorted[index].EachQuestionSpace != 1)
                    //{
                    //    for (int k = sorted[index].CollumnStart - 1; k < sorted[index].CollumnStart + sorted[index].NumberOfSpaces - 1; k += sorted[index].EachQuestionSpace)
                    //    {
                    //        string thisResponse = "";
                    //        for (int m = sorted[index].CollumnStart - 1; m < sorted[index + 1].CollumnStart; m++)
                    //        {
                    //            thisResponse += AllDatSurveys[i][m];
                    //        }
                    //        Console.WriteLine(thisResponse);

                    //        //newSurvey += string.Format("{0},");
                    //    }
                    //}
                    //else
                    //{
                    //    string thisResponse = AllDatSurveys[i][sorted[index].CollumnStart].ToString();
                    //    Console.WriteLine(" " + thisResponse);
                    //}
                    //go from collumstart of item to 



                }
                //Console.WriteLine(newSurvey);
                csv.AppendLine(newSurvey);
                

            }
            using (var output = new System.IO.StreamWriter(@"C:\Users\yodac\Documents\TestOutput\Test.csv"))
            {
                output.Write(csv.ToString());
            }
        }



        private void parcer_Click(object sender, EventArgs e)
        {
            //Save the length of each survay to a variable
            var sLength = Length.Text;
            int surveyLength = int.Parse(sLength);
            Console.WriteLine("test");
            //Create the survey parser
            createSurvey(VarFileLocation.Text);
            //int abcd = 0;
            //foreach(var item in Survey)
            //{
            //    item.Print();
            //    abcd++;
            //}

            List<QuestionAndResponse> sorted = Survey.OrderBy(s => s.CollumnStart).ToList();

            foreach(var item in sorted)
            {
                if (item.NumberOfSpaces == 0)
                    item.Print();
            }

            Console.WriteLine("============End Class===========");
            Console.WriteLine(Survey.Count().ToString());


           ParseDatFile(DatFileLocation.Text.ToString(), int.Parse(Length.Text));


            label4.Text="done";
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void DatFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select file to be upload.";
            openFileDialog1.Filter = "Select Valid Document(*.DAT)|*.DAT";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        DatFileLocation.Text = path;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload document.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void VarFileLocation_Click(object sender, EventArgs e)
        {

        }

        private void VarFile_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = "C://Desktop";
            openFileDialog2.Title = "Select file to be upload.";
            openFileDialog2.Filter = "Select Valid Document(*.var)|*.var";
            openFileDialog2.FilterIndex = 1;
            try
            {
                if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog2.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog2.FileName);
                        VarFileLocation.Text = path;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload document.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void DatFileLocation_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Length_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
