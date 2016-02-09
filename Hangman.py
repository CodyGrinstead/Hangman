#Cody Grinstead
#Python Hangman
#Last updated 2/5/16
import random

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
		

#get word for a list x
#ask user for guess x
#check to see if letter & hasnt been guessed x
#if eligable letter check with work x
#if correct guess switch blank spaces with letter x
#if wrong update life count
#print current guessed word with current hangman life
#reapeat till life is 0 or word is guessed
#ask if user wants to play again

wordListEasy=['fox','cat','dog','mouse','ant','desk','chair','man','woman','phone']
wordListHard=['jazz','jinx','blizzard','quiz','house','buzzer','cozy','rose','daisy','dinner']

def game(difficulty):
	life=8
	guessedcorrectly=0
	guessedletters=" "
	notletters='1234567890 `~!@#$%^&*()_+-=}{][;:/?.>,<'
	currentword=difficulty[random.randint(0,9)]
	wordlen=len(currentword)
	currentwordline=''
	wordlinelist=[]#making a list of stings that can be edited
	for n in range(0,wordlen):
		wordlinelist+='_'
		currentwordline+=wordlinelist[n]
	while(life>0 and guessedcorrectly<wordlen):		
		isletterword= False 
		currentHangman(life)
		print(guessedletters,"\n",currentwordline)
		currentwordline=''#reset the wordline
		guessl=input("Enter a letter to guess\n")
		guess=guessl.lower()
		#making sure guessed letter is a letter and can be used as such
		for n in range(0,len(guessedletters)): 
			if(guess==guessedletters[n]):
				print("You have already guessed that letter")
				guessl=input("Enter a letter you have not guessed\n")
				guess=guessl.lower()
			if(len(guess)>1):
				print("You can only guess one letter at a time")
				guessl=input("Enter a letter you have not guessed\n")
				guess=guessl.lower()
		guessedletters+=guess #so user can see current guessed letters
		for n in range(0,len(notletters)):
			if(guess==notletters[n]):
				print("This is not a letter")
				guessl=input("Enter a letter you have not guessed\n")
				guess=guessl.lower()		
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
		print("You correctly guessed the word ", currentword,"!")
		playagain=input("Would you like to play again? (y/n)  ")
		if(playagain=='y'):
			return True	
		else:
			return False
	else:
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
	play=game(wordListEasy)

	
	
