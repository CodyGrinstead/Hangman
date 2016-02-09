//Cody Grinstead
//1/16/16
//Hangman program
#include <iostream>
#include <iomanip>
#include <string>
#include <fstream>
#include <ctime>
using namespace std;
const int maxwords=20;
void readineasy(string words[])
{
	ifstream inf("easy.txt");
	int i = 0;
	while (!inf.eof())
	{
		inf >> words[i];
		i++;
	}
}
void readinnormal(string words[])
{
	ifstream inf("normal.txt");
	int i = 0;
	while (!inf.eof())
	{
		inf >> words[i];
		i++;
	}
}
void readinhard(string words[])
{
	ifstream inf("hard.txt");
	int i = 0;
	while (!inf.eof())
	{
		inf >> words[i];
		i++;
	}
}
void printhangman(int life)
{
	if (life == 0)
	{
		cout<< "     _________" << endl
			<< "     |       |" << endl
			<< "   __O__     |" << endl
			<< "     |       |" << endl
			<< "    _|_      |" << endl
			<< "   |   |     |" << endl
			<< "             |" << endl
			<< "_____________|" << endl << endl;
	}
	else if (life == 1)
	{
		cout << "     _________" << endl
			<< "     |       |" << endl
			<< "   __O__     |" << endl
			<< "     |       |" << endl
			<< "    _|       |" << endl
			<< "   |         |" << endl
			<< "             |" << endl
			<< "_____________|" << endl << endl;
	}
	else if (life == 2)
	{
		cout << "     _________" << endl
			<< "     |       |" << endl
			<< "   __O__     |" << endl
			<< "     |       |" << endl
			<< "     |       |" << endl
			<< "             |" << endl
			<< "             |" << endl
			<< "_____________|" << endl << endl;
	}
	else if (life == 3)
	{
		cout << "     _________" << endl
			<< "     |       |" << endl
			<< "     O__     |" << endl
			<< "     |       |" << endl
			<< "     |       |" << endl
			<< "             |" << endl
			<< "             |" << endl
			<< "_____________|" << endl << endl;
	}
	else if (life == 4)
	{
		cout << "     _________" << endl
			<< "     |       |" << endl
			<< "     O       |" << endl
			<< "     |       |" << endl
			<< "     |       |" << endl
			<< "             |" << endl
			<< "             |" << endl
			<< "_____________|" << endl << endl;
	}
	else if (life == 5)
	{
		cout << "     _________" << endl
			<< "     |       |" << endl
			<< "     O       |" << endl
			<< "             |" << endl
			<< "             |" << endl
			<< "             |" << endl
			<< "             |" << endl
			<< "_____________|" << endl << endl;
	}
	else if (life == 6)
	{
		cout << "     _________" << endl
			<< "     |       |" << endl
			<< "             |" << endl
			<< "             |" << endl
			<< "             |" << endl
			<< "             |" << endl
			<< "             |" << endl
			<< "_____________|" << endl << endl;
	}
	else if (life == 7)
	{
		cout << "              " << endl
			<< "              " << endl
			<< "              " << endl
			<< "              " << endl
			<< "              " << endl
			<< "              " << endl
			<< "              " << endl
			<< "_____________ " << endl << endl;
	}
	else if (life == 8)
	{
		cout << "              " << endl
			<< "              " << endl
			<< "              " << endl
			<< "              " << endl
			<< "              " << endl
			<< "              " << endl
			<< "              " << endl
			<< "              " << endl << endl;
	}
}
char lowerToUpper(char c)
{
	if (c == 'a')return 'A';
	else if (c == 'b')return 'B';
	else if (c == 'c')return 'C';
	else if (c == 'd')return 'D';
	else if (c == 'e')return 'E';
	else if (c == 'f')return 'F';
	else if (c == 'g')return 'G';
	else if (c == 'h')return 'H';
	else if (c == 'i')return 'I';
	else if (c == 'j')return 'J';
	else if (c == 'k')return 'K';
	else if (c == 'l')return 'L';
	else if (c == 'm')return 'M';
	else if (c == 'n')return 'N';
	else if (c == 'o')return 'O';
	else if (c == 'p')return 'P';
	else if (c == 'q')return 'Q';
	else if (c == 'r')return 'R';
	else if (c == 's')return 'S';
	else if (c == 't')return 'T';
	else if (c == 'u')return 'U';
	else if (c == 'v')return 'V';
	else if (c == 'w')return 'W';
	else if (c == 'x')return 'X';
	else if (c == 'y')return 'Y';
	else if (c == 'z')return 'Z';
	else return c;
}
int game(string easy[], string normal[], string hard[])
{
	cout << "Welcome to hangman" << endl;
	system("pause");
	cout << "Choose a difficulty: " << endl << "Type 'E' for Easy, Type 'N' for Normal, Type 'H' for Hard" << endl;
	char difficulty;
	string wordList[maxwords];
	int i;
	char playAgain;
	int random=21;
	while (random >= 20)
	{
		srand((unsigned)time(0));//makes random really random
		random = rand() % (20 - 0 + 1);
	}
	bool isDifficulty = false;
	while (isDifficulty == false)
	{
		cin >> difficulty;
		if (difficulty == 'E' || 'e')
		{
			isDifficulty = true;
			for (i = 0; i < maxwords; i++)wordList[i] = easy[i];
		}
		else if (difficulty == 'N' || 'n')
		{
			isDifficulty = true;
			for (i = 0; i < maxwords; i++)wordList[i] = normal[i];
		}
		else if (difficulty == 'H' || 'h')
		{
			isDifficulty = true;
			for (i = 0; i < maxwords; i++)wordList[i] = hard[i];
		}
		else
		{
			cout << "Please reselect difficulty by typing the letter 'E','N', or 'H'" << endl;
			cin >> difficulty;
		}
	}
	string currentWord = wordList[random];
	int length = currentWord.length();
	char guessedLetter[26];
	char guess;
	int life = 8;
	int trynumber = 0;
	printhangman(life);
	string guessedLetters = "";
	string currentWordLine = "";
	for (i = 1; i <= length; i++) {
		cout << "_";
		currentWordLine += "_";
	}
	cout << endl;
	bool isNewLetter = false;
	int currentGuessedCorrectly = 0;
	while ((life > 0) && (currentGuessedCorrectly<length))
	{
		bool isLetter = false;
		cout << "Choose a letter:" << endl;
		cin >> guess;
		guess = lowerToUpper(guess);
		for (i = 0; i < guessedLetters.length(); i++)if (guessedLetters[i] == guess)isNewLetter = true;
		if (isNewLetter == true) {
			cout << "You have guessed this letter" << endl;
			isNewLetter = false;
		}
		else 
		{
			guessedLetters += guess;
			for (i = 0; i < length; i++)
			{
				if (currentWord[i] == guess)
				{
					currentWordLine[i] = guess;
					isLetter = true;
					currentGuessedCorrectly++;
				}
			}
			if (isLetter == false)life--;
			cout << currentWordLine << endl;
			printhangman(life);
			cout << "Picked Letters:" << endl << guessedLetters << endl;
			cout << endl << endl;
		}
	}
	if (life > 0)
	{
		cout << "You guessed the word correctly." << endl << "You had " << life << " guesses left"
			<< endl << endl << "Would you like to play again?(Y/N)" << endl;
		cin >> playAgain;
	}
	else
	{
		cout << "Sorry, the correct word was " << currentWord << endl << "Would you like to play again?(Y/N)" << endl;
		cin >> playAgain;
	}
	if (playAgain == 'Y') return 1;
	else return 0;
}

int main()
{
	string easy[maxwords], normal[maxwords], hard[maxwords];
	readineasy(easy);
	readinnormal(normal);
	readinhard(hard);
	int playAgain;
	playAgain=game(easy, normal, hard);
	while (playAgain == 1)playAgain = game(easy, normal, hard);
	system("pause");
	return 0;
}