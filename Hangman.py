#Cody Grinstead
#Python Hangman
#Last updated 2/9/16
import random
import os


def currentHangman(life):
	if(life==8):
		print("        ",'\n',"        ",'\n',"        ",'\n',"        ",'\n',"        ",'\n',"        ",'\n',"        ",'\n',"        ")
	elif(life==7):
		print("        ",'\n',"        ",'\n',"        ",'\n',"        ",'\n',"        ",'\n',"        ",'\n',"        ",'\n',"_______|")
	elif(life==6):
		print("   _____",'\n',"  |    |",'\n',"       |",'\n',"       |",'\n',"       |",'\n',"       |",'\n',"       |",'\n',"_______|")
	elif(life==5):
		print("   _____",'\n',"  |    |",'\n',"  O    |",'\n',"       |",'\n',"       |",'\n',"       |",'\n',"       |",'\n',"_______|")
	elif(life==4):
		print("   _____",'\n',"  |    |",'\n',"  O    |",'\n',"  |    |",'\n',"  |    |",'\n',"       |",'\n',"       |",'\n',"_______|")
	elif(life==3):
		print("   _____",'\n',"  |    |",'\n',"  O__  |",'\n',"  |    |",'\n',"  |    |",'\n',"       |",'\n',"       |",'\n',"_______|")
	elif(life==2):
		print("   _____",'\n',"  |    |",'\n',"__O__  |",'\n',"  |    |",'\n',"  |    |",'\n',"       |",'\n',"       |",'\n',"_______|")
	elif(life==1):
		print("   _____",'\n',"  |    |",'\n',"__O__  |",'\n',"  |    |",'\n'," _|    |",'\n',"|      |",'\n',"       |",'\n',"_______|")
	else:
		print("   _____",'\n',"  |    |",'\n',"__O__  |",'\n',"  |    |",'\n'," _|_   |",'\n',"|   |  |",'\n',"       |",'\n',"_______|")

#difficulty lists
wordListEasy=['fox','cat','dog','mouse','ant','desk','chair','man','woman','phone','sun','ghost',
'flower','banana','book','light','snake','apple','socks','smile']
wordlistNormal=['horse','door','song','trip','backbone','bomb','round','treasure','garbage','park',
'pirate','pie','ski','state','whistle','place','baseball','coal','queen','dominoes','photograph']
wordListHard=['jazz','jinx','blizzard','quiz','house','buzzer','cozy','rose','daisy','dinner','important',
'baggage','password','newsletter','bookend','pharmacist','catalog','vegetarian','neighborhood','vitamin']

def isOneChar(guess):
	if(len(guess)>1):
		return False
	elif(len(guess)==0):
		return False
	else:
		return True

def isLetter(guess):
	isaLetter=True
	notletters=['1','2','3','4','5','6','7','8','9','0',' ','`','~','!',
	'@','#','$','%','^','&','*','(',')','_','+','-','=','}','{',']','[',
	';',':','/','?','.','>',',','<',',',"'",'"']
	if guess in notletters:
			isaLetter=False
	return isaLetter	
		
def isNewGuess(guess,guessedletters):#curret is current guess, already is string of already guessed
	isaNewGuess=True
	if(guess in guessedletters):
		isaNewGuess=False
	return isaNewGuess
	
def game(difficulty):
	life=8
	guessedcorrectly=0
	guessedletters=" "
	guessedlist=[]
	notletters='1234567890 `~!@#$%^&*()_+-=}{][;:/?.>,<'
	currentword=difficulty[random.randint(0,19)]
	wordlen=len(currentword)
	currentwordline=''
	wordlinelist=[]#making a list of stings that can be edited
	for n in range(0,wordlen):
		wordlinelist+='_'
		currentwordline+=wordlinelist[n]
	while(life>0 and guessedcorrectly<wordlen):	
		os.system('cls' if os.name == 'nt' else 'clear')#From stackover flow	
		isletterword= False 
		currentHangman(life)
		print(guessedletters,"\n",currentwordline)
		currentwordline=''#reset the wordline
		guessl=input("Enter a letter to guess\n")
		guess=guessl.lower()
		#making sure guessed letter is a letter and can be used as such
		while(isOneChar(guess)==False):
			print("You can only guess one letter at a time")
			guessl=input("Enter a letter you have not guessed\n")
			guess=guessl.lower()
		while(isLetter(guess)==False):
			print("You can only enter letters")
			guessl=input("Enter a letter you have not guessed\n")
			guess=guessl.lower()
		while(isNewGuess(guess,guessedlist)==False):
				print("You have already guessed that letter")
				guessl=input("Enter a letter you have not guessed\n")
				guess=guessl.lower()	
		guessedletters+=guess #so user can see current guessed letters
		guessedlist+=guess
		for n in range(0,wordlen):
			if(guess==currentword[n]):
				isletterword= True
				guessedcorrectly +=1
				wordlinelist[n]=guess
		if(isletterword==False):
			life-=1
		#making the wordlist into currentwordline for better display
		for n in range(0,wordlen):
			currentwordline+=wordlinelist[n]
	if(life>0):
		os.system('cls' if os.name == 'nt' else 'clear')#From stackover flow
		print("You correctly guessed the word ", currentword,"!")
		playagain=input("Would you like to play again? (y/n)  ")
		if(playagain=='y'):
			return True	
		else:
			print("Good bye")
			return False
	else:
		os.system('cls' if os.name == 'nt' else 'clear')#From Stackover flow
		currentHangman(0)
		print("Sorry the word was: ",currentword)
		playagain=input("Would you like to play again? (y/n)  ")
		if(playagain=='y'):
			return True	
		else:
			print("Good bye")
			return False
		
play=True
while(play==True):
	print("Welcome to Hangman")
	difficultyl=''
	difficultyl=input("Please select a difficulty.\n Easy, Normal, Hard: (e/n/h)")
	difficulty=difficultyl.lower()
	if(difficulty == 'e'):
		play=game(wordListEasy)
	elif(difficulty == 'n'):
		play=game(wordlistNormal)
	elif(difficulty == 'h'):
		play=game(wordListHard)
	else:
		print("Please select a difficulty, use 'e' 'n' or 'h' to select")

	
	
