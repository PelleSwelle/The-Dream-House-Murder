using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static List<Question> maryQuestions = new List<Question>()
    {
        new Question(1, 0, 0,
            "Who are you to Olivia", new Answer(
                "I'm her best friend. We do everything together, who could do this?"), hasBranches: true),
        new Question(2, 1, 0,
            "What were you doing that day?", new Answer(
                "Me and Olivia were just playing cards and talking about my relationship issues while playing cards")),
        new Question(2, 2, 0,
            "How many were you in the house that day?", new Answer(
                "It was me and Olivia in our room with James in the living room watching sports")),
        new Question(3, 0, 0,
            "Where were you at the time of the murder of Olivia?", new Answer(
                "I was drinking a local bar to calm down...")),
        new Question(4, 0, 0,
            "Why wasn't Olivia with you?", new Answer(
                "We had an argument, that made me upset.")),
        new Question(5, 0, 0,
            "What was the argument about?", new Answer(
                "I heard her speaking to another guys on the phone. Which made me angry because, she has a boyfriend"), hasBranches: true),
        new Question(6, 1, 0,
            "Who was on the phone?", new Answer(
                "Her secret lover, I belive he's an Real-Estate Agent"), hasBranches: true),
        new Question(6, 2, 0,
            "When did you return to the house?", new Answer(
                "I returned to the house an hour later, and saw someone running out of the house."), hasBranches: true),
        new Question(7, 1, 1,
            "Do you know him?", new Answer(
                "No, I just hear Olivia speak to him over the phone very often")),
        new Question(7, 1, 2,
            "Did the two of you fight?", new Answer(
                "No we would never! I left as soon as I felt it was necessary to escape the argument")),
        new Question(7, 2, 1,
            "Did you see who it was?", new Answer(
                "It was quite dark, so i could't see his face. But it did't look like James. ")),
        new Question(7, 2, 2,
            "Was James with you?", new Answer(
                "No he was not, he was going for the pizza, when I left the house.")),
        new Question(8, 0, 0,
            "Do you have any idea of who could have done this?", new Answer(
                "It must have been that person running away! If only I knew who it was! How could he just do this"), isEndPoint: true)
    };

    public static List<Question> officerQuestions = new List<Question>()
    {
        new Question(1, 0, 0,
                "Who was Involved?", new Answer(
                    "Based on the information we have gathered, the possible suspects are Mary, her boyfriend James and REA")),
            new Question(2, 0, 0,
                "What time did this happen?", new Answer(
                    "We arrived at the crime scene at 1 am")),
            new Question(3, 0, 0,
                "Who was at home when you arrived?", new Answer(
                    "It was only Mary, who was the one that called the cops. James showed up 30 minutes later.")),
            new Question(4, 0, 0,
                "Did you examine the body?", new Answer(
                    "Yes, the victim was stabbed multiple times by a kitchen knife")),
            new Question(5, 0, 0,
                "Do you have any idea who the murderer is?", new Answer(
                    "No, based on the information we have gathered from each of the suspects, the murderer could be any of them..."), isEndPoint: true)
    };

    public static List<Question> jamesQuestions = new List<Question>()
    {
        new Question(1, 0, 0,
                "Who are you to Olivia?", new Answer(
                    "I'm her boyfriend for the past 3 years"), hasBranches: true),
            new Question(2, 1, 0,
                "How many were you guys that day?", new Answer(
                    "We were just three people")),
            new Question(2, 2, 0,
                "What were you guys doing at the house?", new Answer(
                    "We were having fun talking about vacation ideas together")),
            new Question(3, 0, 0,
                "Where were you at the time of the murder of Olivia?", new Answer(
                    "I was on my way to collect the pizzas that we ordered beforehand.", _isTrue: false), hasBranches: true),
            new Question(4, 1, 0,
                "Where was the pizzas ordered from?", new Answer(
                    "They were from Papa Jones")),
            new Question(4, 2, 0,
                "Who ordered the pizzas?", new Answer(
                    "It was Mary who ordered them! "), hasBranches: true),
            new Question(5, 0, 0,
                "How long did it take you to collect the pizzas?", new Answer(
                    "It took me around an 45 minutes to get there", _isTrue: false), hasBranches: true),
            new Question(6, 1, 0,
                "What took you so long to get them?", new Answer(
                    "I drove into a lot of traffic which caused a big delay", _isTrue: false), hasBranches: true),
            new Question(6, 2, 0,
                "Any reason why it was you that went to get them?", new Answer(
                    "Because Mary ordered them, It must have been Mary who set this up!", _isTrue: false), hasBranches: true),
            new Question(7, 1, 1,
                "When did you return home with the pizzas?", new Answer(
                    "*sob* I came home an hour later and was too late, all the cops were there", false)),
            new Question(7, 1, 2,
                "Who is Mary to you?", new Answer(
                    "I thought she was Olivias best friend. It  appears they argued a lot lately which seems strange")),
            new Question(7, 2, 1,
                "When did you return home with the pizzas?", new Answer(
                    "*sob* I arrived home 1 hour later and was too late, all the cops were present at the house...", false)),
            new Question(7, 2, 2,
                "Who is Mary to you?", new Answer(
                    "I thought she was Olivias best friend. It  appears they argued a lot lately which seems strange")),
            new Question(8, 0, 0,
                "Who do you suspect the murderer to be?", new Answer(
                    "It has to be Mary! She setup all of this and called the cops", _isTrue: false), isEndPoint: true)
    };

    public static List<Question> harryQuestions = new List<Question>()
    {
        new Question(1, 0, 0,
                "Who are you to Olivia?", new Answer(
                    "I am her real estate agent"), hasBranches: true),
            new Question(2, 1, 0,
                "How long have you known Olivia for?", new Answer(
                    "We have been in contact for about 6 months now.")),
            new Question(2, 2, 0,
                "When did you last speak with Olivia?", new Answer(
                    "I called her last week", false)),
            new Question(3, 0, 0,
                "Where were you at the time of the murder of Olivia?", new Answer(
                    "I was at home by myself.", false)),
            new Question(4, 1, 0,
                "Did you know that Olivia has a boyfriend?", new Answer(
                    "Yes, but she didn't mention him very often")),
            new Question(4, 2, 0,
                "Did you and Olivia ever meet up?", new Answer(
                    "Occasionally yes")),
            new Question(5, 0, 0,
                "You called Olivia on the phone on the night she died. What was the call about?", new Answer(
                    "I- I was just talking with Olivia about selling me her house."), hasBranches: true),
            new Question(6, 1, 0,
                "Why did you want to buy her house?", new Answer(
                    "I buy houses I deem profitable as a long term investment. That's my job."), hasBranches: true),
            new Question(6, 2, 0,
                "How did the call between the two of you end?", new Answer(
                    "I heard an argument between Olivia and her friend which ended up in screams and smacks, so I hung up"), hasBranches: true),
            new Question(7, 1, 1,
                "Did olivia refuse to sell her house?", new Answer(
                    "Yes, she kept mentioning that she will keep it as a memory of her grandmother who passed away.")),
            new Question(7, 1, 2,
                "Who will own the house now?", new Answer(
                    "Well, because she told me that her grandmother passed away the house will eventually be sold to the highest bidder.")),
            new Question(7, 2, 1,
                "Did you go to the house afterwards?", new Answer(
                    "No! I did not want to get involved in their drama. I dont know what happened that night.")),
            new Question(7, 2, 2,
                "Were you romantically involved with Olivia?", new Answer(
                    "Uhm I guess you could say that. We started to like each other more over time")),
            new Question(8, 0, 0,
                "Do you have any idea, who could have done this?", new Answer(
                    "I have no idea! I did not expect this to ever even happen. I should'nt even be here!"), isEndPoint: true)
    };

    public static List<Question> maryAct2 = new List<Question>()
    {
        new Question(1, 0, 0, "Why were you covered in blood by the time the cops arrived?", new Answer(
            "I was only trying to save Olivia! I could not do anything else.")),
        new Question(2, 0, 0, "Did you hear Harry mention coming to the house on the phone?", new Answer(
            "I am not sure... I was too busy yelling at Olivia for speaking to him in the first place.")),
        new Question(3, 0, 0, "James is accusing you of the murder. Do you have any idea as to what that might be?", new Answer(
            "What?! No! Why would I kill my best friend?! We went on a vacation together to have fun. How could it ever end like this?!"), hasBranches: true),
        new Question(4, 1, 0, "Did you know, that you already had pizza downstairs in the kitchen before you left the house?", new Answer(
            "Pizza in the kitchen? But that doesn't make any sense! I specifically ordered pizzas for us all to eat, while playing cards. I did not pay attention to that, I swear! Why did I leave Olivia in her house like this... *sob*")),
        new Question(4, 2, 0, "Did you leave your front door unlocked when you ran out to the bar?", new Answer(
            "Yes! That was so stupid of me. Did I really cause this to happen? No... why...why did I leave Olivia in her house like this. We were too busy talking about my relationship problems I just screamed at her for talking to Harry. This is all my fault! *sob*"), isEndPoint: true)
    };

    public static List<Question> officerAct2 = new List<Question>()
    {
        new Question(1, 1, 0, "I need to ask you some questions about Harry.", new Answer(
            "Of course, detective. What do you what to know?")),
        new Question(2, 1, 0, "Did Olivia speak with Hary over the phone before her murder?", new Answer(
            "Yes. They had a call that lasted around 7 minutes.")),
        new Question(3, 1, 0, "Can you give me a summary of what the call was about?", new Answer(
            "The call involved Harry, who sounded a bit aggitated. He was proposing a higher price for Olivia's house, to which Olivia refused. The call ended when Olivia hung up after sounds of girls screaming and him yelling: I'm coming to you, do you he-")),


        new Question(1, 2, 0, "I need to ask you some questions about Mary.", new Answer(
            "Sure, detective. What do you want to know?")),
        new Question(2, 2, 0, "Was there anyone else at the house besides Mary by the time you arrived?", new Answer(
            "No. Mary was the only one present, when we arrived.")),
        new Question(3, 2, 0, "Was there any signs of bruises or blood on Mary when you saw her at that point?", new Answer(
            "Actually she did have blood on her hands and shoes, but she claims it was because of shock that she tried to do CPR in hopes of bringing Olivia back, before we got there.")),

        new Question(1, 3, 0, "I need to ask you some questions about James.", new Answer(
            "Of course. What do you need to know?")),
        new Question(2, 3, 0, "Did james return home with pizzas with him?", new Answer(
            "No. He did not have any pizzas with him, when he found out what happened to Olivia.")),
        new Question(3, 3, 0, "Can you check how much time has passed from when Mary ordered the pizzas and until they were paid for?", new Answer(
            "Yes, most certainly. It appears, that the food was picked up by James 23 minutes after Mary's phone call.")),
        new Question(4, 3, 0, "Where there any pizzas in the house, when you arrived?", new Answer("Yes. There were actually two unopened pizza boxes on the kitchen table."))
    };

    public static List<Question> jamesAct2 = new List<Question>()
    {
        new Question(1, 0, 0, "Were you aware, that Olivia was speaking to a real estate agent named Harry?", new Answer(
            "A real estate agent? I never knew about this. Olivia never told me.")),
        new Question(2, 0, 0, "They were talking together over the phoner shortly after you left to pick up the pizzas, that Mary had ordered. It caused Mary to freak out a bit.", new Answer(
            "That explains Olivia's weird behaviour lately. What the hell happened that night?!")),
        new Question(3, 0, 0, "Does Mary and Olivia often yell at each other?", new Answer(
            "Yes... Olivia keeps mentioning how jealous Mary is of our relationship. It is... I mean... was very loving, and Mary never had that. Mary always had issues with boyfriends cheating on her and expressed her feelings to Olivia.")),
        new Question(4, 1, 0, "Why did you go after pizza, when you had two pizzas at home already?", new Answer(
            "Wait... what? I was told, Mary ordered pizza, and that I should go get them. The whole thing was my own fault for ever leaving Olivia alone with Mary!")),
        new Question(4, 2, 0, "I have reason to believe that you are lying about the delivery delay on the pizzas, you brought home.", new Answer(
            "What? I clearly went there by myself. This whole thing was my own fault for ever leaving Olivia alone with Mary!"))
    };

    public static List<Question> harryAct2 = new List<Question>()
    {
        new Question(1, 0, 0, "Do you have any idea, why you are here today, Harry?", new Answer(
            "No? I assume, it's because I spoke to her over the phone the same day she died.", _isTrue: false)),
        new Question(2, 0, 0, "Are you certain that you were not at Olivia's house during the night she was murdered?", new Answer(
            "Wha- what are you talking abvout? H-how many times do I have to tell you that I.. I was at home!", _isTrue: false)),
        new Question(3, 0, 0, "I have reason to believe that you went to Olivia's house as soon as Olivia hung up on you.", new Answer(
            "*sob* Okay! I was there! I'm sorry! I just didn't know what to do. I- I came by to try to change her mind about the house, but when I... When I arrived, the door was left unlocked, so I walked in and... I s- saw Olivia dead on the ground.")),
        new Question(4, 1, 0, "Why did you not call the police?", new Answer(
            "I am so sorry, dedective! I did not know what to do! I was shook at really did not want to get involved and imprisoned because of this, so I ran out the front door. Y- you have to believe me. I did not do this!")),
        new Question(4, 2, 0, "Why did you lie about being home the whole time?", new Answer(
            "I am so sorry, dedective! I did not know what to do! I was shook at really did not want to get involved and imprisoned because of this, so I ran out the front door. Y- you have to believe me. I did not do this!"))
    };
}

