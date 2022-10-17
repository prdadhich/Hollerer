import replicate
import json
import random

output_url =''
#wordsToSend =""
wordsArray = []
wordsTrans = {}



def predict(wordsToSend):
    model = replicate.models.get("stability-ai/stable-diffusion")
    output_url = model.predict(prompt=wordsToSend)[0]

    with open('D:\Replicate.json', 'r+') as file:
        data = json.load(file)
        
        file.seek(0)
        data['Url'] = output_url
        
        file.write(json.dumps(data))
    print(wordsToSend)
    


def main():

    with open('D:\Replicate.json', 'r') as file:
        
        data = json.load(file)
        wordsInGerman = data['SelectedWords']
        wordsTrans = data['Words']
        tempList = wordsInGerman.split(",")
        tempList = random.choices(tempList,k=3)
        wordsToSen = ''
        for word in tempList:
            if(word != ''):
                print(wordsTrans[word])
                wordsToSen = wordsToSen + wordsTrans[word] + ',' 

            
        
    
    predict(wordsToSen)


        
            
   

if __name__ == '__main__':
    main()



