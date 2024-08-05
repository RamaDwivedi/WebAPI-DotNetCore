
    public class Employee 
    {
        public int Id { get; set;}
        public string Name { get; set;}

        public string City{ get; set;}
        public string PhoneNumber{ get; set;}


         public Employee()
         {
            
         }
        public Employee(int id, string name, string city,string phoneNumber)
        {
            Id = id;
            Name = name;
            City = city;
            PhoneNumber=phoneNumber;
        }


    }

