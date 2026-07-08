using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid
{
    /// <summary>
    /// This class represents the CustomersInfoRepository
    /// </summary>
    class CustomersInfoRepository
    {
        Random r = new Random();
        Dictionary<string, string> companyName = new Dictionary<string, string>();
        Dictionary<string, string> contactName = new Dictionary<string, string>();
        Dictionary<string, string> contactTitle = new Dictionary<string, string>();
        Dictionary<string, string> address = new Dictionary<string, string>();
        Dictionary<string, string> city = new Dictionary<string, string>();
        Dictionary<string, string> postalCode = new Dictionary<string, string>();
        Dictionary<string, string> country = new Dictionary<string, string>();
        static int discount;

        /// <summary>
        /// Get the CustomerDetails
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public ObservableCollection<Customers> GetCustomerDetails(int count)
        {
            ObservableCollection<Customers> customers = new ObservableCollection<Customers>();
            PopulateData();
            for (int i = 1; i < count; i++)
            {
                customers.Add(GetCustomers(i));
            }
            return customers;
        }

        private void PopulateData()
        {
            companyName.Add("ABBCB", "Alfreds Futterkiste");
            companyName.Add("DDEAD", "Ana Trujillo Emparedados y helados");
            companyName.Add("GBGGG", "Antonio Moreno Taquería");
            companyName.Add("AFCEC", "Around the Horn");
            companyName.Add("AEBAG", "Berglunds snabbköp");
            companyName.Add("GGEBG", "Blauer See Delikatessen");
            companyName.Add("BBEDB", "Blondel père et fils");
            companyName.Add("DCFBB", "Bólido Comidas preparadas");
            companyName.Add("EGEAG", "Bon app'");
            companyName.Add("BDDDD", "Bottom-Dollar Markets");
            companyName.Add("GBGFC", "B's Beverages");
            companyName.Add("CGADB", "Cactus Comidas para llevar");
            companyName.Add("DFCAB", "Centro comercial Moctezuma");
            companyName.Add("FDCED", "Chop-suey Chinese");
            companyName.Add("FGACE", "Comércio Mineiro");
            companyName.Add("AAAAF", "Consolidated Holdings");
            companyName.Add("CDAFA", "Drachenblut Delikatessen");
            companyName.Add("ABGBG", "Du monde entier");
            companyName.Add("DBCAF", "Eastern Connection");
            companyName.Add("FDDFB", "Ernst Handel");
            companyName.Add("EBGAE", "Familia Arquibaldo");
            companyName.Add("GADBB", "FISSA Fabrica Inter. Salchichas S.A.");
            companyName.Add("BGGEF", "Folies gourmandes");
            companyName.Add("AEEGB", "Folk och fä HB");
            companyName.Add("DEAFA", "Frankenversand");
            companyName.Add("DEAEG", "France restauration");
            companyName.Add("DDBGA", "Franchi S.p.A.");
            companyName.Add("BEAAB", "Furia Bacalhau e Frutos do Mar");
            companyName.Add("GEEAE", "Galería del gastrónomo");
            companyName.Add("ECECF", "Godos Cocina Típica");
            companyName.Add("AGDEE", "Gourmet Lanchonetes");
            companyName.Add("GAAAB", "Great Lakes Food Market");
            companyName.Add("CAGBE", "GROSELLA-Restaurante");
            companyName.Add("EEAED", "Hanari Carnes");
            companyName.Add("DEDFB", "HILARIÓN-Abastos");
            companyName.Add("GGABD", "Hungry Coyote Import Store");
            companyName.Add("AGFGF", "Hungry Owl All-Night Grocers");
            companyName.Add("EAEBA", "Island Trading");
            companyName.Add("BDAFF", "Königlich Essen");
            companyName.Add("GFDCG", "La corne d'abondance");
            companyName.Add("GCAFB", "La maison d'Asie");
            companyName.Add("DCFCF", "Laughing Bacchus Wine Cellars");
            companyName.Add("AGGEA", "Lazy K Kountry Store");
            companyName.Add("CCGDA", "Lehmanns Marktstand");
            companyName.Add("EAFDC", "Let's Stop N Shop");
            companyName.Add("GDFGC", "LILA-Supermercado");
            companyName.Add("ADEBD", "LINO-Delicateses");
            companyName.Add("FGGEB", "Lonesome Pine Restaurant");
            companyName.Add("BBBDA", "Magazzini Alimentari Riuniti");
            companyName.Add("BEDEC", "Maison Dewey");

            contactName.Add("ABBCB", "Maria Anders");
            contactName.Add("DDEAD", "Ana Trujillo");
            contactName.Add("GBGGG", "Antonio Moreno");
            contactName.Add("AFCEC", "Thomas Hardy");
            contactName.Add("AEBAG", "Christina Berglund");
            contactName.Add("GGEBG", "Hanna Moos");
            contactName.Add("BBEDB", "Frédérique Citeaux");
            contactName.Add("DCFBB", "Martín Sommer");
            contactName.Add("EGEAG", "Laurence Lebihan");
            contactName.Add("BDDDD", "Elizabeth Lincoln");
            contactName.Add("GBGFC", "Victoria Ashworth");
            contactName.Add("CGADB", "Patricio Simpson");
            contactName.Add("DFCAB", "Francisco Chang");
            contactName.Add("FDCED", "Yang Wang");
            contactName.Add("FGACE", "Pedro Afonso");
            contactName.Add("AAAAF", "Elizabeth Brown");
            contactName.Add("CDAFA", "Sven Ottlieb");
            contactName.Add("ABGBG", "Janine Labrune");
            contactName.Add("DBCAF", "Ann Devon");
            contactName.Add("FDDFB", "Roland Mendel");
            contactName.Add("EBGAE", "Aria Cruz");
            contactName.Add("GADBB", "Diego Roel");
            contactName.Add("BGGEF", "Martine Rancé");
            contactName.Add("AEEGB", "Maria Larsson");
            contactName.Add("DEAFA", "Peter Franken");
            contactName.Add("DEAEG", "Carine Schmitt");
            contactName.Add("DDBGA", "Paolo Accorti");
            contactName.Add("BEAAB", "Lino Rodriguez ");
            contactName.Add("GEEAE", "Eduardo Saavedra");
            contactName.Add("ECECF", "José Pedro Freyre");
            contactName.Add("AGDEE", "André Fonseca");
            contactName.Add("GAAAB", "Howard Snyder");
            contactName.Add("CAGBE", "Manuel Pereira");
            contactName.Add("EEAED", "Mario Pontes");
            contactName.Add("DEDFB", "Carlos Hernández");
            contactName.Add("GGABD", "Yoshi Latimer");
            contactName.Add("AGFGF", "Patricia McKenna");
            contactName.Add("EAEBA", "Helen Bennett");
            contactName.Add("BDAFF", "Philip Cramer");
            contactName.Add("GFDCG", "Daniel Tonini");
            contactName.Add("GCAFB", "Annette Roulet");
            contactName.Add("DCFCF", "Yoshi Tannamuri");
            contactName.Add("AGGEA", "John Steel");
            contactName.Add("CCGDA", "Renate Messner");
            contactName.Add("EAFDC", "Jaime Yorres");
            contactName.Add("GDFGC", "Carlos González");
            contactName.Add("ADEBD", "Felipe Izquierdo");
            contactName.Add("FGGEB", "Fran Wilson");
            contactName.Add("BBBDA", "Giovanni Rovelli");
            contactName.Add("BEDEC", "Catherine Dewey");

            contactTitle.Add("ABBCB", "Sales Representative");
            contactTitle.Add("DDEAD", "Owner");
            contactTitle.Add("GBGGG", "Owner");
            contactTitle.Add("AFCEC", "Sales Representative");
            contactTitle.Add("AEBAG", "Order Administrator");
            contactTitle.Add("GGEBG", "Sales Representative");
            contactTitle.Add("BBEDB", "Marketing Manager");
            contactTitle.Add("DCFBB", "Owner");
            contactTitle.Add("EGEAG", "Owner");
            contactTitle.Add("BDDDD", "Accounting Manager");
            contactTitle.Add("GBGFC", "Sales Representative");
            contactTitle.Add("CGADB", "Sales Agent");
            contactTitle.Add("DFCAB", "Marketing Manager");
            contactTitle.Add("FDCED", "Owner");
            contactTitle.Add("FGACE", "Sales Associate");
            contactTitle.Add("AAAAF", "Sales Representative");
            contactTitle.Add("CDAFA", "Order Administrator");
            contactTitle.Add("ABGBG", "Owner");
            contactTitle.Add("DBCAF", "Sales Agent");
            contactTitle.Add("FDDFB", "Sales Manager");
            contactTitle.Add("EBGAE", "Marketing Assistant");
            contactTitle.Add("GADBB", "Accounting Manager");
            contactTitle.Add("BGGEF", "Assistant Sales Agent");
            contactTitle.Add("AEEGB", "Owner");
            contactTitle.Add("DEAFA", "Marketing Manager");
            contactTitle.Add("DEAEG", "Marketing Manager");
            contactTitle.Add("DDBGA", "Sales Representative");
            contactTitle.Add("BEAAB", "Sales Manager");
            contactTitle.Add("GEEAE", "Marketing Manager");
            contactTitle.Add("ECECF", "Sales Manager");
            contactTitle.Add("AGDEE", "Sales Associate");
            contactTitle.Add("GAAAB", "Marketing Manager");
            contactTitle.Add("CAGBE", "Owner");
            contactTitle.Add("EEAED", "Accounting Manager");
            contactTitle.Add("DEDFB", "Sales Representative");
            contactTitle.Add("GGABD", "Sales Representative");
            contactTitle.Add("AGFGF", "Sales Associate");
            contactTitle.Add("EAEBA", "Marketing Manager");
            contactTitle.Add("BDAFF", "Sales Associate");
            contactTitle.Add("GFDCG", "Sales Representative");
            contactTitle.Add("GCAFB", "Sales Manager");
            contactTitle.Add("DCFCF", "Marketing Assistant");
            contactTitle.Add("AGGEA", "Marketing Manager");
            contactTitle.Add("CCGDA", "Sales Representative");
            contactTitle.Add("EAFDC", "Owner");
            contactTitle.Add("GDFGC", "Accounting Manager");
            contactTitle.Add("ADEBD", "Owner");
            contactTitle.Add("FGGEB", "Sales Manager");
            contactTitle.Add("BBBDA", "Marketing Manager");
            contactTitle.Add("BEDEC", "Sales Agent");

            address.Add("ABBCB", "Obere Str. 57");
            address.Add("DDEAD", "Avda. de la Constitución 2222");
            address.Add("GBGGG", "Mataderos  2312");
            address.Add("AFCEC", "120 Hanover Sq.");
            address.Add("AEBAG", "Berguvsvägen  8");
            address.Add("GGEBG", "Forsterstr. 57");
            address.Add("BBEDB", "24, place Kléber");
            address.Add("DCFBB", "C/ Araquil, 67");
            address.Add("EGEAG", "12, rue des Bouchers");
            address.Add("BDDDD", "23 Tsawassen Blvd.");
            address.Add("GBGFC", "Fauntleroy Circus");
            address.Add("CGADB", "Cerrito 333");
            address.Add("DFCAB", "Sierras de Granada 9993");
            address.Add("FDCED", "Hauptstr. 29");
            address.Add("FGACE", "Av. dos Lusíadas, 23");
            address.Add("AAAAF", "Berkeley Gardens");
            address.Add("CDAFA", "12  Brewery ");
            address.Add("ABGBG", "Walserweg 21");
            address.Add("DBCAF", "67, rue des Cinquante Otages");
            address.Add("FDDFB", "35 King George");
            address.Add("EBGAE", "Kirchgasse 6");
            address.Add("GADBB", "Rua Orós, 92");
            address.Add("BGGEF", "C/ Moralzarzal, 86");
            address.Add("AEEGB", "184, chaussée de Tournai");
            address.Add("DEAFA", "Åkergatan 24");
            address.Add("DEAEG", "Berliner Platz 43");
            address.Add("DDBGA", "54, rue Royale");
            address.Add("BEAAB", "Via Monte Bianco 34");
            address.Add("GEEAE", "Jardim das rosas n. 32");
            address.Add("ECECF", "Rambla de Cataluña, 23");
            address.Add("AGDEE", "C/ Romero, 33");
            address.Add("GAAAB", "Av. Brasil, 442");
            address.Add("CAGBE", "2732 Baker Blvd.");
            address.Add("EEAED", "5ª Ave. Los Palos Grandes");
            address.Add("DEDFB", "Rua do Paço, 67");
            address.Add("GGABD", "Carrera 22 con Ave. Carlos Soublette #8-35");
            address.Add("AGFGF", "City Center Plaza");
            address.Add("EAEBA", "516 Main St.");
            address.Add("BDAFF", "8 Johnstown Road");
            address.Add("GFDCG", "Garden House");
            address.Add("GCAFB", "Crowther Way");
            address.Add("DCFCF", "Maubelstr. 90");
            address.Add("AGGEA", "67, avenue de l'Europe");
            address.Add("CCGDA", "1 rue Alsace-Lorraine");
            address.Add("EAFDC", "1900 Oak St.");
            address.Add("GDFGC", "12 Orchestra Terrace");
            address.Add("ADEBD", "Magazinweg 7");
            address.Add("FGGEB", "87 Polk St.");
            address.Add("BBBDA", "Suite 5");
            address.Add("BEDEC", "Carrera 52 con Ave. Bolívar #65-98 Llano Largo");

            city.Add("ABBCB", "Berlin");
            city.Add("DDEAD", "México D.F.");
            city.Add("GBGGG", "México D.F.");
            city.Add("AFCEC", "London");
            city.Add("AEBAG", "Luleå");
            city.Add("GGEBG", "Mannheim");
            city.Add("BBEDB", "Strasbourg");
            city.Add("DCFBB", "Madrid");
            city.Add("EGEAG", "Marseille");
            city.Add("BDDDD", "Tsawassen");
            city.Add("GBGFC", "London");
            city.Add("CGADB", "Buenos Aires");
            city.Add("DFCAB", "México D.F.");
            city.Add("FDCED", "Bern");
            city.Add("FGACE", "São Paulo");
            city.Add("AAAAF", "London");
            city.Add("CDAFA", "Aachen");
            city.Add("ABGBG", "Nantes");
            city.Add("DBCAF", "London");
            city.Add("FDDFB", "Graz");
            city.Add("EBGAE", "São Paulo");
            city.Add("GADBB", "Madrid");
            city.Add("BGGEF", "Lille");
            city.Add("AEEGB", "Bräcke");
            city.Add("DEAFA", "München");
            city.Add("DEAEG", "Nantes");
            city.Add("DDBGA", "Torino");
            city.Add("BEAAB", "Lisboa");
            city.Add("GEEAE", "Barcelona");
            city.Add("ECECF", "Sevilla");
            city.Add("AGDEE", "Campinas");
            city.Add("GAAAB", "Eugene");
            city.Add("CAGBE", "Caracas");
            city.Add("EEAED", "Rio de Janeiro");
            city.Add("DEDFB", "San Cristóbal");
            city.Add("GGABD", "Elgin");
            city.Add("AGFGF", "Cork");
            city.Add("EAEBA", "Hedge End");
            city.Add("BDAFF", "Brandenburg");
            city.Add("GFDCG", "Versailles");
            city.Add("GCAFB", "Toulouse");
            city.Add("DCFCF", "Vancouver");
            city.Add("AGGEA", "Walla Walla");
            city.Add("CCGDA", "Frankfurt a.M. ");
            city.Add("EAFDC", "San Francisco");
            city.Add("GDFGC", "Barquisimeto");
            city.Add("ADEBD", "I. de Margarita");
            city.Add("FGGEB", "Portland");
            city.Add("BBBDA", "Bergamo");
            city.Add("BEDEC", "Bruxelles");

            postalCode.Add("ABBCB", "12209");
            postalCode.Add("DDEAD", "5021");
            postalCode.Add("GBGGG", "5023");
            postalCode.Add("AFCEC", "WA1 1DP");
            postalCode.Add("AEBAG", "S-958 22");
            postalCode.Add("GGEBG", "68306");
            postalCode.Add("BBEDB", "67000");
            postalCode.Add("DCFBB", "28023");
            postalCode.Add("EGEAG", "13008");
            postalCode.Add("BDDDD", "T2F 8M4");
            postalCode.Add("GBGFC", "EC2 5NT");
            postalCode.Add("CGADB", "1010");
            postalCode.Add("DFCAB", "5022");
            postalCode.Add("FDCED", "3012");
            postalCode.Add("FGACE", "05432-043");
            postalCode.Add("AAAAF", "WX1 6LT");
            postalCode.Add("CDAFA", "52066");
            postalCode.Add("ABGBG", "44000");
            postalCode.Add("DBCAF", "WX3 6FW");
            postalCode.Add("FDDFB", "8010");
            postalCode.Add("EBGAE", "05442-030");
            postalCode.Add("GADBB", "28034");
            postalCode.Add("BGGEF", "59000");
            postalCode.Add("AEEGB", "S-844 67");
            postalCode.Add("DEAFA", "80805");
            postalCode.Add("DEAEG", "44000");
            postalCode.Add("DDBGA", "10100");
            postalCode.Add("BEAAB", "1675");
            postalCode.Add("GEEAE", "8022");
            postalCode.Add("ECECF", "41101");
            postalCode.Add("AGDEE", "04876-786");
            postalCode.Add("GAAAB", "97403");
            postalCode.Add("CAGBE", "1081");
            postalCode.Add("EEAED", "05454-876");
            postalCode.Add("DEDFB", "5022");
            postalCode.Add("GGABD", "97827");
            postalCode.Add("AGFGF", "34564");
            postalCode.Add("EAEBA", "LA9 PX8");
            postalCode.Add("BDAFF", "14776");
            postalCode.Add("GFDCG", "78000");
            postalCode.Add("GCAFB", "31000");
            postalCode.Add("DCFCF", "V3F 2K1");
            postalCode.Add("AGGEA", "99362");
            postalCode.Add("CCGDA", "60528");
            postalCode.Add("EAFDC", "94117");
            postalCode.Add("GDFGC", "3508");
            postalCode.Add("ADEBD", "4980");
            postalCode.Add("FGGEB", "97219");
            postalCode.Add("BBBDA", "24100");
            postalCode.Add("BEDEC", "B-1180");

            country.Add("ABBCB", "Germany");
            country.Add("DDEAD", "Mexico");
            country.Add("GBGGG", "Mexico");
            country.Add("AFCEC", "UK");
            country.Add("AEBAG", "Sweden");
            country.Add("GGEBG", "Germany");
            country.Add("BBEDB", "France");
            country.Add("DCFBB", "Spain");
            country.Add("EGEAG", "France");
            country.Add("BDDDD", "Canada");
            country.Add("GBGFC", "UK");
            country.Add("CGADB", "Argentina");
            country.Add("DFCAB", "Mexico");
            country.Add("FDCED", "Switzerland");
            country.Add("FGACE", "Brazil");
            country.Add("AAAAF", "UK");
            country.Add("CDAFA", "Germany");
            country.Add("ABGBG", "France");
            country.Add("DBCAF", "UK");
            country.Add("FDDFB", "Austria");
            country.Add("EBGAE", "Brazil");
            country.Add("GADBB", "Spain");
            country.Add("BGGEF", "France");
            country.Add("AEEGB", "Sweden");
            country.Add("DEAFA", "Germany");
            country.Add("DEAEG", "France");
            country.Add("DDBGA", "Italy");
            country.Add("BEAAB", "Portugal");
            country.Add("GEEAE", "Spain");
            country.Add("ECECF", "Spain");
            country.Add("AGDEE", "Brazil");
            country.Add("GAAAB", "USA");
            country.Add("CAGBE", "Venezuela");
            country.Add("EEAED", "Brazil");
            country.Add("DEDFB", "Venezuela");
            country.Add("GGABD", "USA");
            country.Add("AGFGF", "Ireland");
            country.Add("EAEBA", "UK");
            country.Add("BDAFF", "Germany");
            country.Add("GFDCG", "France");
            country.Add("GCAFB", "France");
            country.Add("DCFCF", "Canada");
            country.Add("AGGEA", "USA");
            country.Add("CCGDA", "Germany");
            country.Add("EAFDC", "USA");
            country.Add("GDFGC", "Venezuela");
            country.Add("ADEBD", "Venezuela");
            country.Add("FGGEB", "USA");
            country.Add("BBBDA", "Italy");
            country.Add("BEDEC", "Belgium");


        }

        private Customers GetCustomers(int i)
        {
            var id = customersID[r.Next(customersID.Count() - 1)];
            return new Customers()
            {
                CustomerID = id,
                CompanyName = companyName[id],
                ContactName = contactName[id],
                ContactTitle = contactTitle[id],
                Address = address[id],
                City = city[id],
                Country = country[id],
                PostalCode = postalCode[id],
                Orders = GetComplexPropertiesOrderInfo(),
                Products = GetComplexPropertiesProduct()

            };
        }

        private OrderInfo GetComplexPropertiesOrderInfo()
        {
            OrderInfo ord = new OrderInfo();
            if (discount > 25)
                discount = 5;
            ord.Discount = discount + 3;
            ord.Expense = (discount * discount) + 100;
            ord.Quantity = discount + 65;
            discount++;
            return ord;
        }

        private ProductInfo[] GetComplexPropertiesProduct()
        {
            ProductInfo[] prodInfo = new ProductInfo[1];

            prodInfo[0] = new ProductInfo();

            prodInfo[0].ShippingDays = discount % 2 == 0 ? discount + 32 : discount + 39;
            prodInfo[0].ProductID = discount + 26;

            return prodInfo;
        }

        string[] customersID = new string[]
        {
            "ABBCB",
            "DDEAD",
            "GBGGG",
            "AFCEC",
            "AEBAG",
            "GGEBG",
            "BBEDB",
            "DCFBB",
            "EGEAG",
            "BDDDD",
            "GBGFC",
            "CGADB",
            "DFCAB",
            "FDCED",
            "FGACE",
            "AAAAF",
            "CDAFA",
            "ABGBG",
            "DBCAF",
            "FDDFB",
            "EBGAE",
            "GADBB",
            "BGGEF",
            "AEEGB",
            "DEAFA",
            "DEAEG",
            "DDBGA",
            "BEAAB",
            "GEEAE",
            "ECECF",
            "AGDEE",
            "GAAAB",
            "CAGBE",
            "EEAED",
            "DEDFB",
            "GGABD",
            "AGFGF",
            "EAEBA",
            "BDAFF",
            "GFDCG",
            "GCAFB",
            "DCFCF",
            "AGGEA",
            "CCGDA",
            "EAFDC",
            "GDFGC",
            "ADEBD",
            "FGGEB",
            "BBBDA",
            "BEDEC"

        };
    }
}
