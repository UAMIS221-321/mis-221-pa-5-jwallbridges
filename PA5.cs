using System;

namespace TrainLikeAChampion
{

    class Trainer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
    public class Listing
    {
    public string ListingId { get; set; }
    public string TrainerName { get; set; }
    public string SessionDate { get; set; }
    public string SessionTime { get; set; }
    public decimal Cost { get; set; }
    public bool IsTaken { get; set; }

    public Listing(string listingId, string trainerName, string sessionDate, string sessionTime, decimal cost, bool isTaken)
    {
        ListingId = listingId;
        TrainerName = trainerName;
        SessionDate = sessionDate;
        SessionTime = sessionTime;
        Cost = cost;
        IsTaken = isTaken;
    }

    public override string ToString()
    {
        return String.Format("{0}#{1}#{2}#{3}#{4}#{5}", ListingId, TrainerName, SessionDate, SessionTime, Cost.ToString("0.00"), IsTaken.ToString());
    }
    }

    public class Booking
    {
    public string SessionId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEMail { get; set; }
    public string SessionDate { get; set; }
    public string TrainerID { get; set; }
    public string TrainerName { get; set; }
    public string Status { get; set; }

    public Booking(string sessionId, string customerName, string customerEMail, string sessionDate, string trainerID, string trainerName, string status)
    {
        SessionId = sessionId;
        CustomerName = customerName;
        CustomerEMail = customerEMail;
        SessionDate = sessionDate;
        TrainerID = trainerID;
        TrainerName = trainerName;
        Status = status;
    }

    public override string ToString()
    {
        return String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}", SessionId, CustomerName, CustomerEMail, SessionDate, TrainerID, TrainerName, Status);
    }
    }


    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Train Like A Champion - Personal Fitness");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1. Manage Trainer Data");
                Console.WriteLine("2. Manage Listing Data");
                Console.WriteLine("3. Manage Customer Booking Data");
                Console.WriteLine("4. Run Reports");
                Console.WriteLine("5. Exit the Application");

                Console.Write("Enter your choice (1-5): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageTrainerData();
                        Console.WriteLine("Managing Trainer Data...");
                        break;
                    case "2":
                        ManageListingData();
                        Console.WriteLine("Managing Listing Data...");
                        break;
                    case "3":
                        Booking();
                        Console.WriteLine("Managing Customer Booking Data...");
                        break;
                    case "4":
                        // Call a function to run reports
                        Console.WriteLine("Running Reports...");
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Exiting the Application...");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice. Please enter a valid choice (1-5).");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void ManageTrainerData() {

            string trainersFile = "trainers.txt";

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Trainer Menu");
                Console.WriteLine("------------");
                Console.WriteLine("1. Add Trainer");
                Console.WriteLine("2. Edit Trainer");
                Console.WriteLine("3. Delete Trainer");
                Console.WriteLine("4. Exit");

                Console.Write("Enter your choice (1-4): ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddTrainer(trainersFile);
                        break;
                    case "2":
                        EditTrainer(trainersFile);
                        break;
                    case "3":
                        DeleteTrainer(trainersFile);
                        break;
                    case "4":
                        exit = true;
                        Console.WriteLine("Exiting the Trainer Menu...");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice. Please enter a valid choice (1-4).");
                        break;
                }

                Console.WriteLine();
            }

        }

        static void AddTrainer(string trainersFile) {
            Console.WriteLine("Adding Trainer");
            Console.WriteLine("--------------");

            Console.Write("Enter Trainer ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter Trainer Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Trainer Address: ");
            string address = Console.ReadLine();

            Console.Write("Enter Trainer Email: ");
            string email = Console.ReadLine();

            Trainer trainer = new Trainer { ID = id, Name = name, Address = address, Email = email };

            using (StreamWriter sw = new StreamWriter(trainersFile, true))
            {
                sw.WriteLine($"{trainer.ID}#{trainer.Name}#{trainer.Address}#{trainer.Email}");
            }

            Console.WriteLine("Trainer Added Successfully!");
        }

        static void EditTrainer(string trainersFile) {
            Console.WriteLine("Editing Trainer");
            Console.WriteLine("---------------");

            Console.Write("Enter Trainer ID to Edit: ");
            string id = Console.ReadLine();

            bool trainerFound = false;

            string[] lines = File.ReadAllLines(trainersFile);

            using (StreamWriter sw = new StreamWriter(trainersFile, false))
            {
                foreach (string line in lines)
                {
                    string[] fields = line.Split('#');

                    if (fields[0] == id)
                    {
                        Console.WriteLine("Trainer Found!");

                        Console.Write("Enter Trainer Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter Trainer Address: ");
                        string address = Console.ReadLine();

                        Console.Write("Enter Trainer Email: ");
                        string email = Console.ReadLine();

                        Trainer trainer = new Trainer { ID = id, Name = name, Address = address, Email = email };

                        sw.WriteLine($"{trainer.ID}#{trainer.Name}#{trainer.Address}#{trainer.Email}");

                        Console.WriteLine("Trainer Edited Successfully!");

                        trainerFound = true;
                    }
                    else
                    {
                        sw.WriteLine(line);
                    }
                }
            }

            if (!trainerFound)
            {
                Console.WriteLine("Trainer Not Found!");
            }
        }

        static void DeleteTrainer(string trainersFile) {
            // Read all the trainers from the trainers file
            string[] lines = File.ReadAllLines(trainersFile);

            Console.WriteLine("What is the trainer's ID?");
            string trainerId = Console.ReadLine();

            // Find the line that corresponds to the trainer ID
            int lineToDelete = -1;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split('#');
                if (fields[0] == trainerId)
            {
            lineToDelete = i;
            break;
            }
            }

            if (lineToDelete != -1)
            {
                // Remove the line from the array of lines
                string[] newLines = new string[lines.Length - 1];
                int j = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i != lineToDelete)
                    {
                        newLines[j] = lines[i];
                        j++;
                    }
                }

                // Write the new array of lines back to the trainers file
                File.WriteAllLines("trainers.txt", newLines);
                Console.WriteLine("Trainer with ID {0} deleted successfully.", trainerId);
            }
            else
            {
                Console.WriteLine("Trainer with ID {0} not found.", trainerId);
            }
        }



        static void ManageListingData() {
            string listingsFile = "listings.txt";

            bool exit = false;

            //Listing list = new Listing();

            while (!exit)
            {
                Console.WriteLine("Listing Menu");
                Console.WriteLine("------------");
                Console.WriteLine("1. Add Listing");
                Console.WriteLine("2. Edit Listing");
                Console.WriteLine("3. Delete Listing");
                Console.WriteLine("4. Exit");

                Console.Write("Enter your choice (1-4): ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddListing(listingsFile);
                        break;
                    case "2":
                        EditListing(listingsFile);
                        break;
                    case "3":
                        DeleteListing(listingsFile);
                        break;
                    case "4":
                        exit = true;
                        Console.WriteLine("Exiting the Listing Menu...");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice. Please enter a valid choice (1-4).");
                        break;
                }

                Console.WriteLine();
            }

        }

        static void AddListing(string listingsFile) {
            // Get input from user for listing details
            Console.Write("Enter Listing ID: ");
            string listingId = Console.ReadLine();

            Console.Write("Enter Trainer Name: ");
            string trainerName = Console.ReadLine();

            Console.Write("Enter Date of Session (MM/dd/yyyy): ");
            string sessionDate = Console.ReadLine();

            Console.Write("Enter Time of Session (hh:mm tt): ");
            string sessionTime = Console.ReadLine();

            Console.Write("Enter Cost of Session: ");
            decimal sessionCost = decimal.Parse(Console.ReadLine());

            Console.Write("Is the listing taken? (Y/N): ");
            bool isTaken = (Console.ReadLine().ToUpper() == "Y");

            // Create new listing object
            Listing newListing = new Listing(listingId, trainerName, sessionDate, sessionTime, sessionCost, isTaken);

            // Add new listing to file
            using (StreamWriter sw = File.AppendText(listingsFile))
            {
                sw.WriteLine(newListing);
            }

            Console.WriteLine("\nNew listing added successfully.\n");
        }

        static void EditListing(string listingsFile) {
            Console.WriteLine("Editing Listing");
            Console.WriteLine("---------------");

            Console.Write("Enter Listing ID to Edit: ");
            string id = Console.ReadLine();

            bool listingFound = false;

            string[] lines = File.ReadAllLines(listingsFile);

            using (StreamWriter sw = new StreamWriter(listingsFile, false))
            {
                foreach (string line in lines)
                {
                    string[] fields = line.Split('#');

                    if (fields[0] == id)
                    {
                        Console.WriteLine("Listing Found!");

                        Console.Write("Enter Trainer Name: ");
                        string trainerName = Console.ReadLine();

                        Console.Write("Enter Session Date (MM/dd/yyyy): ");
                        string sessionDate = Console.ReadLine();

                        Console.Write("Enter Session Time (HH:mm tt): ");
                        string sessionTime = Console.ReadLine();

                        Console.Write("Enter Cost: ");
                        decimal cost = Decimal.Parse(Console.ReadLine());

                        Console.Write("Is the Listing Taken? (Y/N): ");
                        bool isTaken = bool.Parse(Console.ReadLine());

                        Listing listing = new Listing(id, trainerName, sessionDate, sessionTime, cost, isTaken);

                        sw.WriteLine(listing.ToString());

                        Console.WriteLine("Listing Edited Successfully!");

                        listingFound = true;
                    }
                    else
                    {
                        sw.WriteLine(line);
                    }
                }
            }

            if (!listingFound)
            {
                Console.WriteLine("Listing Not Found!");
            }
        }

        static void DeleteListing(string listingsFile) {
            Console.WriteLine("Deleting Listing");
            Console.WriteLine("----------------");

            Console.Write("Enter Listing ID to Delete: ");
            string id = Console.ReadLine();

            bool listingFound = false;

            string[] lines = File.ReadAllLines(listingsFile);

            using (StreamWriter sw = new StreamWriter(listingsFile, false))
            {
                foreach (string line in lines)
                {
                    string[] fields = line.Split('#');

                    if (fields[0] == id)
                    {
                        Console.WriteLine("Listing Found!");

                        listingFound = true;
                    }
                    else
                    {
                        sw.WriteLine(line);
                    }
                }
            }

            if (listingFound)
            {
                Console.WriteLine("Listing Deleted Successfully!");
            }
            else
            {
                Console.WriteLine("Listing Not Found!");
            }
        }

        static void Booking() {
            string transactionsFile = "transactions.txt";
            
            bool exit = false;

            while (!exit) {
            Console.WriteLine("BOOKING MANAGEMENT MENU");
            Console.WriteLine("-----------------------");
            Console.WriteLine("1. View available training sessions");
            Console.WriteLine("2. Book a training session");
            Console.WriteLine("3. View booked training sessions");
            Console.WriteLine("4. View completed training sessions");
            Console.WriteLine("5. Cancel a training session");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your choice (1-6): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewAvailableSessions(transactionsFile);
                    break;
                case "2":
                    BookSession(transactionsFile);
                    break;
                case "3":
                    ViewBookedSessions(transactionsFile);
                    break;
                case "4":
                    ViewCompletedSessions(transactionsFile);
                    break;
                case "5":
                    CancelSession(transactionsFile);
                    break;
                case "6":
                    Console.WriteLine("Exiting booking management menu...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                break;
            }

            Console.WriteLine();
            }
        }

        static void ViewAvailableSessions(string transactionsFile) {
            Console.WriteLine("Viewing Available Sessions");
            Console.WriteLine("----------------");

            string lFile = "listings.txt";

            string [] lines = File.ReadAllLines(lFile);
            //bool listingsfound = false;

            using (StreamReader sr = new StreamReader(lFile, false)) {
                
                foreach (string line in lines) {

                    string [] fields = line.Split('#');
                    //Console.WriteLine(fields[5]);

                    if (fields[5] == "True") {
                        Console.WriteLine(fields [0] + " " + fields[1] + " " + fields[2] + " " + fields[4]);
                    }


                }
            }

        }

        static void BookSession(string transactionsFile) {

            Console.Write("Enter Booking ID: ");
            string bookingID = Console.ReadLine();

            Console.Write("Enter Customer Name: ");
            string customerName = Console.ReadLine();

            Console.Write("Enter Customer Email: ");
            string customerEMail = Console.ReadLine();

            Console.Write("Enter Session Date: ");
            string sessionDate = Console.ReadLine();

            Console.Write("Enter the Trainer's ID: ");
            string trainerID = Console.ReadLine();

            Console.Write("Enter Trainer Name: ");
            string trainerName = Console.ReadLine();

            Console.Write("What is the status of this listing? (Either Completed, Canceled, or Booked): ");
            string status = Console.ReadLine();

            // Create new booking object
            Booking booking = new Booking(bookingID, customerName, customerEMail, sessionDate, trainerID, trainerName, status);

            //Console.WriteLine("Booking in");

            // Add new booking to file
            using (StreamWriter sw = new StreamWriter(transactionsFile, true))
            {
                sw.WriteLine($"{booking.SessionId}#{booking.CustomerName}#{booking.CustomerEMail}#{booking.SessionDate}#{booking.TrainerID}#{booking.TrainerName}#{booking.Status}");
            }

            Console.WriteLine("\nNew Booking added successfully.\n");


        }

        static void ViewBookedSessions (string transactionsFile) {
            Console.WriteLine("Viewing Unavailable Sessions");
            Console.WriteLine("----------------");

            string lFile = "listings.txt";

            string [] lines = File.ReadAllLines(lFile);
            //bool listingsfound = false;

            using (StreamReader sr = new StreamReader(lFile, false)) {
                
                foreach (string line in lines) {

                    string [] fields = line.Split('#');
                    //Console.WriteLine(fields[5]);

                    if (fields[5] == "False") {
                        Console.WriteLine(fields [0] + " " + fields[1] + " " + fields[2] + " " + fields[4]);
                    }


                }
            }
        }

        static void ViewCompletedSessions (string transactionsFile) {
            Console.WriteLine("Viewing Completed Sessions");
            Console.WriteLine("----------------");

            string [] lines = File.ReadAllLines(transactionsFile);
            //bool listingsfound = false;

            using (StreamReader sr = new StreamReader(transactionsFile, false)) {
                
                foreach (string line in lines) {

                    string [] fields = line.Split('#');
                    //Console.WriteLine(fields[5]);

                    if (fields[6] == "Completed") {
                        Console.WriteLine(fields [0] + " " + fields[1] + " " + fields[2] + " " + fields[4] + " " + fields[5] + " " + fields[6]);
                    }


                }
            }
        }

        static void CancelSession(string transactionsFile) {

        }

    }
}
