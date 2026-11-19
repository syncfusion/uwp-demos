using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.GroupBar
{
    public class MailViewModel
    {
        private ObservableCollection<SortedMailCollection> mainMailCollection;

        public ObservableCollection<SortedMailCollection> SelectedMailCollection
        {
            get { return mainMailCollection; }
            set { mainMailCollection = value;  }
        }

        public MailViewModel(string header)
        {
            if (header == "Inbox")
                mainMailCollection = GetInboxMails();
            else if (header == "Outbox")
                mainMailCollection = GetOutboxMails();
            else if (header == "Sent Items")
                mainMailCollection = GetSentMails();
            else if (header == "Drafts")
                mainMailCollection = GetMails(5, "Drafts");
            else if (header == "Junk Items")
                mainMailCollection = GetMails(2, "Junks");
            else if (header == "Deleted Items")
                mainMailCollection = GetMails(1, "Today");
        }
        public ObservableCollection<SortedMailCollection> GetMails(int count , string category)
        {
            ObservableCollection<SortedMailCollection> mails = new ObservableCollection<SortedMailCollection>();
            SortedMailCollection sorted1 = new SortedMailCollection();
            MailItem mail;
            for (int i = 1; i <= count; i++)
            {
                mail = new MailItem()
                {
                    From = "sumoneannoying@mail.com",
                    To = "thispc@mail.com",
                    IsUnRead = false,
                    Sender = "Some One",
                    Subject = "House loan",
                    Message = @"adsjnfasdflksadfjlasdfasdf
                            asdfasdfasdfasdfasdfasdf
                            asdfa
                            sdf
                            asdf
                            asdfffffffffffffasdfasdfaskdfuhcaoisfjoasxdijfaosjfasdfijasdoifjazdofas
                            sadfaksdfoascfjasodifasodixfjaodfmjaofiasdofiasdkfnkcnkzxjcnaisdufhspuefhq
                            asdfhaspidfhaspdiufuhansdicjapse8fy p aw97efyaspidfhaspdifjaspdiufhar7ofapsdfvimalasdciunasidupisducnipaudshfaprhfasd
                            asduahsdcuinasiuahspfihaperifasidfjaso8fu9r8fhapisdfjapsidfhasry8ghapsdfknapsdofarup8gahsdifhmas
                            asdhfupiasdfhasr8fhaskdnvasndcaosdfijas[o9rugfaspodphvmasduifhpa7rhfasdufhapr8faosdifamosdchas[df8asd
                            ahsdipfuhaspidfhaspe8fuosdjasodiifjasodfias\asdipfuhas[dfhaspaisuhfasncnakxcna;sdfas;difjasd;fjasdfgasdgfasdfjasdfhas;kdfha;sdkfhask;djfhasdjfh
                            asdkkfhasdfhasdifhasodifamso[difjmasdpifhyrsAFHNSIDUHASD;IFA;SDFJAS;DFa;sidf;masdhf;asdifhasmdasj;difja;sdif;asdfija"
                };
                sorted1.Header = category;
                sorted1.MailCollection.Add(mail);
            }
            mails.Add(sorted1);
            return mails;
        }       
        private ObservableCollection<SortedMailCollection> GetSentMails()
        {

            ObservableCollection<SortedMailCollection> sentMails = new ObservableCollection<SortedMailCollection>();
            SortedMailCollection sorted1 = new SortedMailCollection();
            sorted1.Header = "Today";
            MailItem mail1 = new MailItem();
            mail1.Sender = "Paolo Accorti";
            mail1.To = "Steve John";
            mail1.Subject = "Regarding Meeting";
            mail1.IsUnRead = false;
            mail1.Message = @" Hi Steve John,

                               Can we schedule Meeting Appointment for today?.
  
                               Thanks,
                               Accorti.";
            sorted1.MailCollection.Add(mail1);
            MailItem mail2 = new MailItem();
            mail2.Sender = "Henry";
            mail2.To = "Kol";
            mail2.Subject = "Customer has accpeted...";
            mail2.IsUnRead = false;
            mail2.Message = @" 
                            Hi Kol,

                            Customer has accepted our proposal. Would it be possible for arrange meeting tomorrow?.
  
                            Thanks,
                            Henry.";
            sorted1.MailCollection.Add(mail2);
            MailItem mail3 = new MailItem();
            mail3.Sender = "Martina Berglund";
            mail3.To = "Dev Khar";
            mail3.Subject = "Greeting";
            mail3.IsUnRead = false;
            mail3.Message = @" Hi Dev Khar,

                                Merry Christmas.
  
                                Thanks,
                                Martina Berglund.";
            sentMails.Add(sorted1);

            SortedMailCollection sorted2 = new SortedMailCollection();
            sorted2.Header = "Yesterday";
            MailItem mail4 = new MailItem();
            mail4.Sender = "Martín Sommer";
            mail4.To = "Anil Pahr";
            mail4.Subject = "Please come and collect ";
            mail4.IsUnRead = false;
            mail4.Message = @"  Hi Anil Pahr,

                                Please come and collect the rent receipt.
  
                                Thanks,
                                Martín Sommer.";
           // sorted2.MailCollection.Add(mail4);

            MailItem mail5 = new MailItem();
            mail5.Sender = "Victoria Ashworth";
            mail5.To = "Shanakar";
            mail5.Subject = "Regarding meeting";
            mail5.IsUnRead = false;
            mail5.Message = @"  Hi Shanakar,

    Yes we are available for meeting tomorrow.
  
    Thanks,
    Victoria Ashworth.";
            sorted2.MailCollection.Add(mail5);
            MailItem mail6 = new MailItem();
            mail6.Sender = "Yang Wang.";
            mail6.To = "Krish Kael";
            mail6.Subject = "Schedule meeting";
            mail6.IsUnRead = false;
            mail6.Message = @"  Hi Krish Kael,

    Please schedule the meeting on tomorrow.
  
    Thanks,
    Yang Wang.";
            //sorted2.MailCollection.Add(mail6);
            SortedMailCollection sorted3 = new SortedMailCollection();
            sorted3.Header = "LastWeek";
            MailItem mail7 = new MailItem();
            mail7.Sender = "Sven Ottlieb";
            mail7.To = "Marie Krilsh";
            mail7.Subject = "Greeting";
            mail7.IsUnRead = false;
            mail7.Message = @"  
    Hi Marie Krilsh,

    Merry Christmas.
  
    Thanks,
    Sven Ottlieb.";
            sorted3.MailCollection.Add(mail7);
            MailItem mail8 = new MailItem();
            mail8.Sender = "Aria Cruz";
            mail8.To = "Maria Krilsh";
            mail8.Subject = "New year Greeting";
            mail8.IsUnRead = false;
            mail8.Message = @" Hi Maria Krilsh,

    I wish you Happy new year..
  
    Thanks,
    Aria Cruz.";
         //   sorted3.MailCollection.Add(mail8);
            MailItem mail9 = new MailItem();
            mail9.Sender = "Diego Roel";
            mail9.To = "Michael";
            mail9.Subject = "Weekend Greeting";
            mail9.IsUnRead = false;
            mail9.Message = @" Hi Michael,

    Have a Great Weekend.
  
    Thanks,
    Diego Roel.";
         //   sorted3.MailCollection.Add(mail9);

            SortedMailCollection sorted4 = new SortedMailCollection();
            sorted4.Header = "Last Month";
            MailItem mail10 = new MailItem();
            mail10.Sender = "Paolo Accorti.";
            mail10.To = "Thomas Hardy";
            mail10.Subject = "Greeting";
            mail10.IsUnRead = false;
            mail10.Message = @"   Hi Thomas Hardy,

    Have a Great Day.
  
    Thanks,
    Paolo Accorti.";
          //  sorted4.MailCollection.Add(mail10);
            sentMails.Add(sorted2);
            sentMails.Add(sorted4);
            return sentMails;
        }
        private ObservableCollection<SortedMailCollection> GetInboxMails()
        {

            ObservableCollection<SortedMailCollection> inboxMails = new ObservableCollection<SortedMailCollection>();
            SortedMailCollection sorted1 = new SortedMailCollection();
            sorted1.Header = "Today";
            MailItem mail1 = new MailItem();
            mail1.Sender = "Maria Anders";
            mail1.To = "Jane Jonk";
            mail1.Subject = "Regarding Meeting";
            mail1.IsUnRead = true;
            mail1.Message = @" Hi Jane Jonk,

                               Can we schedule Meeting Appointment for today?.
  
                               Thanks,
                               Maria Anders.";
            sorted1.MailCollection.Add(mail1);
            MailItem mail2 = new MailItem();
            mail2.Sender = "Thomas Hardy";
            mail2.To = "Chris Kol";
            mail2.Subject = "Customer has accepted...";
            mail2.IsUnRead = true;
            mail2.Message = @" 
                            Hi Chris Kol,

                            Customer has accepted our proposal. Would it be possible for arrange meeting tomorrow?.
  
                            Thanks,
                            Thomas Hardy.";
            sorted1.MailCollection.Add(mail2);
            MailItem mail3 = new MailItem();
            mail3.Sender = "Christina Berglund";
            mail3.To = "Dev Khar";
            mail3.Subject = "Greeting";
            mail3.IsUnRead = false;
            mail3.Message = @" Hi Dev Khar,

                                Merry Christmas.
  
                                Thanks,
                                Christina Berglund.";
            sorted1.MailCollection.Add(mail3);
            inboxMails.Add(sorted1);

            SortedMailCollection sorted2 = new SortedMailCollection();
            sorted2.Header = "Yesterday";
            MailItem mail4 = new MailItem();
            mail4.Sender = "Martín Sommer";
            mail4.To = "Anil Pahr";
            mail4.Subject = "Please come and collect ";
            mail4.IsUnRead = false;
            mail4.Message = @"  Hi Anil Pahr,

                                Please come and collect the rent receipt.
  
                                Thanks,
                                Martín Sommer.";
            sorted2.MailCollection.Add(mail4);

            MailItem mail5 = new MailItem();
            mail5.Sender = "Victoria Ashworth";
            mail5.To = "Shanakar";
            mail5.Subject = "Regarding meeting";
            mail5.IsUnRead = false;
            mail5.Message = @"  Hi Shanakar,

    Yes we are available for meeting tomorrow.
  
    Thanks,
    Victoria Ashworth.";
           // sorted2.MailCollection.Add(mail5);
            MailItem mail6 = new MailItem();
            mail6.Sender = "Yang Wang.";
            mail6.To = "Krish Kael";
            mail6.Subject = "Schedule meeting";
            mail6.IsUnRead = false;
            mail6.Message = @"  Hi Krish Kael,

    Please schedule the meeting on tomorrow.
  
    Thanks,
    Yang Wang.";
            //sorted2.MailCollection.Add(mail6);
            SortedMailCollection sorted3 = new SortedMailCollection();
            sorted3.Header = "LastWeek";
            MailItem mail7 = new MailItem();
            mail7.Sender = "Sven Ottlieb";
            mail7.To = "Marie Krilsh";
            mail7.Subject = "Greeting";
            mail7.IsUnRead = false;
            mail7.Message = @"  
    Hi Marie Krilsh,

    Merry Christmas.
  
    Thanks,
    Sven Ottlieb.";
           // sorted3.MailCollection.Add(mail7);
            MailItem mail8 = new MailItem();
            mail8.Sender = "Aria Cruz";
            mail8.To = "Maria Krilsh";
            mail8.Subject = "New year Greeting";
            mail8.IsUnRead = false;
            mail8.Message = @" Hi Maria Krilsh,

    I wish you Happy new year..
  
    Thanks,
    Aria Cruz.";
           // sorted3.MailCollection.Add(mail8);
            MailItem mail9 = new MailItem();
            mail9.Sender = "Diego Roel";
            mail9.To = "Michael";
            mail9.Subject = "Weekend Greeting";
            mail9.IsUnRead = false;
            mail9.Message = @" Hi Michael,

    Have a Great Weekend.
  
    Thanks,
    Diego Roel.";
         //   sorted3.MailCollection.Add(mail9);

            SortedMailCollection sorted4 = new SortedMailCollection();
            sorted4.Header = "Last Month";
            MailItem mail10 = new MailItem();
            mail10.Sender = "Paolo Accorti.";
            mail10.To = "John Michael";
            mail10.Subject = "Greeting";
            mail10.IsUnRead = false;
            mail10.Message = @"   Hi John Michael,

    Have a Great Day.
  
    Thanks,
    Paolo Accorti.";
          //  sorted4.MailCollection.Add(mail10);
            inboxMails.Add(sorted2);
            inboxMails.Add(sorted3);
            inboxMails.Add(sorted4);
            return inboxMails;
        }
        private ObservableCollection<SortedMailCollection> GetOutboxMails()
        {

            ObservableCollection<SortedMailCollection> sentMails = new ObservableCollection<SortedMailCollection>();            
            SortedMailCollection sorted2 = new SortedMailCollection();
            sorted2.Header = "Yesterday";
            MailItem mail4 = new MailItem();
            mail4.Sender = "Martín Sommer";
            mail4.To = "Anil Pahr";
            mail4.Subject = "Please come and collect ";
            mail4.IsUnRead = false;
            mail4.Message = @"  Hi Anil Pahr,

                                Please come and collect the rent receipt.
  
                                Thanks,
                                Martín Sommer.";
            sorted2.MailCollection.Add(mail4);

            MailItem mail5 = new MailItem();
            mail5.Sender = "Victoria Ashworth";
            mail5.To = "Shanakar";
            mail5.Subject = "Regarding meeting";
            mail5.IsUnRead = false;
            mail5.Message = @"  Hi Shanakar,

    Yes we are available for meeting tomorrow.
  
    Thanks,
    Victoria Ashworth.";
            sorted2.MailCollection.Add(mail5);
            MailItem mail6 = new MailItem();
            mail6.Sender = "Yang Wang.";
            mail6.To = "Krish Kael";
            mail6.Subject = "Schedule meeting";
            mail6.IsUnRead = false;
            mail6.Message = @"  Hi Krish Kael,

    Please schedule the meeting on tomorrow.
  
    Thanks,
    Yang Wang.";
            sorted2.MailCollection.Add(mail6);
            sentMails.Add(sorted2);
            return sentMails;
        }

    }
}
