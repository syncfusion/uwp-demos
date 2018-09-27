using System;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.UI.Xaml.Reports;
using System.Collections;
using System.IO;
using System.Reflection;

namespace Syncfusion.SampleBrowser.UWP.ReportViewer
{
    #region ReportViewerSampleHelper
    public abstract class ReportViewerSampleHelper
    {
        #region IReportViewerSampleHelper Members

        public virtual void SetParameter() { }

        public virtual void UpdateDataSet() { }

        public virtual void LoadReport()
        {
            try
            {
                Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
                Stream reportStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.ReportViewer.ReportViewer." + this.FolderPath + "." + this.ReportName + ".rdlc");
                this.ReportViewer.ProcessingMode = ProcessingMode.Local;
                //this.ReportViewer.ReportServiceURL = @"http://ssrs.syncfusion.com/ReportingService/ReportingService.svc";
                this.ReportViewer.LoadReport(reportStream);
                this.ReportViewer.RefreshReport();
            }
            catch
            { }
        }

        public virtual Stream GetReportStream()
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream reportStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.ReportViewer.ReportViewer." + this.FolderPath + "." + this.ReportName + ".rdlc");
            return reportStream;
        }

        #endregion

        #region IReportViewerSampleHelper Members

        public SfReportViewer ReportViewer
        {
            get;
            set;
        }

        public string ReportName
        {
            get;
            set;
        }

        public string FolderPath
        {
            get;
            set;
        }

        #endregion
    }

    #endregion

#if !UWP_STORE

    #region ProductShowCase

    public class TableFormatting : ReportViewerSampleHelper
    {
        public TableFormatting(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ProductShowcase";
            this.ReportName = "Table Formatting";
        }

        public override void UpdateDataSet()
        {
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "CustomerDetail", Value = CustomerDetails.GetData() });
        }

        public class CustomerDetails
        {
            public string CustomerID { get; set; }
            public string CompanyName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }

            public static IList GetData()
            {
                List<CustomerDetails> CustomerDetailsCollection = new List<CustomerDetails>();
                CustomerDetails CustomerDetail = null;
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "ALFKI",
                    CompanyName = "Alfreds Futterkiste",
                    Address = "Obere Str. 57",
                    City = "Berlin",
                    PostalCode = "12209",
                    Country = "Germany"
                };

                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "ANATR",
                    CompanyName = "Ana Trujillo Emparedados y helados",
                    Address = "Avda. de la Constitución 2222",
                    City = "México D.F.",
                    PostalCode = "05021",
                    Country = "Mexico"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "ANTON",
                    CompanyName = "Antonio Moreno Taquería",
                    Address = "Mataderos  2312",
                    City = "México D.F.",
                    PostalCode = "05023",
                    Country = "Mexico"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "AROUT",
                    CompanyName = "Around the Horn",
                    Address = "120 Hanover Sq.",
                    City = "London",
                    PostalCode = "WA1 1DP",
                    Country = "UK"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "BERGS",
                    CompanyName = "Berglunds snabbköp",
                    Address = "Berguvsvägen  8",
                    City = "Luleå",
                    PostalCode = "S-958 22",
                    Country = "Sweden"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "BLAUS",
                    CompanyName = "Blauer See Delikatessen",
                    Address = "Forsterstr. 57",
                    City = "Mannheim",
                    PostalCode = "68306",
                    Country = "Germany"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "BLONP",
                    CompanyName = "Blondel père et fils",
                    Address = "24, place Kléber",
                    City = "Strasbourg",
                    PostalCode = "67000",
                    Country = "France"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "BOLID",
                    CompanyName = "Bólido Comidas preparadas",
                    Address = "C/ Araquil, 67",
                    City = "Madrid",
                    PostalCode = "28023",
                    Country = "Spain"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "BONAP",
                    CompanyName = "Bon app'",
                    Address = "12, rue des Bouchers",
                    City = "Marseille",
                    PostalCode = "13008",
                    Country = "France"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "BOTTM",
                    CompanyName = "Bottom-Dollar Markets",
                    Address = "23 Tsawassen Blvd.",
                    City = "Tsawassen",
                    PostalCode = "T2F 8M4",
                    Country = "Canada"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "BSBEV",
                    CompanyName = "B's Beverages",
                    Address = "Fauntleroy Circus",
                    City = "London",
                    PostalCode = "EC2 5NT",
                    Country = "UK"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "CACTU",
                    CompanyName = "Cactus Comidas para llevar",
                    Address = "Cerrito 333",
                    City = "Buenos Aires",
                    PostalCode = "1010",
                    Country = "Argentina"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "CENTC",
                    CompanyName = "Centro comercial Moctezuma",
                    Address = "Sierras de Granada 9993",
                    City = "México D.F.",
                    PostalCode = "05022",
                    Country = "Mexico"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "CHOPS",
                    CompanyName = "Chop-suey Chinese",
                    Address = "Hauptstr. 29",
                    City = "Bern",
                    PostalCode = "3012",
                    Country = "Switzerland"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "COMMI",
                    CompanyName = "Comércio Mineiro",
                    Address = "Av. dos Lusíadas, 23",
                    City = "São Paulo",
                    PostalCode = "05432-043",
                    Country = "Brazil"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "CONSH",
                    CompanyName = "Consolidated Holdings",
                    Address = "Berkeley Gardens 12  Brewery ",
                    City = "London",
                    PostalCode = "WX1 6LT",
                    Country = "UK"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "DRACD",
                    CompanyName = "Drachenblut Delikatessen",
                    Address = "Walserweg 21",
                    City = "Aachen",
                    PostalCode = "52066",
                    Country = "Germany"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "DUMON",
                    CompanyName = "Du monde entier",
                    Address = "67, rue des Cinquante Otages",
                    City = "Nantes",
                    PostalCode = "44000",
                    Country = "France"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "EASTC",
                    CompanyName = "Eastern Connection",
                    Address = "35 King George",
                    City = "London",
                    PostalCode = "WX3 6FW",
                    Country = "UK"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "ERNSH",
                    CompanyName = "Ernst Handel",
                    Address = "Kirchgasse 6",
                    City = "Graz",
                    PostalCode = "8010",
                    Country = "Austria"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "FAMIA",
                    CompanyName = "Familia Arquibaldo",
                    Address = "Rua Orós, 92",
                    City = "São Paulo",
                    PostalCode = "05442-030",
                    Country = "Brazil"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "FISSA",
                    CompanyName = "FISSA Fabrica Inter. Salchichas S.A.",
                    Address = "C/ Moralzarzal, 86",
                    City = "Madrid",
                    PostalCode = "28034",
                    Country = "Spain"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "FOLIG",
                    CompanyName = "Folies gourmandes",
                    Address = "184, chaussée de Tournai",
                    City = "Lille",
                    PostalCode = "59000",
                    Country = "France"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "FOLKO",
                    CompanyName = "Folk och fä HB",
                    Address = "Åkergatan 24",
                    City = "Bräcke",
                    PostalCode = "S-844 67",
                    Country = "Sweden"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "FRANK",
                    CompanyName = "Frankenversand",
                    Address = "Berliner Platz 43",
                    City = "München",
                    PostalCode = "80805",
                    Country = "Germany"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "FRANR",
                    CompanyName = "France restauration",
                    Address = "54, rue Royale",
                    City = "Nantes",
                    PostalCode = "44000",
                    Country = "France"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "FRANS",
                    CompanyName = "Franchi S.p.A.",
                    Address = "Via Monte Bianco 34",
                    City = "Torino",
                    PostalCode = "10100",
                    Country = "Italy"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "FURIB",
                    CompanyName = "Furia Bacalhau e Frutos do Mar",
                    Address = "Jardim das rosas n. 32",
                    City = "Lisboa",
                    PostalCode = "1675",
                    Country = "Portugal"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "GALED",
                    CompanyName = "Galería del gastrónomo",
                    Address = "Rambla de Cataluña, 23",
                    City = "Barcelona",
                    PostalCode = "08022",
                    Country = "Spain"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "GODOS",
                    CompanyName = "Godos Cocina Típica",
                    Address = "C/ Romero, 33",
                    City = "Sevilla",
                    PostalCode = "41101",
                    Country = "Spain"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "GOURL",
                    CompanyName = "Gourmet Lanchonetes",
                    Address = "Av. Brasil, 442",
                    City = "Campinas",
                    PostalCode = "04876-786",
                    Country = "Brazil"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "GREAL",
                    CompanyName = "Great Lakes Food Market",
                    Address = "2732 Baker Blvd.",
                    City = "Eugene",
                    PostalCode = "97403",
                    Country = "USA"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "GROSR",
                    CompanyName = "GROSELLA-Restaurante",
                    Address = "5ª Ave. Los Palos Grandes",
                    City = "Caracas",
                    PostalCode = "1081",
                    Country = "Venezuela"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "HANAR",
                    CompanyName = "Hanari Carnes",
                    Address = "Rua do Paço, 67",
                    City = "Rio de Janeiro",
                    PostalCode = "05454-876",
                    Country = "Brazil"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "HILAA",
                    CompanyName = "HILARIÓN-Abastos",
                    Address = "Carrera 22 con Ave. Carlos Soublette #8-35",
                    City = "San Cristóbal",
                    PostalCode = "5022",
                    Country = "Venezuela"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "HUNGC",
                    CompanyName = "Hungry Coyote Import Store",
                    Address = "City Center Plaza 516 Main St.",
                    City = "Elgin",
                    PostalCode = "97827",
                    Country = "USA"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "HUNGO",
                    CompanyName = "Hungry Owl All-Night Grocers",
                    Address = "8 Johnstown Road",
                    City = "Cork",
                    PostalCode = "",
                    Country = "Ireland"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "ISLAT",
                    CompanyName = "Island Trading",
                    Address = "Garden House Crowther Way",
                    City = "Cowes",
                    PostalCode = "PO31 7PJ",
                    Country = "UK"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "KOENE",
                    CompanyName = "Königlich Essen",
                    Address = "Maubelstr. 90",
                    City = "Brandenburg",
                    PostalCode = "14776",
                    Country = "Germany"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "LACOR",
                    CompanyName = "La corne d'abondance",
                    Address = "67, avenue de l'Europe",
                    City = "Versailles",
                    PostalCode = "78000",
                    Country = "France"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "LAMAI",
                    CompanyName = "La maison d'Asie",
                    Address = "1 rue Alsace-Lorraine",
                    City = "Toulouse",
                    PostalCode = "31000",
                    Country = "France"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "LAUGB",
                    CompanyName = "Laughing Bacchus Wine Cellars",
                    Address = "1900 Oak St.",
                    City = "Vancouver",
                    PostalCode = "V3F 2K1",
                    Country = "Canada"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "LAZYK",
                    CompanyName = "Lazy K Kountry Store",
                    Address = "12 Orchestra Terrace",
                    City = "Walla Walla",
                    PostalCode = "99362",
                    Country = "USA"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "LEHMS",
                    CompanyName = "Lehmanns Marktstand",
                    Address = "Magazinweg 7",
                    City = "Frankfurt a.M. ",
                    PostalCode = "60528",
                    Country = "Germany"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "LETSS",
                    CompanyName = "Let's Stop N Shop",
                    Address = "87 Polk St. Suite 5",
                    City = "San Francisco",
                    PostalCode = "94117",
                    Country = "USA"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "LILAS",
                    CompanyName = "LILA-Supermercado",
                    Address = "Carrera 52 con Ave. Bolívar #65-98 Llano Largo",
                    City = "Barquisimeto",
                    PostalCode = "3508",
                    Country = "Venezuela"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "LINOD",
                    CompanyName = "LINO-Delicateses",
                    Address = "Ave. 5 de Mayo Porlamar",
                    City = "I. de Margarita",
                    PostalCode = "4980",
                    Country = "Venezuela"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "LONEP",
                    CompanyName = "Lonesome Pine Restaurant",
                    Address = "89 Chiaroscuro Rd.",
                    City = "Portland",
                    PostalCode = "97219",
                    Country = "USA"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "MAGAA",
                    CompanyName = "Magazzini Alimentari Riuniti",
                    Address = "Via Ludovico il Moro 22",
                    City = "Bergamo",
                    PostalCode = "24100",
                    Country = "Italy"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "MAISD",
                    CompanyName = "Maison Dewey",
                    Address = "Rue Joseph-Bens 532",
                    City = "Bruxelles",
                    PostalCode = "B-1180",
                    Country = "Belgium"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "MEREP",
                    CompanyName = "Mère Paillarde",
                    Address = "43 rue St. Laurent",
                    City = "Montréal",
                    PostalCode = "H1J 1C3",
                    Country = "Canada"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "MORGK",
                    CompanyName = "Morgenstern Gesundkost",
                    Address = "Heerstr. 22",
                    City = "Leipzig",
                    PostalCode = "04179",
                    Country = "Germany"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "NORTS",
                    CompanyName = "North/South",
                    Address = "South House 300 Queensbridge",
                    City = "London",
                    PostalCode = "SW7 1RZ",
                    Country = "UK"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "OCEAN",
                    CompanyName = "Océano Atlántico Ltda.",
                    Address = "Ing. Gustavo Moncada 8585 Piso 20-A",
                    City = "Buenos Aires",
                    PostalCode = "1010",
                    Country = "Argentina"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "OLDWO",
                    CompanyName = "Old World Delicatessen",
                    Address = "2743 Bering St.",
                    City = "Anchorage",
                    PostalCode = "99508",
                    Country = "USA"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "OTTIK",
                    CompanyName = "Ottilies Käseladen",
                    Address = "Mehrheimerstr. 369",
                    City = "Köln",
                    PostalCode = "50739",
                    Country = "Germany"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "PARIS",
                    CompanyName = "Paris spécialités",
                    Address = "265, boulevard Charonne",
                    City = "Paris",
                    PostalCode = "75012",
                    Country = "France"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "PERIC",
                    CompanyName = "Pericles Comidas clásicas",
                    Address = "Calle Dr. Jorge Cash 321",
                    City = "México D.F.",
                    PostalCode = "05033",
                    Country = "Mexico"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "PICCO",
                    CompanyName = "Piccolo und mehr",
                    Address = "Geislweg 14",
                    City = "Salzburg",
                    PostalCode = "5020",
                    Country = "Austria"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "PRINI",
                    CompanyName = "Princesa Isabel Vinhos",
                    Address = "Estrada da saúde n. 58",
                    City = "Lisboa",
                    PostalCode = "1756",
                    Country = "Portugal"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "QUEDE",
                    CompanyName = "Que Delícia",
                    Address = "Rua da Panificadora, 12",
                    City = "Rio de Janeiro",
                    PostalCode = "02389-673",
                    Country = "Brazil"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "QUEEN",
                    CompanyName = "Queen Cozinha",
                    Address = "Alameda dos Canàrios, 891",
                    City = "São Paulo",
                    PostalCode = "05487-020",
                    Country = "Brazil"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "QUICK",
                    CompanyName = "QUICK-Stop",
                    Address = "Taucherstraße 10",
                    City = "Cunewalde",
                    PostalCode = "01307",
                    Country = "Germany"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "RANCH",
                    CompanyName = "Rancho grande",
                    Address = "Av. del Libertador 900",
                    City = "Buenos Aires",
                    PostalCode = "1010",
                    Country = "Argentina"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "RATTC",
                    CompanyName = "Rattlesnake Canyon Grocery",
                    Address = "2817 Milton Dr.",
                    City = "Albuquerque",
                    PostalCode = "87110",
                    Country = "USA"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "REGGC",
                    CompanyName = "Reggiani Caseifici",
                    Address = "Strada Provinciale 124",
                    City = "Reggio Emilia",
                    PostalCode = "42100",
                    Country = "Italy"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "RICAR",
                    CompanyName = "Ricardo Adocicados",
                    Address = "Av. Copacabana, 267",
                    City = "Rio de Janeiro",
                    PostalCode = "02389-890",
                    Country = "Brazil"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "RICSU",
                    CompanyName = "Richter Supermarkt",
                    Address = "Grenzacherweg 237",
                    City = "Genève",
                    PostalCode = "1203",
                    Country = "Switzerland"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "ROMEY",
                    CompanyName = "Romero y tomillo",
                    Address = "Gran Vía, 1",
                    City = "Madrid",
                    PostalCode = "28001",
                    Country = "Spain"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "SANTG",
                    CompanyName = "Santé Gourmet",
                    Address = "Erling Skakkes gate 78",
                    City = "Stavern",
                    PostalCode = "4110",
                    Country = "Norway"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "SAVEA",
                    CompanyName = "Save-a-lot Markets",
                    Address = "187 Suffolk Ln.",
                    City = "Boise",
                    PostalCode = "83720",
                    Country = "USA"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "SEVES",
                    CompanyName = "Seven Seas Imports",
                    Address = "90 Wadhurst Rd.",
                    City = "London",
                    PostalCode = "OX15 4NB",
                    Country = "UK"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "SIMOB",
                    CompanyName = "Simons bistro",
                    Address = "Vinbæltet 34",
                    City = "København",
                    PostalCode = "1734",
                    Country = "Denmark"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "SPECD",
                    CompanyName = "Spécialités du monde",
                    Address = "25, rue Lauriston",
                    City = "Paris",
                    PostalCode = "75016",
                    Country = "France"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "SPLIR",
                    CompanyName = "Split Rail Beer & Ale",
                    Address = "P.O. Box 555",
                    City = "Lander",
                    PostalCode = "82520",
                    Country = "USA"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "SUPRD",
                    CompanyName = "Suprêmes délices",
                    Address = "Boulevard Tirou, 255",
                    City = "Charleroi",
                    PostalCode = "B-6000",
                    Country = "Belgium"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "THEBI",
                    CompanyName = "The Big Cheese",
                    Address = "89 Jefferson Way Suite 2",
                    City = "Portland",
                    PostalCode = "97201",
                    Country = "USA"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "THECR",
                    CompanyName = "The Cracker Box",
                    Address = "55 Grizzly Peak Rd.",
                    City = "Butte",
                    PostalCode = "59801",
                    Country = "USA"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "TOMSP",
                    CompanyName = "Toms Spezialitäten",
                    Address = "Luisenstr. 48",
                    City = "Münster",
                    PostalCode = "44087",
                    Country = "Germany"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "TORTU",
                    CompanyName = "Tortuga Restaurante",
                    Address = "Avda. Azteca 123",
                    City = "México D.F.",
                    PostalCode = "05033",
                    Country = "Mexico"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "TRADH",
                    CompanyName = "Tradição Hipermercados",
                    Address = "Av. Inês de Castro, 414",
                    City = "São Paulo",
                    PostalCode = "05634-030",
                    Country = "Brazil"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "TRAIH",
                    CompanyName = "Trail's Head Gourmet Provisioners",
                    Address = "722 DaVinci Blvd.",
                    City = "Kirkland",
                    PostalCode = "98034",
                    Country = "USA"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "VAFFE",
                    CompanyName = "Vaffeljernet",
                    Address = "Smagsløget 45",
                    City = "Århus",
                    PostalCode = "8200",
                    Country = "Denmark"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "VICTE",
                    CompanyName = "Victuailles en stock",
                    Address = "2, rue du Commerce",
                    City = "Lyon",
                    PostalCode = "69004",
                    Country = "France"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "VINET",
                    CompanyName = "Vins et alcools Chevalier",
                    Address = "59 rue de l'Abbaye",
                    City = "Reims",
                    PostalCode = "51100",
                    Country = "France"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "WANDK",
                    CompanyName = "Die Wandernde Kuh",
                    Address = "Adenauerallee 900",
                    City = "Stuttgart",
                    PostalCode = "70563",
                    Country = "Germany"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "WARTH",
                    CompanyName = "Wartian Herkku",
                    Address = "Torikatu 38",
                    City = "Oulu",
                    PostalCode = "90110",
                    Country = "Finland"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "WELLI",
                    CompanyName = "Wellington Importadora",
                    Address = "Rua do Mercado, 12",
                    City = "Resende",
                    PostalCode = "08737-363",
                    Country = "Brazil"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "WHITC",
                    CompanyName = "White Clover Markets",
                    Address = "305 - 14th Ave. S. Suite 3B",
                    City = "Seattle",
                    PostalCode = "98128",
                    Country = "USA"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "WILMK",
                    CompanyName = "Wilman Kala",
                    Address = "Keskuskatu 45",
                    City = "Helsinki",
                    PostalCode = "21240",
                    Country = "Finland"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                CustomerDetail = new CustomerDetails()
                {
                    CustomerID = "WOLZA",
                    CompanyName = "Wolski  Zajazd",
                    Address = "ul. Filtrowa 68",
                    City = "Warszawa",
                    PostalCode = "01-012",
                    Country = "Poland"
                };
                CustomerDetailsCollection.Add(CustomerDetail);
                return CustomerDetailsCollection;
            }
        }
    }

    public class ProductCatalogDemo : ReportViewerSampleHelper
    {
        public ProductCatalogDemo(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ProductShowcase";
            this.ReportName = "Product Catalog";
        }

        public override void UpdateDataSet()
        {
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "ProductCatalog", Value = ProductCatalog.GetData() });
        }

        public class ProductCatalog
        {
            public string ProdSubCat { get; set; }
            public string ProdModel { get; set; }
            public string ProdCat { get; set; }
            public string Description { get; set; }
            public string ProdName { get; set; }
            public string ProductNumber { get; set; }
            public string Color { get; set; }
            public string Size { get; set; }
            public double? Weight { get; set; }
            public double? StandardCost { get; set; }
            public string Style { get; set; }
            public string Class { get; set; }
            public double? ListPrice { get; set; }
            public static IList GetData()
            {
                List<ProductCatalog> datas = new List<ProductCatalog>();
                ProductCatalog data = null;
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "HL Road Frame",
                    ProdCat = "Components",
                    Description = "Our lightest and best quality aluminum frame made from the newest alloy; it is welded and heat-treated for strength. Our innovative design results in maximum comfort and performance.",
                    ProdName = "HL Road Frame - Black, 58",
                    ProductNumber = "FR-R92B-58",
                    Color = "Black",
                    Size = "58",
                    Weight = 2.24,
                    StandardCost = 1059.3100,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1431.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "HL Road Frame",
                    ProdCat = "Components",
                    Description = "Our lightest and best quality aluminum frame made from the newest alloy; it is welded and heat-treated for strength. Our innovative design results in maximum comfort and performance.",
                    ProdName = "HL Road Frame - Red, 58",
                    ProductNumber = "FR-R92R-58",
                    Color = "Red",
                    Size = "58",
                    Weight = 2.24,
                    StandardCost = 1059.3100,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1431.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Helmets",
                    ProdModel = "Sport-100",
                    ProdCat = "Accessories",
                    Description = "Universal fit, well-vented, lightweight , snap-on visor.",
                    ProdName = "Sport-100 Helmet, Red",
                    ProductNumber = "HL-U509-R",
                    Color = "Red",
                    Size = "",
                    Weight = null,
                    StandardCost = 13.0863,
                    Style = "",
                    Class = "",
                    ListPrice = 34.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Helmets",
                    ProdModel = "Sport-100",
                    ProdCat = "Accessories",
                    Description = "Universal fit, well-vented, lightweight , snap-on visor.",
                    ProdName = "Sport-100 Helmet, Black",
                    ProductNumber = "HL-U509",
                    Color = "Black",
                    Size = "",
                    Weight = null,
                    StandardCost = 13.0863,
                    Style = "",
                    Class = "",
                    ListPrice = 34.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Socks",
                    ProdModel = "Mountain Bike Socks",
                    ProdCat = "Clothing",
                    Description = "Combination of natural and synthetic fibers stays dry and provides just the right cushioning.",
                    ProdName = "Mountain Bike Socks, M",
                    ProductNumber = "SO-B909-M",
                    Color = "White",
                    Size = "M",
                    Weight = null,
                    StandardCost = 3.3963,
                    Style = "U ",
                    Class = "",
                    ListPrice = 9.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Socks",
                    ProdModel = "Mountain Bike Socks",
                    ProdCat = "Clothing",
                    Description = "Combination of natural and synthetic fibers stays dry and provides just the right cushioning.",
                    ProdName = "Mountain Bike Socks, L",
                    ProductNumber = "SO-B909-L",
                    Color = "White",
                    Size = "L",
                    Weight = null,
                    StandardCost = 3.3963,
                    Style = "U ",
                    Class = "",
                    ListPrice = 9.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Helmets",
                    ProdModel = "Sport-100",
                    ProdCat = "Accessories",
                    Description = "Universal fit, well-vented, lightweight , snap-on visor.",
                    ProdName = "Sport-100 Helmet, Blue",
                    ProductNumber = "HL-U509-B",
                    Color = "Blue",
                    Size = "",
                    Weight = null,
                    StandardCost = 13.0863,
                    Style = "",
                    Class = "",
                    ListPrice = 34.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Caps",
                    ProdModel = "Cycling Cap",
                    ProdCat = "Clothing",
                    Description = "Traditional style with a flip-up brim; one-size fits all.",
                    ProdName = "AWC Logo Cap",
                    ProductNumber = "CA-1098",
                    Color = "Multi",
                    Size = "",
                    Weight = null,
                    StandardCost = 6.9223,
                    Style = "U ",
                    Class = "",
                    ListPrice = 8.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Jerseys",
                    ProdModel = "Long-Sleeve Logo Jersey",
                    ProdCat = "Clothing",
                    Description = "Unisex long-sleeve AWC logo microfiber cycling jersey",
                    ProdName = "Long-Sleeve Logo Jersey, S",
                    ProductNumber = "LJ-0192-S",
                    Color = "Multi",
                    Size = "S",
                    Weight = null,
                    StandardCost = 38.4923,
                    Style = "U ",
                    Class = "",
                    ListPrice = 49.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Jerseys",
                    ProdModel = "Long-Sleeve Logo Jersey",
                    ProdCat = "Clothing",
                    Description = "Unisex long-sleeve AWC logo microfiber cycling jersey",
                    ProdName = "Long-Sleeve Logo Jersey, M",
                    ProductNumber = "LJ-0192-M",
                    Color = "Multi",
                    Size = "M",
                    Weight = null,
                    StandardCost = 38.4923,
                    Style = "U ",
                    Class = "",
                    ListPrice = 49.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Jerseys",
                    ProdModel = "Long-Sleeve Logo Jersey",
                    ProdCat = "Clothing",
                    Description = "Unisex long-sleeve AWC logo microfiber cycling jersey",
                    ProdName = "Long-Sleeve Logo Jersey, L",
                    ProductNumber = "LJ-0192-L",
                    Color = "Multi",
                    Size = "L",
                    Weight = null,
                    StandardCost = 38.4923,
                    Style = "U ",
                    Class = "",
                    ListPrice = 49.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Jerseys",
                    ProdModel = "Long-Sleeve Logo Jersey",
                    ProdCat = "Clothing",
                    Description = "Unisex long-sleeve AWC logo microfiber cycling jersey",
                    ProdName = "Long-Sleeve Logo Jersey, XL",
                    ProductNumber = "LJ-0192-X",
                    Color = "Multi",
                    Size = "XL",
                    Weight = null,
                    StandardCost = 38.4923,
                    Style = "U ",
                    Class = "",
                    ListPrice = 49.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "HL Road Frame",
                    ProdCat = "Components",
                    Description = "Our lightest and best quality aluminum frame made from the newest alloy; it is welded and heat-treated for strength. Our innovative design results in maximum comfort and performance.",
                    ProdName = "HL Road Frame - Red, 62",
                    ProductNumber = "FR-R92R-62",
                    Color = "Red",
                    Size = "62",
                    Weight = 2.30,
                    StandardCost = 868.6342,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1431.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "HL Road Frame",
                    ProdCat = "Components",
                    Description = "Our lightest and best quality aluminum frame made from the newest alloy; it is welded and heat-treated for strength. Our innovative design results in maximum comfort and performance.",
                    ProdName = "HL Road Frame - Red, 44",
                    ProductNumber = "FR-R92R-44",
                    Color = "Red",
                    Size = "44",
                    Weight = 2.12,
                    StandardCost = 868.6342,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1431.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "HL Road Frame",
                    ProdCat = "Components",
                    Description = "Our lightest and best quality aluminum frame made from the newest alloy; it is welded and heat-treated for strength. Our innovative design results in maximum comfort and performance.",
                    ProdName = "HL Road Frame - Red, 48",
                    ProductNumber = "FR-R92R-48",
                    Color = "Red",
                    Size = "48",
                    Weight = 2.16,
                    StandardCost = 868.6342,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1431.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "HL Road Frame",
                    ProdCat = "Components",
                    Description = "Our lightest and best quality aluminum frame made from the newest alloy; it is welded and heat-treated for strength. Our innovative design results in maximum comfort and performance.",
                    ProdName = "HL Road Frame - Red, 52",
                    ProductNumber = "FR-R92R-52",
                    Color = "Red",
                    Size = "52",
                    Weight = 2.20,
                    StandardCost = 868.6342,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1431.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "HL Road Frame",
                    ProdCat = "Components",
                    Description = "Our lightest and best quality aluminum frame made from the newest alloy; it is welded and heat-treated for strength. Our innovative design results in maximum comfort and performance.",
                    ProdName = "HL Road Frame - Red, 56",
                    ProductNumber = "FR-R92R-56",
                    Color = "Red",
                    Size = "56",
                    Weight = 2.24,
                    StandardCost = 868.6342,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1431.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "LL Road Frame",
                    ProdCat = "Components",
                    Description = "The LL Frame provides a safe comfortable ride, while offering superior bump absorption in a value-priced aluminum frame.",
                    ProdName = "LL Road Frame - Black, 58",
                    ProductNumber = "FR-R38B-58",
                    Color = "Black",
                    Size = "58",
                    Weight = 2.46,
                    StandardCost = 204.6251,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 337.2200
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "LL Road Frame",
                    ProdCat = "Components",
                    Description = "The LL Frame provides a safe comfortable ride, while offering superior bump absorption in a value-priced aluminum frame.",
                    ProdName = "LL Road Frame - Black, 60",
                    ProductNumber = "FR-R38B-60",
                    Color = "Black",
                    Size = "60",
                    Weight = 2.48,
                    StandardCost = 204.6251,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 337.2200
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "LL Road Frame",
                    ProdCat = "Components",
                    Description = "The LL Frame provides a safe comfortable ride, while offering superior bump absorption in a value-priced aluminum frame.",
                    ProdName = "LL Road Frame - Black, 62",
                    ProductNumber = "FR-R38B-62",
                    Color = "Black",
                    Size = "62",
                    Weight = 2.50,
                    StandardCost = 204.6251,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 337.2200
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "LL Road Frame",
                    ProdCat = "Components",
                    Description = "The LL Frame provides a safe comfortable ride, while offering superior bump absorption in a value-priced aluminum frame.",
                    ProdName = "LL Road Frame - Red, 44",
                    ProductNumber = "FR-R38R-44",
                    Color = "Red",
                    Size = "44",
                    Weight = 2.32,
                    StandardCost = 187.1571,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 337.2200
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "LL Road Frame",
                    ProdCat = "Components",
                    Description = "The LL Frame provides a safe comfortable ride, while offering superior bump absorption in a value-priced aluminum frame.",
                    ProdName = "LL Road Frame - Red, 48",
                    ProductNumber = "FR-R38R-48",
                    Color = "Red",
                    Size = "48",
                    Weight = 2.36,
                    StandardCost = 187.1571,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 337.2200
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "LL Road Frame",
                    ProdCat = "Components",
                    Description = "The LL Frame provides a safe comfortable ride, while offering superior bump absorption in a value-priced aluminum frame.",
                    ProdName = "LL Road Frame - Red, 52",
                    ProductNumber = "FR-R38R-52",
                    Color = "Red",
                    Size = "52",
                    Weight = 2.40,
                    StandardCost = 187.1571,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 337.2200
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "LL Road Frame",
                    ProdCat = "Components",
                    Description = "The LL Frame provides a safe comfortable ride, while offering superior bump absorption in a value-priced aluminum frame.",
                    ProdName = "LL Road Frame - Red, 58",
                    ProductNumber = "FR-R38R-58",
                    Color = "Red",
                    Size = "58",
                    Weight = 2.46,
                    StandardCost = 187.1571,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 337.2200
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "LL Road Frame",
                    ProdCat = "Components",
                    Description = "The LL Frame provides a safe comfortable ride, while offering superior bump absorption in a value-priced aluminum frame.",
                    ProdName = "LL Road Frame - Red, 60",
                    ProductNumber = "FR-R38R-60",
                    Color = "Red",
                    Size = "60",
                    Weight = 2.48,
                    StandardCost = 187.1571,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 337.2200
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "LL Road Frame",
                    ProdCat = "Components",
                    Description = "The LL Frame provides a safe comfortable ride, while offering superior bump absorption in a value-priced aluminum frame.",
                    ProdName = "LL Road Frame - Red, 62",
                    ProductNumber = "FR-R38R-62",
                    Color = "Red",
                    Size = "62",
                    Weight = 2.50,
                    StandardCost = 187.1571,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 337.2200
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "ML Road Frame",
                    ProdCat = "Components",
                    Description = "Made from the same aluminum alloy as our top-of-the line HL frame, the ML features a lightweight down-tube milled to the perfect diameter for optimal strength. Men's version.",
                    ProdName = "ML Road Frame - Red, 44",
                    ProductNumber = "FR-R72R-44",
                    Color = "Red",
                    Size = "44",
                    Weight = 2.22,
                    StandardCost = 352.1394,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 594.8300
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "ML Road Frame",
                    ProdCat = "Components",
                    Description = "Made from the same aluminum alloy as our top-of-the line HL frame, the ML features a lightweight down-tube milled to the perfect diameter for optimal strength. Men's version.",
                    ProdName = "ML Road Frame - Red, 48",
                    ProductNumber = "FR-R72R-48",
                    Color = "Red",
                    Size = "48",
                    Weight = 2.26,
                    StandardCost = 352.1394,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 594.8300
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "ML Road Frame",
                    ProdCat = "Components",
                    Description = "Made from the same aluminum alloy as our top-of-the line HL frame, the ML features a lightweight down-tube milled to the perfect diameter for optimal strength. Men's version.",
                    ProdName = "ML Road Frame - Red, 52",
                    ProductNumber = "FR-R72R-52",
                    Color = "Red",
                    Size = "52",
                    Weight = 2.30,
                    StandardCost = 352.1394,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 594.8300
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "ML Road Frame",
                    ProdCat = "Components",
                    Description = "Made from the same aluminum alloy as our top-of-the line HL frame, the ML features a lightweight down-tube milled to the perfect diameter for optimal strength. Men's version.",
                    ProdName = "ML Road Frame - Red, 58",
                    ProductNumber = "FR-R72R-58",
                    Color = "Red",
                    Size = "58",
                    Weight = 2.36,
                    StandardCost = 352.1394,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 594.8300
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "ML Road Frame",
                    ProdCat = "Components",
                    Description = "Made from the same aluminum alloy as our top-of-the line HL frame, the ML features a lightweight down-tube milled to the perfect diameter for optimal strength. Men's version.",
                    ProdName = "ML Road Frame - Red, 60",
                    ProductNumber = "FR-R72R-60",
                    Color = "Red",
                    Size = "60",
                    Weight = 2.38,
                    StandardCost = 352.1394,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 594.8300
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "LL Road Frame",
                    ProdCat = "Components",
                    Description = "The LL Frame provides a safe comfortable ride, while offering superior bump absorption in a value-priced aluminum frame.",
                    ProdName = "LL Road Frame - Black, 44",
                    ProductNumber = "FR-R38B-44",
                    Color = "Black",
                    Size = "44",
                    Weight = 2.32,
                    StandardCost = 204.6251,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 337.2200
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "LL Road Frame",
                    ProdCat = "Components",
                    Description = "The LL Frame provides a safe comfortable ride, while offering superior bump absorption in a value-priced aluminum frame.",
                    ProdName = "LL Road Frame - Black, 48",
                    ProductNumber = "FR-R38B-48",
                    Color = "Black",
                    Size = "48",
                    Weight = 2.36,
                    StandardCost = 204.6251,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 337.2200
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Frames",
                    ProdModel = "LL Road Frame",
                    ProdCat = "Components",
                    Description = "The LL Frame provides a safe comfortable ride, while offering superior bump absorption in a value-priced aluminum frame.",
                    ProdName = "LL Road Frame - Black, 52",
                    ProductNumber = "FR-R38B-52",
                    Color = "Black",
                    Size = "52",
                    Weight = 2.40,
                    StandardCost = 204.6251,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 337.2200
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Frames",
                    ProdModel = "HL Mountain Frame",
                    ProdCat = "Components",
                    Description = "Each frame is hand-crafted in our Bothell facility to the optimum diameter and wall-thickness required of a premium mountain frame. The heat-treated welded aluminum frame has a larger diameter tube that absorbs the bumps.",
                    ProdName = "HL Mountain Frame - Silver, 42",
                    ProductNumber = "FR-M94S-42",
                    Color = "Silver",
                    Size = "42",
                    Weight = 2.72,
                    StandardCost = 747.2002,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1364.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Frames",
                    ProdModel = "HL Mountain Frame",
                    ProdCat = "Components",
                    Description = "Each frame is hand-crafted in our Bothell facility to the optimum diameter and wall-thickness required of a premium mountain frame. The heat-treated welded aluminum frame has a larger diameter tube that absorbs the bumps.",
                    ProdName = "HL Mountain Frame - Silver, 44",
                    ProductNumber = "FR-M94S-44",
                    Color = "Silver",
                    Size = "44",
                    Weight = 2.76,
                    StandardCost = 706.8110,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1364.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Frames",
                    ProdModel = "HL Mountain Frame",
                    ProdCat = "Components",
                    Description = "Each frame is hand-crafted in our Bothell facility to the optimum diameter and wall-thickness required of a premium mountain frame. The heat-treated welded aluminum frame has a larger diameter tube that absorbs the bumps.",
                    ProdName = "HL Mountain Frame - Silver, 48",
                    ProductNumber = "FR-M94S-52",
                    Color = "Silver",
                    Size = "48",
                    Weight = 2.80,
                    StandardCost = 706.8110,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1364.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Frames",
                    ProdModel = "HL Mountain Frame",
                    ProdCat = "Components",
                    Description = "Each frame is hand-crafted in our Bothell facility to the optimum diameter and wall-thickness required of a premium mountain frame. The heat-treated welded aluminum frame has a larger diameter tube that absorbs the bumps.",
                    ProdName = "HL Mountain Frame - Silver, 46",
                    ProductNumber = "FR-M94S-46",
                    Color = "Silver",
                    Size = "46",
                    Weight = 2.84,
                    StandardCost = 747.2002,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1364.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Frames",
                    ProdModel = "HL Mountain Frame",
                    ProdCat = "Components",
                    Description = "Each frame is hand-crafted in our Bothell facility to the optimum diameter and wall-thickness required of a premium mountain frame. The heat-treated welded aluminum frame has a larger diameter tube that absorbs the bumps.",
                    ProdName = "HL Mountain Frame - Black, 42",
                    ProductNumber = "FR-M94B-42",
                    Color = "Black",
                    Size = "42",
                    Weight = 2.72,
                    StandardCost = 739.0410,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1349.6000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Frames",
                    ProdModel = "HL Mountain Frame",
                    ProdCat = "Components",
                    Description = "Each frame is hand-crafted in our Bothell facility to the optimum diameter and wall-thickness required of a premium mountain frame. The heat-treated welded aluminum frame has a larger diameter tube that absorbs the bumps.",
                    ProdName = "HL Mountain Frame - Black, 44",
                    ProductNumber = "FR-M94B-44",
                    Color = "Black",
                    Size = "44",
                    Weight = 2.76,
                    StandardCost = 699.0928,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1349.6000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Frames",
                    ProdModel = "HL Mountain Frame",
                    ProdCat = "Components",
                    Description = "Each frame is hand-crafted in our Bothell facility to the optimum diameter and wall-thickness required of a premium mountain frame. The heat-treated welded aluminum frame has a larger diameter tube that absorbs the bumps.",
                    ProdName = "HL Mountain Frame - Black, 48",
                    ProductNumber = "FR-M94B-48",
                    Color = "Black",
                    Size = "48",
                    Weight = 2.80,
                    StandardCost = 699.0928,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1349.6000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Frames",
                    ProdModel = "HL Mountain Frame",
                    ProdCat = "Components",
                    Description = "Each frame is hand-crafted in our Bothell facility to the optimum diameter and wall-thickness required of a premium mountain frame. The heat-treated welded aluminum frame has a larger diameter tube that absorbs the bumps.",
                    ProdName = "HL Mountain Frame - Black, 46",
                    ProductNumber = "FR-M94B-46",
                    Color = "Black",
                    Size = "46",
                    Weight = 2.84,
                    StandardCost = 739.0410,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1349.6000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Frames",
                    ProdModel = "HL Mountain Frame",
                    ProdCat = "Components",
                    Description = "Each frame is hand-crafted in our Bothell facility to the optimum diameter and wall-thickness required of a premium mountain frame. The heat-treated welded aluminum frame has a larger diameter tube that absorbs the bumps.",
                    ProdName = "HL Mountain Frame - Black, 38",
                    ProductNumber = "FR-M94B-38",
                    Color = "Black",
                    Size = "38",
                    Weight = 2.68,
                    StandardCost = 739.0410,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1349.6000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Frames",
                    ProdModel = "HL Mountain Frame",
                    ProdCat = "Components",
                    Description = "Each frame is hand-crafted in our Bothell facility to the optimum diameter and wall-thickness required of a premium mountain frame. The heat-treated welded aluminum frame has a larger diameter tube that absorbs the bumps.",
                    ProdName = "HL Mountain Frame - Silver, 38",
                    ProductNumber = "FR-M94S-38",
                    Color = "Silver",
                    Size = "38",
                    Weight = 2.68,
                    StandardCost = 747.2002,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 1364.5000
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-150",
                    ProdCat = "Bikes",
                    Description = "This bike is ridden by race winners. Developed with the Adventure Works Cycles professional race team, it has a extremely light heat-treated aluminum frame, and steering that allows precision control.",
                    ProdName = "Road-150 Red, 62",
                    ProductNumber = "BK-R93R-62",
                    Color = "Red",
                    Size = "62",
                    Weight = 15.00,
                    StandardCost = 2171.2942,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 3578.2700
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-150",
                    ProdCat = "Bikes",
                    Description = "This bike is ridden by race winners. Developed with the Adventure Works Cycles professional race team, it has a extremely light heat-treated aluminum frame, and steering that allows precision control.",
                    ProdName = "Road-150 Red, 44",
                    ProductNumber = "BK-R93R-44",
                    Color = "Red",
                    Size = "44",
                    Weight = 13.77,
                    StandardCost = 2171.2942,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 3578.2700
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-150",
                    ProdCat = "Bikes",
                    Description = "This bike is ridden by race winners. Developed with the Adventure Works Cycles professional race team, it has a extremely light heat-treated aluminum frame, and steering that allows precision control.",
                    ProdName = "Road-150 Red, 48",
                    ProductNumber = "BK-R93R-48",
                    Color = "Red",
                    Size = "48",
                    Weight = 14.13,
                    StandardCost = 2171.2942,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 3578.2700
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-150",
                    ProdCat = "Bikes",
                    Description = "This bike is ridden by race winners. Developed with the Adventure Works Cycles professional race team, it has a extremely light heat-treated aluminum frame, and steering that allows precision control.",
                    ProdName = "Road-150 Red, 52",
                    ProductNumber = "BK-R93R-52",
                    Color = "Red",
                    Size = "52",
                    Weight = 14.42,
                    StandardCost = 2171.2942,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 3578.2700
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-150",
                    ProdCat = "Bikes",
                    Description = "This bike is ridden by race winners. Developed with the Adventure Works Cycles professional race team, it has a extremely light heat-treated aluminum frame, and steering that allows precision control.",
                    ProdName = "Road-150 Red, 56",
                    ProductNumber = "BK-R93R-56",
                    Color = "Red",
                    Size = "56",
                    Weight = 14.68,
                    StandardCost = 2171.2942,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 3578.2700
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-450",
                    ProdCat = "Bikes",
                    Description = "A true multi-sport bike that offers streamlined riding and a revolutionary design. Aerodynamic design lets you ride with the pros, and the gearing will conquer hilly roads.",
                    ProdName = "Road-450 Red, 58",
                    ProductNumber = "BK-R68R-58",
                    Color = "Red",
                    Size = "58",
                    Weight = 17.79,
                    StandardCost = 884.7083,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 1457.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-450",
                    ProdCat = "Bikes",
                    Description = "A true multi-sport bike that offers streamlined riding and a revolutionary design. Aerodynamic design lets you ride with the pros, and the gearing will conquer hilly roads.",
                    ProdName = "Road-450 Red, 60",
                    ProductNumber = "BK-R68R-60",
                    Color = "Red",
                    Size = "60",
                    Weight = 17.90,
                    StandardCost = 884.7083,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 1457.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-450",
                    ProdCat = "Bikes",
                    Description = "A true multi-sport bike that offers streamlined riding and a revolutionary design. Aerodynamic design lets you ride with the pros, and the gearing will conquer hilly roads.",
                    ProdName = "Road-450 Red, 44",
                    ProductNumber = "BK-R68R-44",
                    Color = "Red",
                    Size = "44",
                    Weight = 16.77,
                    StandardCost = 884.7083,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 1457.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-450",
                    ProdCat = "Bikes",
                    Description = "A true multi-sport bike that offers streamlined riding and a revolutionary design. Aerodynamic design lets you ride with the pros, and the gearing will conquer hilly roads.",
                    ProdName = "Road-450 Red, 48",
                    ProductNumber = "BK-R68R-48",
                    Color = "Red",
                    Size = "48",
                    Weight = 17.13,
                    StandardCost = 884.7083,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 1457.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-450",
                    ProdCat = "Bikes",
                    Description = "A true multi-sport bike that offers streamlined riding and a revolutionary design. Aerodynamic design lets you ride with the pros, and the gearing will conquer hilly roads.",
                    ProdName = "Road-450 Red, 52",
                    ProductNumber = "BK-R68R-52",
                    Color = "Red",
                    Size = "52",
                    Weight = 17.42,
                    StandardCost = 884.7083,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 1457.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-650",
                    ProdCat = "Bikes",
                    Description = "Value-priced bike with many features of our top-of-the-line models. Has the same light, stiff frame, and the quick acceleration we're famous for.",
                    ProdName = "Road-650 Red, 58",
                    ProductNumber = "BK-R50R-58",
                    Color = "Red",
                    Size = "58",
                    Weight = 19.79,
                    StandardCost = 486.7066,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 782.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-650",
                    ProdCat = "Bikes",
                    Description = "Value-priced bike with many features of our top-of-the-line models. Has the same light, stiff frame, and the quick acceleration we're famous for.",
                    ProdName = "Road-650 Red, 60",
                    ProductNumber = "BK-R50R-60",
                    Color = "Red",
                    Size = "60",
                    Weight = 19.90,
                    StandardCost = 486.7066,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 782.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-650",
                    ProdCat = "Bikes",
                    Description = "Value-priced bike with many features of our top-of-the-line models. Has the same light, stiff frame, and the quick acceleration we're famous for.",
                    ProdName = "Road-650 Red, 62",
                    ProductNumber = "BK-R50R-62",
                    Color = "Red",
                    Size = "62",
                    Weight = 20.00,
                    StandardCost = 486.7066,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 782.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-650",
                    ProdCat = "Bikes",
                    Description = "Value-priced bike with many features of our top-of-the-line models. Has the same light, stiff frame, and the quick acceleration we're famous for.",
                    ProdName = "Road-650 Red, 44",
                    ProductNumber = "BK-R50R-44",
                    Color = "Red",
                    Size = "44",
                    Weight = 18.77,
                    StandardCost = 486.7066,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 782.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-650",
                    ProdCat = "Bikes",
                    Description = "Value-priced bike with many features of our top-of-the-line models. Has the same light, stiff frame, and the quick acceleration we're famous for.",
                    ProdName = "Road-650 Red, 48",
                    ProductNumber = "BK-R50R-48",
                    Color = "Red",
                    Size = "48",
                    Weight = 19.13,
                    StandardCost = 486.7066,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 782.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-650",
                    ProdCat = "Bikes",
                    Description = "Value-priced bike with many features of our top-of-the-line models. Has the same light, stiff frame, and the quick acceleration we're famous for.",
                    ProdName = "Road-650 Red, 52",
                    ProductNumber = "BK-R50R-52",
                    Color = "Red",
                    Size = "52",
                    Weight = 19.42,
                    StandardCost = 486.7066,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 782.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-650",
                    ProdCat = "Bikes",
                    Description = "Value-priced bike with many features of our top-of-the-line models. Has the same light, stiff frame, and the quick acceleration we're famous for.",
                    ProdName = "Road-650 Black, 58",
                    ProductNumber = "BK-R50B-58",
                    Color = "Black",
                    Size = "58",
                    Weight = 19.79,
                    StandardCost = 486.7066,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 782.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-650",
                    ProdCat = "Bikes",
                    Description = "Value-priced bike with many features of our top-of-the-line models. Has the same light, stiff frame, and the quick acceleration we're famous for.",
                    ProdName = "Road-650 Black, 60",
                    ProductNumber = "BK-R50B-60",
                    Color = "Black",
                    Size = "60",
                    Weight = 19.90,
                    StandardCost = 486.7066,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 782.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-650",
                    ProdCat = "Bikes",
                    Description = "Value-priced bike with many features of our top-of-the-line models. Has the same light, stiff frame, and the quick acceleration we're famous for.",
                    ProdName = "Road-650 Black, 62",
                    ProductNumber = "BK-R50B-62",
                    Color = "Black",
                    Size = "62",
                    Weight = 20.00,
                    StandardCost = 486.7066,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 782.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-650",
                    ProdCat = "Bikes",
                    Description = "Value-priced bike with many features of our top-of-the-line models. Has the same light, stiff frame, and the quick acceleration we're famous for.",
                    ProdName = "Road-650 Black, 44",
                    ProductNumber = "BK-R50B-44",
                    Color = "Black",
                    Size = "44",
                    Weight = 18.77,
                    StandardCost = 486.7066,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 782.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-650",
                    ProdCat = "Bikes",
                    Description = "Value-priced bike with many features of our top-of-the-line models. Has the same light, stiff frame, and the quick acceleration we're famous for.",
                    ProdName = "Road-650 Black, 48",
                    ProductNumber = "BK-R50B-48",
                    Color = "Black",
                    Size = "48",
                    Weight = 19.13,
                    StandardCost = 486.7066,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 782.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-650",
                    ProdCat = "Bikes",
                    Description = "Value-priced bike with many features of our top-of-the-line models. Has the same light, stiff frame, and the quick acceleration we're famous for.",
                    ProdName = "Road-650 Black, 52",
                    ProductNumber = "BK-R50B-52",
                    Color = "Black",
                    Size = "52",
                    Weight = 19.42,
                    StandardCost = 486.7066,
                    Style = "U ",
                    Class = "L ",
                    ListPrice = 782.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-100",
                    ProdCat = "Bikes",
                    Description = "Top-of-the-line competition mountain bike. Performance-enhancing options include the innovative HL Frame, super-smooth front suspension, and traction for all terrain.",
                    ProdName = "Mountain-100 Silver, 38",
                    ProductNumber = "BK-M82S-38",
                    Color = "Silver",
                    Size = "38",
                    Weight = 20.35,
                    StandardCost = 1912.1544,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 3399.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-100",
                    ProdCat = "Bikes",
                    Description = "Top-of-the-line competition mountain bike. Performance-enhancing options include the innovative HL Frame, super-smooth front suspension, and traction for all terrain.",
                    ProdName = "Mountain-100 Silver, 42",
                    ProductNumber = "BK-M82S-42",
                    Color = "Silver",
                    Size = "42",
                    Weight = 20.77,
                    StandardCost = 1912.1544,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 3399.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-100",
                    ProdCat = "Bikes",
                    Description = "Top-of-the-line competition mountain bike. Performance-enhancing options include the innovative HL Frame, super-smooth front suspension, and traction for all terrain.",
                    ProdName = "Mountain-100 Silver, 44",
                    ProductNumber = "BK-M82S-44",
                    Color = "Silver",
                    Size = "44",
                    Weight = 21.13,
                    StandardCost = 1912.1544,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 3399.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-100",
                    ProdCat = "Bikes",
                    Description = "Top-of-the-line competition mountain bike. Performance-enhancing options include the innovative HL Frame, super-smooth front suspension, and traction for all terrain.",
                    ProdName = "Mountain-100 Silver, 48",
                    ProductNumber = "BK-M82S-48",
                    Color = "Silver",
                    Size = "48",
                    Weight = 21.42,
                    StandardCost = 1912.1544,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 3399.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-100",
                    ProdCat = "Bikes",
                    Description = "Top-of-the-line competition mountain bike. Performance-enhancing options include the innovative HL Frame, super-smooth front suspension, and traction for all terrain.",
                    ProdName = "Mountain-100 Black, 38",
                    ProductNumber = "BK-M82B-38",
                    Color = "Black",
                    Size = "38",
                    Weight = 20.35,
                    StandardCost = 1898.0944,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 3374.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-100",
                    ProdCat = "Bikes",
                    Description = "Top-of-the-line competition mountain bike. Performance-enhancing options include the innovative HL Frame, super-smooth front suspension, and traction for all terrain.",
                    ProdName = "Mountain-100 Black, 42",
                    ProductNumber = "BK-M82B-42",
                    Color = "Black",
                    Size = "42",
                    Weight = 20.77,
                    StandardCost = 1898.0944,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 3374.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-100",
                    ProdCat = "Bikes",
                    Description = "Top-of-the-line competition mountain bike. Performance-enhancing options include the innovative HL Frame, super-smooth front suspension, and traction for all terrain.",
                    ProdName = "Mountain-100 Black, 44",
                    ProductNumber = "BK-M82B-44",
                    Color = "Black",
                    Size = "44",
                    Weight = 21.13,
                    StandardCost = 1898.0944,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 3374.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-100",
                    ProdCat = "Bikes",
                    Description = "Top-of-the-line competition mountain bike. Performance-enhancing options include the innovative HL Frame, super-smooth front suspension, and traction for all terrain.",
                    ProdName = "Mountain-100 Black, 48",
                    ProductNumber = "BK-M82B-48",
                    Color = "Black",
                    Size = "48",
                    Weight = 21.42,
                    StandardCost = 1898.0944,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 3374.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-200",
                    ProdCat = "Bikes",
                    Description = "Serious back-country riding. Perfect for all levels of competition. Uses the same HL Frame as the Mountain-100.",
                    ProdName = "Mountain-200 Silver, 38",
                    ProductNumber = "BK-M68S-38",
                    Color = "Silver",
                    Size = "38",
                    Weight = 23.35,
                    StandardCost = 1265.6195,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2319.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-200",
                    ProdCat = "Bikes",
                    Description = "Serious back-country riding. Perfect for all levels of competition. Uses the same HL Frame as the Mountain-100.",
                    ProdName = "Mountain-200 Silver, 42",
                    ProductNumber = "BK-M68S-42",
                    Color = "Silver",
                    Size = "42",
                    Weight = 23.77,
                    StandardCost = 1265.6195,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2319.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-200",
                    ProdCat = "Bikes",
                    Description = "Serious back-country riding. Perfect for all levels of competition. Uses the same HL Frame as the Mountain-100.",
                    ProdName = "Mountain-200 Silver, 46",
                    ProductNumber = "BK-M68S-46",
                    Color = "Silver",
                    Size = "46",
                    Weight = 24.13,
                    StandardCost = 1265.6195,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2319.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-200",
                    ProdCat = "Bikes",
                    Description = "Serious back-country riding. Perfect for all levels of competition. Uses the same HL Frame as the Mountain-100.",
                    ProdName = "Mountain-200 Black, 38",
                    ProductNumber = "BK-M68B-38",
                    Color = "Black",
                    Size = "38",
                    Weight = 23.35,
                    StandardCost = 1251.9813,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2294.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-200",
                    ProdCat = "Bikes",
                    Description = "Serious back-country riding. Perfect for all levels of competition. Uses the same HL Frame as the Mountain-100.",
                    ProdName = "Mountain-200 Black, 42",
                    ProductNumber = "BK-M68B-42",
                    Color = "Black",
                    Size = "42",
                    Weight = 23.77,
                    StandardCost = 1251.9813,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2294.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-200",
                    ProdCat = "Bikes",
                    Description = "Serious back-country riding. Perfect for all levels of competition. Uses the same HL Frame as the Mountain-100.",
                    ProdName = "Mountain-200 Black, 46",
                    ProductNumber = "BK-M68B-46",
                    Color = "Black",
                    Size = "46",
                    Weight = 24.13,
                    StandardCost = 1251.9813,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2294.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-300",
                    ProdCat = "Bikes",
                    Description = "For true trail addicts.  An extremely durable bike that will go anywhere and keep you in control on challenging terrain - without breaking your budget.",
                    ProdName = "Mountain-300 Black, 38",
                    ProductNumber = "BK-M47B-38",
                    Color = "Black",
                    Size = "38",
                    Weight = 25.35,
                    StandardCost = 598.4354,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 1079.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-300",
                    ProdCat = "Bikes",
                    Description = "For true trail addicts.  An extremely durable bike that will go anywhere and keep you in control on challenging terrain - without breaking your budget.",
                    ProdName = "Mountain-300 Black, 40",
                    ProductNumber = "BK-M47B-40",
                    Color = "Black",
                    Size = "40",
                    Weight = 25.77,
                    StandardCost = 598.4354,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 1079.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-300",
                    ProdCat = "Bikes",
                    Description = "For true trail addicts.  An extremely durable bike that will go anywhere and keep you in control on challenging terrain - without breaking your budget.",
                    ProdName = "Mountain-300 Black, 44",
                    ProductNumber = "BK-M47B-44",
                    Color = "Black",
                    Size = "44",
                    Weight = 26.13,
                    StandardCost = 598.4354,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 1079.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Mountain Bikes",
                    ProdModel = "Mountain-300",
                    ProdCat = "Bikes",
                    Description = "For true trail addicts.  An extremely durable bike that will go anywhere and keep you in control on challenging terrain - without breaking your budget.",
                    ProdName = "Mountain-300 Black, 48",
                    ProductNumber = "BK-M47B-48",
                    Color = "Black",
                    Size = "48",
                    Weight = 26.42,
                    StandardCost = 598.4354,
                    Style = "U ",
                    Class = "M ",
                    ListPrice = 1079.9900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-250",
                    ProdCat = "Bikes",
                    Description = "Alluminum-alloy frame provides a light, stiff ride, whether you are racing in the velodrome or on a demanding club ride on country roads.",
                    ProdName = "Road-250 Red, 44",
                    ProductNumber = "BK-R89R-44",
                    Color = "Red",
                    Size = "44",
                    Weight = 14.77,
                    StandardCost = 1518.7864,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2443.3500
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-250",
                    ProdCat = "Bikes",
                    Description = "Alluminum-alloy frame provides a light, stiff ride, whether you are racing in the velodrome or on a demanding club ride on country roads.",
                    ProdName = "Road-250 Red, 48",
                    ProductNumber = "BK-R89R-48",
                    Color = "Red",
                    Size = "48",
                    Weight = 15.13,
                    StandardCost = 1518.7864,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2443.3500
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-250",
                    ProdCat = "Bikes",
                    Description = "Alluminum-alloy frame provides a light, stiff ride, whether you are racing in the velodrome or on a demanding club ride on country roads.",
                    ProdName = "Road-250 Red, 52",
                    ProductNumber = "BK-R89R-52",
                    Color = "Red",
                    Size = "52",
                    Weight = 15.42,
                    StandardCost = 1518.7864,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2443.3500
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-250",
                    ProdCat = "Bikes",
                    Description = "Alluminum-alloy frame provides a light, stiff ride, whether you are racing in the velodrome or on a demanding club ride on country roads.",
                    ProdName = "Road-250 Red, 58",
                    ProductNumber = "BK-R89R-58",
                    Color = "Red",
                    Size = "58",
                    Weight = 15.79,
                    StandardCost = 1554.9479,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2443.3500
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-250",
                    ProdCat = "Bikes",
                    Description = "Alluminum-alloy frame provides a light, stiff ride, whether you are racing in the velodrome or on a demanding club ride on country roads.",
                    ProdName = "Road-250 Black, 44",
                    ProductNumber = "BK-R89B-44",
                    Color = "Black",
                    Size = "44",
                    Weight = 14.77,
                    StandardCost = 1554.9479,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2443.3500
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-250",
                    ProdCat = "Bikes",
                    Description = "Alluminum-alloy frame provides a light, stiff ride, whether you are racing in the velodrome or on a demanding club ride on country roads.",
                    ProdName = "Road-250 Black, 48",
                    ProductNumber = "BK-R89B-48",
                    Color = "Black",
                    Size = "48",
                    Weight = 15.13,
                    StandardCost = 1554.9479,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2443.3500
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-250",
                    ProdCat = "Bikes",
                    Description = "Alluminum-alloy frame provides a light, stiff ride, whether you are racing in the velodrome or on a demanding club ride on country roads.",
                    ProdName = "Road-250 Black, 52",
                    ProductNumber = "BK-R89B-52",
                    Color = "Black",
                    Size = "52",
                    Weight = 15.42,
                    StandardCost = 1554.9479,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2443.3500
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-250",
                    ProdCat = "Bikes",
                    Description = "Alluminum-alloy frame provides a light, stiff ride, whether you are racing in the velodrome or on a demanding club ride on country roads.",
                    ProdName = "Road-250 Black, 58",
                    ProductNumber = "BK-R89B-58",
                    Color = "Black",
                    Size = "58",
                    Weight = 15.68,
                    StandardCost = 1554.9479,
                    Style = "U ",
                    Class = "H ",
                    ListPrice = 2443.3500
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-550-W",
                    ProdCat = "Bikes",
                    Description = "Same technology as all of our Road series bikes, but the frame is sized for a woman.  Perfect all-around bike for road or racing.",
                    ProdName = "Road-550-W Yellow, 38",
                    ProductNumber = "BK-R64Y-38",
                    Color = "Yellow",
                    Size = "38",
                    Weight = 17.35,
                    StandardCost = 713.0798,
                    Style = "W ",
                    Class = "M ",
                    ListPrice = 1120.4900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-550-W",
                    ProdCat = "Bikes",
                    Description = "Same technology as all of our Road series bikes, but the frame is sized for a woman.  Perfect all-around bike for road or racing.",
                    ProdName = "Road-550-W Yellow, 40",
                    ProductNumber = "BK-R64Y-40",
                    Color = "Yellow",
                    Size = "40",
                    Weight = 17.77,
                    StandardCost = 713.0798,
                    Style = "W ",
                    Class = "M ",
                    ListPrice = 1120.4900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-550-W",
                    ProdCat = "Bikes",
                    Description = "Same technology as all of our Road series bikes, but the frame is sized for a woman.  Perfect all-around bike for road or racing.",
                    ProdName = "Road-550-W Yellow, 42",
                    ProductNumber = "BK-R64Y-42",
                    Color = "Yellow",
                    Size = "42",
                    Weight = 18.13,
                    StandardCost = 713.0798,
                    Style = "W ",
                    Class = "M ",
                    ListPrice = 1120.4900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-550-W",
                    ProdCat = "Bikes",
                    Description = "Same technology as all of our Road series bikes, but the frame is sized for a woman.  Perfect all-around bike for road or racing.",
                    ProdName = "Road-550-W Yellow, 44",
                    ProductNumber = "BK-R64Y-44",
                    Color = "Yellow",
                    Size = "44",
                    Weight = 18.42,
                    StandardCost = 713.0798,
                    Style = "W ",
                    Class = "M ",
                    ListPrice = 1120.4900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Road Bikes",
                    ProdModel = "Road-550-W",
                    ProdCat = "Bikes",
                    Description = "Same technology as all of our Road series bikes, but the frame is sized for a woman.  Perfect all-around bike for road or racing.",
                    ProdName = "Road-550-W Yellow, 48",
                    ProductNumber = "BK-R64Y-48",
                    Color = "Yellow",
                    Size = "48",
                    Weight = 18.68,
                    StandardCost = 713.0798,
                    Style = "W ",
                    Class = "M ",
                    ListPrice = 1120.4900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Forks",
                    ProdModel = "LL Fork",
                    ProdCat = "Components",
                    Description = "Stout design absorbs shock and offers more precise steering.",
                    ProdName = "LL Fork",
                    ProductNumber = "FK-1639",
                    Color = "",
                    Size = "",
                    Weight = null,
                    StandardCost = 65.8097,
                    Style = "",
                    Class = "L ",
                    ListPrice = 148.2200
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Forks",
                    ProdModel = "ML Fork",
                    ProdCat = "Components",
                    Description = "Composite road fork with an aluminum steerer tube.",
                    ProdName = "ML Fork",
                    ProductNumber = "FK-5136",
                    Color = "",
                    Size = "",
                    Weight = null,
                    StandardCost = 77.9176,
                    Style = "",
                    Class = "M ",
                    ListPrice = 175.4900
                };
                datas.Add(data);
                data = new ProductCatalog()
                {
                    ProdSubCat = "Forks",
                    ProdModel = "HL Fork",
                    ProdCat = "Components",
                    Description = "High-performance carbon road fork with curved legs.",
                    ProdName = "HL Fork",
                    ProductNumber = "FK-9939",
                    Color = "",
                    Size = "",
                    Weight = null,
                    StandardCost = 101.8936,
                    Style = "",
                    Class = "H ",
                    ListPrice = 229.4900
                };
                datas.Add(data);
                return datas;
            }
        }


    }

    public class ProductLineSales : ReportViewerSampleHelper
    {
        public ProductLineSales(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ProductShowcase";
            this.ReportName = "Product Line Sales";
        }


        public override void SetParameter()
        {
            this.ReportViewer.SetParameters(this.GetParameters());
        }

        public override void UpdateDataSet()
        {
            setDataSource();
        }

        public void setDataSource()
        {
            ReportParameterInfoCollection paramCollection = this.ReportViewer.GetParameters();
            string productCategory = "1";//paramCollection.Where(p => p.Name.Equals("ProductCategory")).FirstOrDefault().Values.FirstOrDefault();
            string subCategory = paramCollection.Where(p => p.Name.Equals("ProductSubcategory")).FirstOrDefault().Values.FirstOrDefault();
            string startDate = paramCollection.Where(p => p.Name.Equals("StartDate")).FirstOrDefault().Values.FirstOrDefault();
            string endDate = paramCollection.Where(p => p.Name.Equals("EndDate")).FirstOrDefault().Values.FirstOrDefault();

            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "TopEmployees", Value = Employee.GetTopEmployees(productCategory, subCategory, DateTime.Parse(startDate), DateTime.Parse(endDate)) });
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "ProductCategories", Value = ProductCategory.GetProductCategories() });
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "TopCustomers", Value = Customer.GetTopCustomers(productCategory, subCategory, DateTime.Parse(startDate), DateTime.Parse(endDate)) });
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "ProductSubcategories", Value = SubCategory.GetProductSubCategories() });
        }

        public IList<ReportParameter> GetParameters()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            ReportParameter param = new ReportParameter();
            //param.Labels.Add("Bikes");
            //param.Values.Add("1");
            //param.Name = "ProductCategory";
            //parameters.Add(param);

            param = new ReportParameter();
            param.Labels.Add("Road Bikes");
            param.Values.Add("2");
            param.Name = "ProductSubcategory";
            parameters.Add(param);

            param = new ReportParameter();
            param.Values.Add("2003-1-1");
            param.Name = "StartDate";
            parameters.Add(param);

            param = new ReportParameter();
            param.Values.Add("2003-12-31");
            param.Name = "EndDate";
            parameters.Add(param);

            return parameters;
        }

        public class Employee
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string FullName { get; set; }
            public string EmployeeID { get; set; }
            public double SaleAmount { get; set; }
            public DateTime OrderDate { get; set; }
            public string ProductSubcategoryID { get; set; }
            public string ProductCategoryID { get; set; }

            public static IList GetTopEmployees(string catId, string subCatId, DateTime startDate, DateTime endDate)
            {
                List<Employee> employees = new List<Employee>();
                Employee employee = null;
                employee = new Employee()
                {
                    LastName = "Blythe",
                    FirstName = "Michael",
                    EmployeeID = "275",
                    SaleAmount = 41608538.6755,
                    OrderDate = DateTime.Parse("2003-1-1"),
                    ProductSubcategoryID = "2",
                    ProductCategoryID = "1"

                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);
                employee = new Employee()
                {
                    LastName = "Pak",
                    FirstName = "Jae",
                    EmployeeID = "285",
                    SaleAmount = 35294804.7266,
                    OrderDate = DateTime.Parse("2003-6-1"),
                    ProductSubcategoryID = "2",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);
                employee = new Employee()
                {
                    LastName = "Carson",
                    FirstName = "Jillian",
                    EmployeeID = "277",
                    SaleAmount = 30990517.95,
                    OrderDate = DateTime.Parse("2003-8-1"),
                    ProductSubcategoryID = "2",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);
                employee = new Employee()
                {
                    LastName = "Mitchell",
                    FirstName = "Linda",
                    EmployeeID = "276",
                    SaleAmount = 29802308.3236,
                    OrderDate = DateTime.Parse("2003-11-1"),
                    ProductSubcategoryID = "2",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);
                employee = new Employee()
                {
                    LastName = "Ito",
                    FirstName = "Shu",
                    EmployeeID = "281",
                    SaleAmount = 20770828.1686,
                    OrderDate = DateTime.Parse("2003-12-1"),
                    ProductSubcategoryID = "2",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);

                employee = new Employee()
                {
                    LastName = "Mitchell",
                    FirstName = "Linda",
                    EmployeeID = "276",
                    SaleAmount = 9132974.375,
                    OrderDate = DateTime.Parse("2003/08/01"),
                    ProductSubcategoryID = "1",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);
                employee = new Employee()
                {
                    LastName = "Pak",
                    FirstName = "Jae",
                    EmployeeID = "285",
                    SaleAmount = 7973439.8469,
                    OrderDate = DateTime.Parse("2003/11/01"),
                    ProductSubcategoryID = "1",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);
                employee = new Employee()
                {
                    LastName = "Blythe",
                    FirstName = "Michael",
                    EmployeeID = "275",
                    SaleAmount = 5156210.2513,
                    OrderDate = DateTime.Parse("2003/09/01"),
                    ProductSubcategoryID = "1",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);
                employee = new Employee()
                {
                    LastName = "Carson",
                    FirstName = "Jillian",
                    EmployeeID = "277",
                    SaleAmount = 4038367.4366,
                    OrderDate = DateTime.Parse("2003/12/01"),
                    ProductSubcategoryID = "1",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);
                employee = new Employee()
                {
                    LastName = "saraiva",
                    FirstName = "Jose",
                    EmployeeID = "295",
                    SaleAmount = 3797988.698,
                    OrderDate = DateTime.Parse("2003/07/01"),
                    ProductSubcategoryID = "1",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);

                employee = new Employee()
                {
                    LastName = "Varkey",
                    FirstName = "Ranjit",
                    EmployeeID = "296",
                    SaleAmount = 8640302.3729,
                    OrderDate = DateTime.Parse("2003/08/01"),
                    ProductSubcategoryID = "3",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);
                employee = new Employee()
                {
                    LastName = "Valdez",
                    FirstName = "Rachel",
                    EmployeeID = "286",
                    SaleAmount = 6534427.6663,
                    OrderDate = DateTime.Parse("2003/11/01"),
                    ProductSubcategoryID = "3",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);
                employee = new Employee()
                {
                    LastName = "Ito",
                    FirstName = "Shu",
                    EmployeeID = "281",
                    SaleAmount = 4317789.4615,
                    OrderDate = DateTime.Parse("2003/07/01"),
                    ProductSubcategoryID = "3",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);
                employee = new Employee()
                {
                    LastName = "Mitchell",
                    FirstName = "Linda",
                    EmployeeID = "276",
                    SaleAmount = 3949171.4308,
                    OrderDate = DateTime.Parse("2003/07/01"),
                    ProductSubcategoryID = "3",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);
                employee = new Employee()
                {
                    LastName = "Pak",
                    FirstName = "Jae",
                    EmployeeID = "285",
                    SaleAmount = 3745099.762,
                    OrderDate = DateTime.Parse("2003/09/01"),
                    ProductSubcategoryID = "3",
                    ProductCategoryID = "1"
                };
                employee.FullName = employee.FirstName + employee.LastName;
                employees.Add(employee);

                return employees.Where(emp => emp.OrderDate >= startDate && emp.OrderDate <= endDate && emp.ProductCategoryID.Trim().Equals(catId) && emp.ProductSubcategoryID.Trim().Equals(subCatId)).ToList();
            }
        }

        public class ProductCategory
        {
            public string ProductCategoryID { get; set; }
            public string Name { get; set; }
            public static IList GetProductCategories()
            {
                List<ProductCategory> productCategories = new List<ProductCategory>();
                ProductCategory productCategory = null;
                productCategory = new ProductCategory()
                {
                    ProductCategoryID = "4",
                    Name = "Accessories"
                };
                productCategories.Add(productCategory);
                productCategory = new ProductCategory()
                {
                    ProductCategoryID = "1",
                    Name = "Bikes"
                };
                productCategories.Add(productCategory);
                productCategory = new ProductCategory()
                {
                    ProductCategoryID = "3",
                    Name = "Clothing"
                };
                productCategories.Add(productCategory);
                productCategory = new ProductCategory()
                {
                    ProductCategoryID = "2",
                    Name = "Components"
                };
                productCategories.Add(productCategory);
                return productCategories;
            }
        }
        public class Customer
        {
            public string StoreName { get; set; }
            public double SaleAmount { get; set; }
            public string ProductSubcategoryID { get; set; }
            public string ProductCategoryID { get; set; }
            public DateTime OrderDate { get; set; }
            public static IList GetTopCustomers(string catId, string subCatId, DateTime startDate, DateTime endDate)
            {
                List<Customer> customers = new List<Customer>();
                Customer customer = null;
                customer = new Customer()
                {
                    StoreName = "Excellent Riding Supplies",
                    SaleAmount = 8564441.5333,
                    ProductSubcategoryID = "2",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003-1-1")
                };
                customers.Add(customer);
                customer = new Customer()
                {
                    StoreName = "Totes & Baskets Company",
                    SaleAmount = 8460123.2145,
                    ProductSubcategoryID = "2",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003-6-1")
                };
                customers.Add(customer);
                customer = new Customer()
                {
                    StoreName = "Corner Bicycle Supply",
                    SaleAmount = 8252044.9923,
                    ProductSubcategoryID = "2",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003-8-1")
                };
                customers.Add(customer);
                customer = new Customer()
                {
                    StoreName = "Sheet Metal Manufacturing",
                    SaleAmount = 8020572.7343,
                    ProductSubcategoryID = "2",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003-11-1")
                };
                customers.Add(customer);
                customer = new Customer()
                {
                    StoreName = "Reasonable Bicycle Sales",
                    SaleAmount = 7781217.897,
                    ProductSubcategoryID = "2",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003-12-1")
                };
                customers.Add(customer);

                customer = new Customer()
                {
                    StoreName = "Field Trip Store",
                    SaleAmount = 3000397.7319,
                    ProductSubcategoryID = "1",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003/08/01")
                };
                customers.Add(customer);
                customer = new Customer()
                {
                    StoreName = "Brakes and Gears",
                    SaleAmount = 2704505.996,
                    ProductSubcategoryID = "1",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003/11/01")
                };
                customers.Add(customer);
                customer = new Customer()
                {
                    StoreName = "Metropolitan Bicycle Supply",
                    SaleAmount = 2702021.422,
                    ProductSubcategoryID = "1",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003/11/01")
                };
                customers.Add(customer);
                customer = new Customer()
                {
                    StoreName = "Rural Cycle Emporium",
                    SaleAmount = 2606812.634,
                    ProductSubcategoryID = "1",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003/08/01")
                };
                customers.Add(customer);
                customer = new Customer()
                {
                    StoreName = "Registered Cycle Store",
                    SaleAmount = 2017204.104,
                    ProductSubcategoryID = "1",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003/08/01")
                };
                customers.Add(customer);

                customer = new Customer()
                {
                    StoreName = "Westside Plaza",
                    SaleAmount = 4262773.1789,
                    ProductSubcategoryID = "3",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003/07/01")
                };
                customers.Add(customer);
                customer = new Customer()
                {
                    StoreName = "Perfect Toys",
                    SaleAmount = 3382412.03,
                    ProductSubcategoryID = "3",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003/08/01")
                };
                customers.Add(customer);
                customer = new Customer()
                {
                    StoreName = "Camping and Sports Store",
                    SaleAmount = 3297559.7696,
                    ProductSubcategoryID = "3",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003/10/01")
                };
                customers.Add(customer);
                customer = new Customer()
                {
                    StoreName = "Roadway Bicycle Supply",
                    SaleAmount = 3234122.8172,
                    ProductSubcategoryID = "3",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003/11/01")
                };
                customers.Add(customer);
                customer = new Customer()
                {
                    StoreName = "Global Bike Retailers",
                    SaleAmount = 2853318.3206,
                    ProductSubcategoryID = "3",
                    ProductCategoryID = "1",
                    OrderDate = DateTime.Parse("2003/08/01")
                };
                customers.Add(customer);

                return customers.Where(cust => cust.OrderDate >= startDate && cust.OrderDate <= endDate && cust.ProductCategoryID.Trim().Equals(catId) && cust.ProductSubcategoryID.Trim().Equals(subCatId)).ToList();
            }
        }

        public class SubCategory
        {
            public string ProductSubcategoryID { get; set; }
            public string ProductCategoryID { get; set; }
            public string Name { get; set; }
            public static IList GetProductSubCategories()
            {
                List<SubCategory> subCategories = new List<SubCategory>();
                SubCategory category = null;
                category = new SubCategory()
                {
                    ProductSubcategoryID = "1",
                    ProductCategoryID = "1",
                    Name = "Mountain Bikes"
                };
                subCategories.Add(category);
                category = new SubCategory()
                {
                    ProductSubcategoryID = "2",
                    ProductCategoryID = "1",
                    Name = "Road Bikes"
                };
                subCategories.Add(category);
                category = new SubCategory()
                {
                    ProductSubcategoryID = "3",
                    ProductCategoryID = "1",
                    Name = "Touring Bikes"
                };
                subCategories.Add(category);
                return subCategories;
            }
        }
    }

    public class SalesDashboard : ReportViewerSampleHelper
    {
        public SalesDashboard(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ProductShowcase";
            this.ReportName = "Sales Dashboard";
        }

        public override void SetParameter()
        {
            this.ReportViewer.SetParameters(this.GetParameters());
        }

        public override void UpdateDataSet()
        {
            setDataSource();
        }

        public void setDataSource()
        {
            ReportParameterInfoCollection paramCollection = this.ReportViewer.GetParameters();
            string Year = paramCollection.Where(p => p.Name.Equals("SalesYearParameter")).FirstOrDefault().Values.FirstOrDefault();

            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "TopSalesPerson", Value = SalesPersons.GetTopSalesPerson(int.Parse(Year)) });
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "TopStores", Value = Stores.GetTopStores(int.Parse(Year)) });
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "TopProduct", Value = Products.GetTopProducts(int.Parse(Year)) });

        }

        public IList<ReportParameter> GetParameters()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            ReportParameter param = new ReportParameter();
            param.Labels.Add("2001");
            param.Values.Add("2001");
            param.Name = "SalesYearParameter";
            parameters.Add(param);
            return parameters;
        }
        public class SalesPersons
        {
            public string Name { get; set; }
            public double QS1 { get; set; }
            public double QS2 { get; set; }
            public double QS3 { get; set; }
            public double QS4 { get; set; }
            public double Total { get; set; }
            public int Year { get; set; }
            public static IList GetTopSalesPerson(int year)
            {
                List<SalesPersons> SalesPersonCollection = new List<SalesPersons>();
                SalesPersons salesPerson = null;
                salesPerson = new SalesPersons()
                {
                    Name = "Carol Elliott",
                    QS3 = 656287.9884,
                    QS4 = 1006810.7219,
                    Total = 1663098.7103,
                    Year = 2001
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Linda Ecoffey",
                    QS3 = 547589.0181,
                    QS4 = 951811.3768,
                    Total = 1499400.3949,
                    Year = 2001
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Shelley Dyck",
                    QS3 = 551800.5660,
                    QS4 = 823059.5628,
                    Total = 1374860.1288,
                    Year = 2001
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Michael Emanuel",
                    QS3 = 515235.5282,
                    QS4 = 733617.1174,
                    Total = 1248852.6456,
                    Year = 2001
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Jauna Elson",
                    QS3 = 453982.3228,
                    QS4 = 614458.9850,
                    Total = 1068441.3078,
                    Year = 2001
                };
                SalesPersonCollection.Add(salesPerson);

                salesPerson = new SalesPersons()
                {
                    Name = "Linda Ecoffey",
                    QS1 = 781351.2313,
                    QS2 = 1028144.2245,
                    QS3 = 1531824.7788,
                    QS4 = 1263787.6537,
                    Total = 4605107.8883,
                    Year = 2002
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Shelley Dyck",
                    QS1 = 710292.4523,
                    QS2 = 714876.5727,
                    QS3 = 1415932.4341,
                    QS4 = 1142145.8485,
                    Total = 3983247.3076,
                    Year = 2002
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Maciej Dusza",
                    QS1 = 489810.4459,
                    QS2 = 558046.5501,
                    QS3 = 1516710.9716,
                    QS4 = 1179535.1114,
                    Total = 3744103.0790,
                    Year = 2002
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Gail Erickson",
                    QS1 = 0,
                    QS2 = 0,
                    QS3 = 1739975.7306,
                    QS4 = 1306538.9169,
                    Total = 3046514.6475,
                    Year = 2002
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Carol Elliott",
                    QS1 = 744774.7917,
                    QS2 = 834823.5107,
                    QS3 = 812349.5603,
                    QS4 = 653131.5843,
                    Total = 3045079.4470,
                    Year = 2002
                };
                SalesPersonCollection.Add(salesPerson);

                salesPerson = new SalesPersons()
                {
                    Name = "Gail Erickson",
                    QS1 = 981735.7977,
                    QS2 = 1258793.8673,
                    QS3 = 1476692.0028,
                    QS4 = 1356994.7597,
                    Total = 5074216.4275,
                    Year = 2003
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Shelley Dyck",
                    QS1 = 925501.3401,
                    QS2 = 1163645.8204,
                    QS3 = 1622531.0508,
                    QS4 = 1284147.5293,
                    Total = 4995825.740,
                    Year = 2003
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Maciej Dusza",
                    QS1 = 820852.0529,
                    QS2 = 1226808.7576,
                    QS3 = 1497942.9170,
                    QS4 = 1200475.2000,
                    Total = 4746078.9275,
                    Year = 2003
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Linda Ecoffey",
                    QS1 = 917504.6713,
                    QS2 = 1278750.6036,
                    QS3 = 1252426.1780,
                    QS4 = 953423.5345,
                    Total = 4402104.9874,
                    Year = 2003
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Jauna Elson",
                    QS1 = 524679.7564,
                    QS2 = 626051.3386,
                    QS3 = 948047.6240,
                    QS4 = 777144.9274,
                    Total = 2875923.6464,
                    Year = 2003
                };
                SalesPersonCollection.Add(salesPerson);

                salesPerson = new SalesPersons()
                {
                    Name = "Shelley Dyck",
                    QS1 = 1026805.5472,
                    QS2 = 1266991.1038,
                    Total = 2293796.6510,
                    Year = 2004
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Gail Erickson",
                    QS1 = 934697.3750,
                    QS2 = 1247298.2376,
                    Total = 2181995.6126,
                    Year = 2004
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Maciej Dusza",
                    QS1 = 875242.441,
                    QS2 = 983384.4873,
                    Total = 1858626.9289,
                    Year = 2004
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Mark Erickson",
                    QS1 = 715362.2737,
                    QS2 = 943155.7965,
                    Total = 1658518.0702,
                    Year = 2004
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Linda Ecoffey",
                    QS1 = 731927.6488,
                    QS2 = 919386.2718,
                    Total = 1651313.9206,
                    Year = 2004
                };
                SalesPersonCollection.Add(salesPerson);

                return SalesPersonCollection.Where(spc => spc.Year == year).ToList();
            }
        }

        public class Stores
        {
            public string Name { get; set; }
            public double QS1 { get; set; }
            public double QS2 { get; set; }
            public double QS3 { get; set; }
            public double QS4 { get; set; }
            public double Total { get; set; }
            public int year { get; set; }
            public static IList GetTopStores(int year)
            {
                List<Stores> StoreCollection = new List<Stores>();
                Stores store = null;
                store = new Stores()
                {
                    Name = "Bicycle Company",
                    QS3 = 656287.9884,
                    QS4 = 1006810.7219,
                    Total = 1663098.7103,
                    year = 2001
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Activity Center",
                    QS3 = 656287.9884,
                    QS4 = 1006810.7219,
                    Total = 1663098.7103,
                    year = 2001
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Bike Shop",
                    QS3 = 656287.9884,
                    QS4 = 1006810.7219,
                    Total = 1663098.7103,
                    year = 2001
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Bike Goods ",
                    QS3 = 656287.9884,
                    QS4 = 1006810.7219,
                    Total = 1663098.7103,
                    year = 2001
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Bike Rims",
                    QS3 = 656287.9884,
                    QS4 = 1006810.7219,
                    Total = 1663098.7103,
                    year = 2001
                };
                StoreCollection.Add(store);

                store = new Stores()
                {
                    Name = "Great Bicycle",
                    QS1 = 781351.2313,
                    QS2 = 1028144.2245,
                    QS3 = 1531824.7788,
                    QS4 = 1263787.6537,
                    Total = 4605107.8883,
                    year = 2002
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Bike Shop",
                    QS1 = 781351.2313,
                    QS2 = 1028144.2245,
                    QS3 = 1531824.7788,
                    QS4 = 1263787.6537,
                    Total = 4605107.8883,
                    year = 2002
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Active Systems",
                    QS1 = 781351.2313,
                    QS2 = 1028144.2245,
                    QS3 = 1531824.7788,
                    QS4 = 1263787.6537,
                    Total = 4605107.8883,
                    year = 2002
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Advanced Bike",
                    QS1 = 781351.2313,
                    QS2 = 1028144.2245,
                    QS3 = 1531824.7788,
                    QS4 = 1263787.6537,
                    Total = 4605107.8883,
                    year = 2002
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Seasons Sports",
                    QS1 = 781351.2313,
                    QS2 = 1028144.2245,
                    QS3 = 1531824.7788,
                    QS4 = 1263787.6537,
                    Total = 4605107.8883,
                    year = 2002
                };
                StoreCollection.Add(store);

                store = new Stores()
                {
                    Name = "Action Bicycle",
                    QS1 = 981735.7977,
                    QS2 = 1258793.8673,
                    QS3 = 1476692.0028,
                    QS4 = 1356994.7597,
                    Total = 5074216.4275,
                    year = 2003
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Area Sheet",
                    QS1 = 981735.7977,
                    QS2 = 1258793.8673,
                    QS3 = 1476692.0028,
                    QS4 = 1356994.7597,
                    Total = 5074216.4275,
                    year = 2003
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Authentic Sales",
                    QS1 = 981735.7977,
                    QS2 = 1258793.8673,
                    QS3 = 1476692.0028,
                    QS4 = 1356994.7597,
                    Total = 5074216.4275,
                    year = 2003
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Discount Stores",
                    QS1 = 981735.7977,
                    QS2 = 1258793.8673,
                    QS3 = 1476692.0028,
                    QS4 = 1356994.7597,
                    Total = 5074216.4275,
                    year = 2003
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Central Bicycle",
                    QS1 = 981735.7977,
                    QS2 = 1258793.8673,
                    QS3 = 1476692.0028,
                    QS4 = 1356994.7597,
                    Total = 5074216.4275,
                    year = 2003
                };
                StoreCollection.Add(store);

                store = new Stores()
                {
                    Name = "Aerobic Exercise",
                    QS1 = 1026805.5472,
                    QS2 = 1266991.1038,
                    Total = 2293796.6510,
                    year = 2004
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Alpine House",
                    QS1 = 1026805.5472,
                    QS2 = 1266991.1038,
                    Total = 2293796.6510,
                    year = 2004
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Sporting Goods",
                    QS1 = 1026805.5472,
                    QS2 = 1266991.1038,
                    Total = 2293796.6510,
                    year = 2004
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Basic Sports",
                    QS1 = 1026805.5472,
                    QS2 = 1266991.1038,
                    Total = 2293796.6510,
                    year = 2004
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Bold Bikes",
                    QS1 = 1026805.5472,
                    QS2 = 1266991.1038,
                    Total = 2293796.6510,
                    year = 2004
                };
                StoreCollection.Add(store);

                return StoreCollection.Where(sc => sc.year == year).ToList();
            }
        }

        public class Products
        {
            public string Name { get; set; }
            public double QS1 { get; set; }
            public double QS2 { get; set; }
            public double QS3 { get; set; }
            public double QS4 { get; set; }
            public double Total { get; set; }
            public int Year { get; set; }
            public static IList GetTopProducts(int year)
            {
                List<Products> ProductCollection = new List<Products>();
                Products product = null;
                product = new Products()
                {
                    Name = "AWC Logo Cap",
                    QS3 = 2104165.3554,
                    QS4 = 3308323.5938,
                    Total = 5412488.9492,
                    Year = 2001
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, B",
                    QS3 = 1988382.4844,
                    QS4 = 3279281.6611,
                    Total = 5267664.1455,
                    Year = 2001
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "L-Sleeve Jrsey",
                    QS3 = 1974839.7878,
                    QS4 = 3282139.2608,
                    Total = 5256979.0486,
                    Year = 2001
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, R",
                    QS3 = 1971943.0387,
                    QS4 = 3140308.6115,
                    Total = 5112251.6502,
                    Year = 2001
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, Bk",
                    QS3 = 2008908.3029,
                    QS4 = 3096292.8994,
                    Total = 5105201.2023,
                    Year = 2001
                };
                ProductCollection.Add(product);

                product = new Products()
                {
                    Name = "AWC Logo Cap",
                    QS1 = 2479585.3057,
                    QS2 = 3246499.0536,
                    QS3 = 6803560.4591,
                    QS4 = 5176061.7865,
                    Total = 17705706.6049,
                    Year = 2002
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "L-Sleeve Jersey",
                    QS1 = 2506478.8038,
                    QS2 = 3451364.1049,
                    QS3 = 6640485.7449,
                    QS4 = 5081732.7792,
                    Total = 17680061.4328,
                    Year = 2002
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, B",
                    QS1 = 1591213.0306,
                    QS2 = 3019373.2897,
                    QS3 = 6681639.5312,
                    QS4 = 4970609.3327,
                    Total = 16262835.1842,
                    Year = 2002
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, R",
                    QS1 = 1200410.2689,
                    QS2 = 2986523.1779,
                    QS3 = 6704405.3528,
                    QS4 = 4796516.5549,
                    Total = 15687855.3545,
                    Year = 2002
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, Bk",
                    QS1 = 1326226.1287,
                    QS2 = 2785577.5765,
                    QS3 = 6702788.2116,
                    QS4 = 4830625.4549,
                    Total = 15645217.3717,
                    Year = 2002
                };
                ProductCollection.Add(product);

                product = new Products()
                {
                    Name = "AWC Logo Cap",
                    QS1 = 3767204.2532,
                    QS2 = 5144768.8936,
                    QS3 = 6850399.6554,
                    QS4 = 5329958.1031,
                    Total = 21092330.9053,
                    Year = 2003
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "L-Sleeve Jersey",
                    QS1 = 3718432.7781,
                    QS2 = 5082651.5234,
                    QS3 = 6479515.2897,
                    QS4 = 4995648.8312,
                    Total = 20276248.4224,
                    Year = 2003
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, B",
                    QS1 = 3001922.0189,
                    QS2 = 4938119.6587,
                    QS3 = 6504481.8462,
                    QS4 = 5092806.3654,
                    Total = 19537329.8892,
                    Year = 2003
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, Bk",
                    QS1 = 2532321.1181,
                    QS2 = 4896278.2250,
                    QS3 = 6517251.1923,
                    QS4 = 5152659.6077,
                    Total = 19098510.1431,
                    Year = 2003
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, R",
                    QS1 = 1444428.4599,
                    QS2 = 5020718.6434,
                    QS3 = 6451496.9938,
                    QS4 = 5015496.1295,
                    Total = 17932140.2266,
                    Year = 2003
                };
                ProductCollection.Add(product);

                product = new Products()
                {
                    Name = "Water Bottle",
                    QS1 = 4121940.2137,
                    QS2 = 5866889.4854,
                    QS3 = 7450.1100,
                    Total = 9996279.8091,
                    Year = 2004
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "AWC Logo Cap",
                    QS1 = 3676842.2034,
                    QS2 = 5407113.3880,
                    QS3 = 5465.0800,
                    Total = 9089420.6714,
                    Year = 2004
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "Classic Vest, S",
                    QS1 = 3794769.6580,
                    QS2 = 4936799.8561,
                    QS3 = 979.0100,
                    Total = 8732548.5241,
                    Year = 2004
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-SC Jersey, XL",
                    QS1 = 3568526.5876,
                    QS2 = 5130984.2023,
                    QS3 = 1761.0300,
                    Total = 8701271.8199,
                    Year = 2004
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, B",
                    QS1 = 3156520.2089,
                    QS2 = 5249154.4748,
                    QS3 = 6531.6000,
                    Total = 8412206.2837,
                    Year = 2004
                };
                ProductCollection.Add(product);

                return ProductCollection.Where(pc => pc.Year == year).ToList();
            }
        }

    }

    #endregion


    #region ReportItems

    public class MapDemo : ReportViewerSampleHelper
    {
        public MapDemo(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ReportElement";
            this.ReportName = "ElectionResultByRegion";
        }

        public override void UpdateDataSet()
        {
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource("ElectionResult", StateElectionResult.GetStateElectionResult()));
        }


        public class StateElectionResult
        {
            public string State { get; set; }

            public string Candidate { get; set; }

            public double Electors { get; set; }

            public static IList<StateElectionResult> GetStateElectionResult()
            {
                IList<StateElectionResult> electionResult = new List<StateElectionResult>();
                electionResult.Add(new StateElectionResult() { State = "Alabama", Candidate = "McCain", Electors = 9 });
                electionResult.Add(new StateElectionResult() { State = "Alaska", Candidate = "McCain", Electors = 3 });
                electionResult.Add(new StateElectionResult() { State = "Arizona", Candidate = "McCain", Electors = 10 });
                electionResult.Add(new StateElectionResult() { State = "Arkansas", Candidate = "McCain", Electors = 6 });
                electionResult.Add(new StateElectionResult() { State = "California", Candidate = "Obama", Electors = 55 });
                electionResult.Add(new StateElectionResult() { State = "Colorado", Candidate = "Obama", Electors = 9 });
                electionResult.Add(new StateElectionResult() { State = "Connecticut", Candidate = "Obama", Electors = 7 });
                electionResult.Add(new StateElectionResult() { State = "Delaware", Candidate = "Obama", Electors = 3 });
                electionResult.Add(new StateElectionResult() { State = "District of Columbia", Candidate = "Obama", Electors = 3 });
                electionResult.Add(new StateElectionResult() { State = "Florida", Candidate = "Obama", Electors = 27 });
                electionResult.Add(new StateElectionResult() { State = "Georgia", Candidate = "McCain", Electors = 15 });
                electionResult.Add(new StateElectionResult() { State = "Hawaii", Candidate = "Obama", Electors = 4 });
                electionResult.Add(new StateElectionResult() { State = "Idaho", Candidate = "McCain", Electors = 4 });
                electionResult.Add(new StateElectionResult() { State = "Illinois", Candidate = "Obama", Electors = 21 });
                electionResult.Add(new StateElectionResult() { State = "Indiana", Candidate = "Obama", Electors = 11 });
                electionResult.Add(new StateElectionResult() { State = "Iowa", Candidate = "Obama", Electors = 7 });
                electionResult.Add(new StateElectionResult() { State = "Kansas", Candidate = "McCain", Electors = 6 });
                electionResult.Add(new StateElectionResult() { State = "Kentucky", Candidate = "McCain", Electors = 8 });
                electionResult.Add(new StateElectionResult() { State = "Louisiana", Candidate = "McCain", Electors = 9 });
                electionResult.Add(new StateElectionResult() { State = "Maine", Candidate = "Obama", Electors = 2 });
                electionResult.Add(new StateElectionResult() { State = "Maryland", Candidate = "Obama", Electors = 10 });
                electionResult.Add(new StateElectionResult() { State = "Massachusetts", Candidate = "Obama", Electors = 12 });
                electionResult.Add(new StateElectionResult() { State = "Michingan", Candidate = "Obama", Electors = 17 });
                electionResult.Add(new StateElectionResult() { State = "Minnesota", Candidate = "Obama", Electors = 10 });
                electionResult.Add(new StateElectionResult() { State = "Mississippi", Candidate = "McCain", Electors = 6 });
                electionResult.Add(new StateElectionResult() { State = "Missouri", Candidate = "McCain", Electors = 11 });
                electionResult.Add(new StateElectionResult() { State = "Montana", Candidate = "McCain", Electors = 3 });
                electionResult.Add(new StateElectionResult() { State = "Nebraska", Candidate = "McCain", Electors = 2 });
                electionResult.Add(new StateElectionResult() { State = "Nevada", Candidate = "Obama", Electors = 5 });
                electionResult.Add(new StateElectionResult() { State = "New Hampshire", Candidate = "Obama", Electors = 4 });
                electionResult.Add(new StateElectionResult() { State = "New Jersey", Candidate = "Obama", Electors = 15 });
                electionResult.Add(new StateElectionResult() { State = "New Mexico", Candidate = "Obama", Electors = 5 });
                electionResult.Add(new StateElectionResult() { State = "New York", Candidate = "Obama", Electors = 31 });
                electionResult.Add(new StateElectionResult() { State = "North Carolina", Candidate = "Obama", Electors = 15 });
                electionResult.Add(new StateElectionResult() { State = "North Dakota", Candidate = "McCain", Electors = 3 });
                electionResult.Add(new StateElectionResult() { State = "Ohio", Candidate = "Obama", Electors = 20 });
                electionResult.Add(new StateElectionResult() { State = "Oklahoma", Candidate = "McCain", Electors = 7 });
                electionResult.Add(new StateElectionResult() { State = "Oregon", Candidate = "Obama", Electors = 7 });
                electionResult.Add(new StateElectionResult() { State = "Pennsylvania", Candidate = "Obama", Electors = 21 });
                electionResult.Add(new StateElectionResult() { State = "Rhode Island", Candidate = "Obama", Electors = 4 });
                electionResult.Add(new StateElectionResult() { State = "South Carolina", Candidate = "McCain", Electors = 8 });
                electionResult.Add(new StateElectionResult() { State = "South Dakota", Candidate = "McCain", Electors = 3 });
                electionResult.Add(new StateElectionResult() { State = "Tennessee", Candidate = "McCain", Electors = 11 });
                electionResult.Add(new StateElectionResult() { State = "Texas", Candidate = "McCain", Electors = 34 });
                electionResult.Add(new StateElectionResult() { State = "Utah", Candidate = "McCain", Electors = 5 });
                electionResult.Add(new StateElectionResult() { State = "Vermont", Candidate = "Obama", Electors = 3 });
                electionResult.Add(new StateElectionResult() { State = "Virginia", Candidate = "Obama", Electors = 13 });
                electionResult.Add(new StateElectionResult() { State = "Washington", Candidate = "Obama", Electors = 11 });
                electionResult.Add(new StateElectionResult() { State = "West Virginia", Candidate = "McCain", Electors = 5 });
                electionResult.Add(new StateElectionResult() { State = "Wisconsin", Candidate = "Obama", Electors = 10 });
                electionResult.Add(new StateElectionResult() { State = "Wyoming", Candidate = "McCain", Electors = 3 });
                return electionResult;
            }
        }
    }


    public class IndicatorDemo : ReportViewerSampleHelper
    {
        public IndicatorDemo(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ReportElement";
            this.ReportName = "IndicatorReport";
        }

        public override void UpdateDataSet()
        {
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet1", Value = Billionaires.GetList_2013() });
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet2", Value = Billionaires.GetList_2012() });
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet3", Value = Indicator.GetIndicator() });
        }

        public class Billionaires
        {
            public int No { get; set; }
            public string Name { get; set; }
            public double NetWorth { get; set; }
            public int Age { get; set; }
            public string CitizenShip { get; set; }
            public string Source { get; set; }
            public int RankingStatus { get; set; }
            public int ProfitStatus { get; set; }

            public Billionaires(int no, string name, double netWorth, int age, string citizenShip, string source, int status, int profit)
            {
                this.No = no;
                this.Name = name;
                this.NetWorth = netWorth;
                this.Age = age;
                this.CitizenShip = citizenShip;
                this.Source = source;
                this.RankingStatus = status;
                this.ProfitStatus = profit;
            }

            public static List<Billionaires> GetList_2013()
            {
                List<Billionaires> list_2013 = new List<Billionaires>();
                list_2013.Add(new Billionaires(1, "Carlos Slim", 73.0, 73, "Mexico", "Telmex,América Móvil, Grupo Carso", 50, 75));
                list_2013.Add(new Billionaires(2, "Bill Gates", 67.0, 57, "United States", "Microsoft", 50, 75));
                list_2013.Add(new Billionaires(3, "Amancio Ortega", 57.0, 76, "Spain", "Inditex Group", 75, 75));
                list_2013.Add(new Billionaires(4, "Warren Buffett", 53.0, 82, "United States", "Berkshire Hathaway", 25, 75));
                list_2013.Add(new Billionaires(5, "Larry Ellison", 43.0, 68, "United States", "Oracle Corporation", 75, 75));
                list_2013.Add(new Billionaires(6, "Charles Koch", 34.0, 77, "United States", "Koch Industries", 75, 75));
                list_2013.Add(new Billionaires(7, "David Koch", 34.0, 72, "United States", "Koch Industries", 75, 75));
                list_2013.Add(new Billionaires(8, "Li Ka-shing", 32.0, 84, "Hong Kong/ Canada", "Cheung Kong Holdings", 75, 75));
                list_2013.Add(new Billionaires(9, "Liliane Bettencourt", 30.0, 90, "France", "L'Oréal", 75, 75));
                list_2013.Add(new Billionaires(10, "Bernard Arnault", 29.0, 63, "France", "LVMH Moët Hennessy Louis Vuitton", 25, 25));
                return list_2013;
            }

            public static List<Billionaires> GetList_2012()
            {
                List<Billionaires> list_2012 = new List<Billionaires>();
                list_2012.Add(new Billionaires(1, "Carlos Slim", 69.0, 72, "Mexico", "Telmex,América Móvil, Grupo Carso", 50, 25));
                list_2012.Add(new Billionaires(2, "Bill Gates", 61.0, 56, "United States", "Microsoft", 50, 75));
                list_2012.Add(new Billionaires(3, "Warren Buffett", 44.0, 81, "United States", "Berkshire Hathaway", 50, 25));
                list_2012.Add(new Billionaires(4, "Bernard Arnault", 41.0, 63, "France", "LVMH Moët Hennessy Louis Vuitton", 50, 75));
                list_2012.Add(new Billionaires(5, "Amancio Ortega", 37.5, 75, "Spain", "Inditex Group", 75, 75));
                list_2012.Add(new Billionaires(6, "Larry Ellison", 36.0, 67, "United States", "Oracle Corporation", 25, 75));
                list_2012.Add(new Billionaires(7, "Eike Batista", 30.0, 55, " Brazil", "EBX Group", 75, 75));
                list_2012.Add(new Billionaires(8, "Stefan Persson", 26.0, 64, "Sweden", "H&M", 75, 75));
                list_2012.Add(new Billionaires(9, "Li Ka-shing", 25.5, 83, "Hong Kong/ Canada", "Cheung Kong Holdings", 75, 75));
                list_2012.Add(new Billionaires(10, "Karl Albrecht", 25.4, 92, "Germany", "Aldi", 75, 75));
                return list_2012;
            }
        }

        public class Indicator
        {
            public int Status { get; set; }
            public string Description { get; set; }

            public Indicator(int status, string description)
            {
                this.Status = status;
                this.Description = description;
            }

            public static List<Indicator> GetIndicator()
            {
                List<Indicator> ind = new List<Indicator>();
                ind.Add(new Indicator(25, "Has not changed from the previous ranking."));
                ind.Add(new Indicator(50, "Has increased from the previous ranking."));
                ind.Add(new Indicator(75, "Has decreased from the previous ranking."));
                return ind;
            }
        }

    }

    public class LineChart : ReportViewerSampleHelper
    {
        public LineChart(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ReportElement";
            this.ReportName = "Line Chart";
        }

        public override void UpdateDataSet()
        {
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "AdventureWorksXMLDataSet", Value = AdventureWorks.GetData() });
        }

        public class AdventureWorks
        {
            public string SalesPersonID { get; set; }
            public string FullName { get; set; }
            public string Title { get; set; }
            public string SalesTerritory { get; set; }
            public string Y2002 { get; set; }
            public string Y2003 { get; set; }
            public string Y2004 { get; set; }
            public string LastModifiedOn { get; set; }
            public static IList GetData()
            {
                List<AdventureWorks> AdventureWorksCollection = new List<AdventureWorks>();
                AdventureWorks AdventureWork = null;
                AdventureWork = new AdventureWorks()
                {
                    SalesPersonID = "280",
                    FullName = "Pamela",
                    Title = "Sales Representative",
                    SalesTerritory = "Northwest",
                    Y2002 = "1473076.9138",
                    Y2003 = "900368.5797",
                    Y2004 = "1656492.8626",
                    LastModifiedOn = "1999-01-13T00:00:00"
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    SalesPersonID = "281",
                    FullName = "Shu",
                    Title = "Sales Representative",
                    SalesTerritory = "Southwest",
                    Y2002 = "1",
                    Y2003 = "2870320.8578",
                    Y2004 = "3018725.4858",
                    LastModifiedOn = "1999-01-13T00:00:00"
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    SalesPersonID = "282",
                    FullName = "José",
                    Title = "Sales Representative",
                    SalesTerritory = "Canada",
                    Y2002 = "2532500.9127",
                    Y2003 = "1488793.3386",
                    Y2004 = "3189356.2465",
                    LastModifiedOn = "1999-01-13T00:00:00"
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    SalesPersonID = "283",
                    FullName = "David",
                    Title = "Sales Representative",
                    SalesTerritory = "Northwest",
                    Y2002 = "1243580.7691",
                    Y2003 = "1377431.3288",
                    Y2004 = "1930885.5631",
                    LastModifiedOn = "1999-01-13T00:00:00"
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    SalesPersonID = "287",
                    FullName = "Tete",
                    Title = "Sales Representative",
                    SalesTerritory = "Northwest",
                    Y2002 = "1",
                    Y2003 = "883338.7107",
                    Y2004 = "1931620.1835",
                    LastModifiedOn = "1999-01-13T00:00:00"
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    SalesPersonID = "275",
                    FullName = "Michael",
                    Title = "Sales Representative",
                    SalesTerritory = "Northeast",
                    Y2002 = "1951086.8256",
                    Y2003 = "4743906.8935",
                    Y2004 = "4557045.0459",
                    LastModifiedOn = "1999-01-13T00:00:00"
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    SalesPersonID = "276",
                    FullName = "Linda",
                    Title = "Sales Representative",
                    SalesTerritory = "Southwest",
                    Y2002 = "2800029.1538",
                    Y2003 = "4647225.4431",
                    Y2004 = "5200475.2311",
                    LastModifiedOn = "1999-01-13T00:00:00"
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    SalesPersonID = "277",
                    FullName = "Jillian",
                    Title = "Sales Representative",
                    SalesTerritory = "Central",
                    Y2002 = "3308895.8507",
                    Y2003 = "4991867.7074",
                    Y2004 = "3857163.6331",
                    LastModifiedOn = "1999-01-13T00:00:00"
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    SalesPersonID = "278",
                    FullName = "Vargas",
                    Title = "Sales Representative",
                    SalesTerritory = "Canada",
                    Y2002 = "1135639.2632",
                    Y2003 = "1480136.0065",
                    Y2004 = "1764938.9857",
                    LastModifiedOn = "1999-01-13T00:00:00"
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    SalesPersonID = "279",
                    FullName = "Reiter",
                    Title = "Sales Representative",
                    SalesTerritory = "Southeast",
                    Y2002 = "3242697.0127",
                    Y2003 = "2661156.2418",
                    Y2004 = "2811012.7150",
                    LastModifiedOn = "1999-01-13T00:00:00"
                };
                AdventureWorksCollection.Add(AdventureWork);
                return AdventureWorksCollection;
            }
        }

    }

    public class RadialGauge : ReportViewerSampleHelper
    {
        public RadialGauge(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ReportElement";
            this.ReportName = "Radial Gauge";
        }

        public override void UpdateDataSet()
        {
            base.UpdateDataSet();
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet1", Value = SalesDetails.GetData() });
        }

        public class SalesDetails
        {
            public string Name { get; set; }
            public string OrderQty { get; set; }
            public string LineTotal { get; set; }
            public static IList GetData()
            {
                List<SalesDetails> SalesDetailsCollection = new List<SalesDetails>();
                SalesDetails SalesDetail = null;
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "4",
                    LineTotal = "201.0400"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "3",
                    LineTotal = "135.3600"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "3",
                    LineTotal = "136.7415"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "550",
                    LineTotal = "8847.3000"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "3",
                    LineTotal = "171.0765"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "550",
                    LineTotal = "20397.3000"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "550",
                    LineTotal = "14628.0750"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "550",
                    LineTotal = "14882.1750"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "550",
                    LineTotal = "18468.4500"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "550",
                    LineTotal = "25334.9250"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "3",
                    LineTotal = "142.4115"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "3",
                    LineTotal = "136.1115"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "3",
                    LineTotal = "148.9320"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "3",
                    LineTotal = "136.1115"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "3",
                    LineTotal = "129.8115"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "3",
                    LineTotal = "142.5690"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "3",
                    LineTotal = "136.2690"
                };
                SalesDetailsCollection.Add(SalesDetail);
                SalesDetail = new SalesDetails()
                {
                    Name = "International",
                    OrderQty = "3",
                    LineTotal = "149.0895"
                };
                SalesDetailsCollection.Add(SalesDetail);
                return SalesDetailsCollection;
            }
        }


    }

    public class PivotTable : ReportViewerSampleHelper
    {
        public PivotTable(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ReportElement";
            this.ReportName = "Pivot Table";
        }

        public override void UpdateDataSet()
        {
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "Sales", Value = new AdventureWorks().GetData() });
        }

        public class AdventureWorks
        {
            public string ProdCat { get; set; }
            public string SubCat { get; set; }
            public string OrderYear { get; set; }
            public string OrderQtr { get; set; }
            public double Sales { get; set; }
            public IList GetData()
            {
                List<AdventureWorks> AdventureWorksCollection = new List<AdventureWorks>();
                AdventureWorks AdventureWork = null;
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Accessories",
                    SubCat = "Helmets",
                    OrderYear = "2002",
                    OrderQtr = "Q1",
                    Sales = 4945.6925
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = "2002",
                    OrderQtr = "Q3",
                    Sales = 957715.1942
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Forks",
                    OrderYear = "2002",
                    OrderQtr = "Q4",
                    Sales = 23543.1060
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = "2002",
                    OrderQtr = "Q1",
                    Sales = 3171787.6112
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Accessories",
                    SubCat = "Helmets",
                    OrderYear = "2002",
                    OrderQtr = "Q3",
                    Sales = 33853.1033
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Wheels",
                    OrderYear = "2002",
                    OrderQtr = "Q4",
                    Sales = 163921.8870
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = "2003",
                    OrderQtr = "Q2",
                    Sales = 4119658.6506
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Socks",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 6968.6884
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 3734891.6389
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = "2002",
                    OrderQtr = "Q3",
                    Sales = 608352.8754
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Handlebars",
                    OrderYear = "2002",
                    OrderQtr = "Q4",
                    Sales = 18309.4452
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 286591.8208
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Accessories",
                    SubCat = "Tires and Tubes",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 41940.3364
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = "2003",
                    OrderQtr = "Q2",
                    Sales = 440260.9831
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = "2003",
                    OrderQtr = "Q2",
                    Sales = 457688.8401
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Vests",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 66882.6450
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Accessories",
                    SubCat = "Pumps",
                    OrderYear = "2002",
                    OrderQtr = "Q4",
                    Sales = 3226.3860
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = "2003",
                    OrderQtr = "Q2",
                    Sales = 51600.6190
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Chains",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 3476.0176
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Handlebars",
                    OrderYear = "2003",
                    OrderQtr = "Q2",
                    Sales = 17194.2146
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 565229.8810
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 243.7175
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = "2002",
                    OrderQtr = "Q2",
                    Sales = 155311.4063
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = "2002",
                    OrderQtr = "Q2",
                    Sales = 220935.1648
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Accessories",
                    SubCat = "Locks",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 15.0000
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 827287.5234
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Accessories",
                    SubCat = "Bike Racks",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 75920.4000
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Bottom Brackets",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 17453.6400
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Bikes",
                    SubCat = "Touring Bikes",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 3298006.2858
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Brakes",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 18571.4700
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = "2002",
                    OrderQtr = "Q4",
                    Sales = 56782.4280
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Pedals",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 54185.2014
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Jerseys",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 173041.0492
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Jerseys",
                    OrderYear = "2002",
                    OrderQtr = "Q2",
                    Sales = 16931.2362
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Headsets",
                    OrderYear = "2002",
                    OrderQtr = "Q3",
                    Sales = 19701.9001
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = "2002",
                    OrderQtr = "Q4",
                    Sales = 458089.4246
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Shorts",
                    OrderYear = "2003",
                    OrderQtr = "Q1",
                    Sales = 11230.1280
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = "2002",
                    OrderQtr = "Q4",
                    Sales = 4189621.8590
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Brakes",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 26659.0800
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Wheels",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 83.2981
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Vests",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 81085.6900
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Cranksets",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 80244.1372
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Socks",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 6183.1422
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Wheels",
                    OrderYear = "2003",
                    OrderQtr = "Q2",
                    Sales = 163929.9435
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = "2002",
                    OrderQtr = "Q3",
                    Sales = 67088.3037
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 779.8960
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Socks",
                    OrderYear = "2002",
                    OrderQtr = "Q1",
                    Sales = 1273.8550
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = "2002",
                    OrderQtr = "Q3",
                    Sales = 4930692.7825
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Shorts",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 84192.3708
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Jerseys",
                    OrderYear = "2002",
                    OrderQtr = "Q3",
                    Sales = 48901.7598
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Shorts",
                    OrderYear = "2002",
                    OrderQtr = "Q3",
                    Sales = 26207.2314
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = "2002",
                    OrderQtr = "Q2",
                    Sales = 3478963.5378
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Shorts",
                    OrderYear = "2003",
                    OrderQtr = "Q2",
                    Sales = 21423.6288
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Derailleurs",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 25385.2550
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Handlebars",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 21675.6840
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Bottom Brackets",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 13339.1820
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Clothing",
                    SubCat = "Jerseys",
                    OrderYear = "2003",
                    OrderQtr = "Q2",
                    Sales = 31334.6088
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Accessories",
                    SubCat = "Helmets",
                    OrderYear = "2002",
                    OrderQtr = "Q2",
                    Sales = 11638.8628
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Headsets",
                    OrderYear = "2003",
                    OrderQtr = "Q2",
                    Sales = 14102.2548
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Handlebars",
                    OrderYear = "2002",
                    OrderQtr = "Q3",
                    Sales = 35341.0863
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Bikes",
                    SubCat = "Touring Bikes",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 3766585.3623
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Chains",
                    OrderYear = "2003",
                    OrderQtr = "Q4",
                    Sales = 2217.8992
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Accessories",
                    SubCat = "Locks",
                    OrderYear = "2003",
                    OrderQtr = "Q2",
                    Sales = 3939.0000
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 3844123.5588
                };
                AdventureWorksCollection.Add(AdventureWork);
                AdventureWork = new AdventureWorks()
                {
                    ProdCat = "Components",
                    SubCat = "Handlebars",
                    OrderYear = "2003",
                    OrderQtr = "Q3",
                    Sales = 43624.0992
                };
                AdventureWorksCollection.Add(AdventureWork);
                return AdventureWorksCollection;
            }
        }


    }

    public class TemplateList : ReportViewerSampleHelper
    {
        public TemplateList(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ReportElement";
            this.ReportName = "Templated List";
        }

        public override void UpdateDataSet()
        {
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "EmployeeDetails", Value = Employees.GetEmployeeDetails() });
        }

        public class Employees
        {
            public byte[] Photo { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string BirthDate { get; set; }
            public string Address { get; set; }
            public string Title { get; set; }
            public string Region { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string TitleOfCourtesy { get; set; }
            public string ReportingFirstName { get; set; }
            public string ReportingLastName { get; set; }
            public string HireDate { get; set; }
            public string Notes { get; set; }
            public string HomePhone { get; set; }
            public static IList GetEmployeeDetails()
            {
                List<Employees> EmployeeCollection = new List<Employees>();
                Employees employee = null;
                employee = new Employees()
                {
                    Photo = System.Convert.FromBase64String(@"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"),
                    FirstName = "Nancy",
                    LastName = "Davolio",
                    BirthDate = "Wednesday, 8 December 1948",
                    Address = "507 - 20th Ave. E.Apt. 2A",
                    Title = "Sales Representative",
                    Region = "WA",
                    City = "Seattle",
                    Country = "USA",
                    TitleOfCourtesy = "Ms.",
                    ReportingFirstName = "Andrew",
                    ReportingLastName = "Fuller",
                    HireDate = "Friday, 1 May 1992",
                    Notes = "Education includes a BA in psychology from Colorado State University in 1970.  She also completed \"The Art of the Cold Call.\"  Nancy is a member of Toastmasters International.",
                    HomePhone = "(206) 555-9857"
                };
                EmployeeCollection.Add(employee);
                employee = new Employees()
                {
                    Photo = System.Convert.FromBase64String(@"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"),
                    FirstName = "Janet",
                    LastName = "Leverling",
                    BirthDate = "Friday, 30 August 1963",
                    Address = "722 Moss Bay Blvd.",
                    Title = "Sales Representative",
                    Region = "WA",
                    City = "Kirkland",
                    Country = "USA",
                    TitleOfCourtesy = "Ms.",
                    ReportingFirstName = "Andrew",
                    ReportingLastName = "Fuller",
                    HireDate = "Wednesday, 1 April 1992",
                    Notes = "Janet has a BS degree in chemistry from Boston College (1984).  She has also completed a certificate program in food retailing management.  Janet was hired as a sales associate in 1991 and promoted to sales representative in February 1992.",
                    HomePhone = "(206) 555-3412"
                };
                EmployeeCollection.Add(employee);
                employee = new Employees()
                {
                    Photo = System.Convert.FromBase64String(@"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"),
                    FirstName = "Margaret",
                    LastName = "Peacock",
                    BirthDate = "Sunday, 19 September 1937",
                    Address = "4110 Old Redmond Rd.",
                    Title = "Sales Representative",
                    Region = "WA",
                    City = "Redmond",
                    Country = "USA",
                    TitleOfCourtesy = "Mrs.",
                    ReportingFirstName = "Andrew",
                    ReportingLastName = "Fuller",
                    HireDate = "Monday, 3 May 1993",
                    Notes = "Margaret holds a BA in English literature from Concordia College (1958) and an MA from the American Institute of Culinary Arts (1966).  She was assigned to the London office temporarily from July through November 1992.",
                    HomePhone = "(206) 555-8122"
                };
                EmployeeCollection.Add(employee);
                employee = new Employees()
                {
                    Photo = System.Convert.FromBase64String(@"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"),
                    FirstName = "Steven",
                    LastName = "Buchanan",
                    BirthDate = "Friday, 4 March 1955",
                    Address = "14 Garrett Hill",
                    Title = "Sales Manager",
                    Region = "",
                    City = "London",
                    Country = "UK",
                    TitleOfCourtesy = "Mr.",
                    ReportingFirstName = "Andrew",
                    ReportingLastName = "Fuller",
                    HireDate = "Sunday, 17 October 1993",
                    Notes = "Steven Buchanan graduated from St. Andrews University, Scotland, with a BSC degree in 1976.  Upon joining the company as a sales representative in 1992, he spent 6 months in an orientation program at the Seattle office and then returned to his permanent post in London.  He was promoted to sales manager in March 1993.  Mr. Buchanan has completed the course \"Successful Telemarketing\".\"  He is fluent in French.",
                    HomePhone = "(71) 555-4848"
                };
                EmployeeCollection.Add(employee);
                employee = new Employees()
                {
                    Photo = System.Convert.FromBase64String(@"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"),
                    FirstName = "Michael",
                    LastName = "Suyama",
                    BirthDate = "Tuesday, 2 July 1963",
                    Address = "Coventry House Miner Rd.",
                    Title = "Sales Representative",
                    Region = "",
                    City = "London",
                    Country = "UK",
                    TitleOfCourtesy = "Mr.",
                    ReportingFirstName = "Steven",
                    ReportingLastName = "Buchanan",
                    HireDate = "Sunday, 17 October 1993",
                    Notes = "Michael is a graduate of Sussex University (MA, economics, 1983) and the University of California at Los Angeles (MBA, marketing, 1986).  He has also taken the courses \"Multi-Cultural Selling\" and \"Time Management for the Sales Professional.\"  He is fluent in Japanese and can read and write French, Portuguese, and Spanish.",
                    HomePhone = "(71) 555-7773"
                };
                EmployeeCollection.Add(employee);
                employee = new Employees()
                {
                    Photo = System.Convert.FromBase64String(@"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"),
                    FirstName = "Robert",
                    LastName = "King",
                    BirthDate = "Sunday, 29 May 1960",
                    Address = "Edgeham Hollow Winchester Way",
                    Title = "Sales Representative",
                    Region = "",
                    City = "London",
                    Country = "UK",
                    TitleOfCourtesy = "Mr.",
                    ReportingFirstName = "Steven",
                    ReportingLastName = "Buchanan",
                    HireDate = "Sunday, 2 January 1994",
                    Notes = "Robert King served in the Peace Corps and traveled extensively before completing his degree in English at the University of Michigan in 1992, the year he joined the company.  After completing a course entitled \"Selling in Europe,\" he was transferred to the London office in March 1993.",
                    HomePhone = "(71) 555-5598"
                };
                EmployeeCollection.Add(employee);
                employee = new Employees()
                {
                    Photo = System.Convert.FromBase64String(@"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"),
                    FirstName = "Laura",
                    LastName = "Callahan",
                    BirthDate = "Thursday, 9 January 1958",
                    Address = "4726 - 11th Ave. N.E.",
                    Title = "Inside Sales Coordinator",
                    Region = "WA",
                    City = "Seattle",
                    Country = "USA",
                    TitleOfCourtesy = "Ms.",
                    ReportingFirstName = "Andrew",
                    ReportingLastName = "Fuller",
                    HireDate = "Saturday, 5 March 1994",
                    Notes = "Laura received a BA in psychology from the University of Washington.  She has also completed a course in business French.  She reads and writes French.",
                    HomePhone = "(206) 555-1189"
                };
                EmployeeCollection.Add(employee);
                employee = new Employees()
                {
                    Photo = System.Convert.FromBase64String(@"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"),
                    FirstName = "Anne",
                    LastName = "Dodsworth",
                    BirthDate = "Thursday, 27 January 1966",
                    Address = "7 Houndstooth Rd.",
                    Title = "Sales Representative",
                    Region = "",
                    City = "London",
                    Country = "UK",
                    TitleOfCourtesy = "Ms.",
                    ReportingFirstName = "Steven",
                    ReportingLastName = "Buchanan",
                    HireDate = "Tuesday, 15 November 1994",
                    Notes = "Anne has a BA degree in English from St. Lawrence College.  She is fluent in French and German.",
                    HomePhone = "(71) 555-4444"
                };
                EmployeeCollection.Add(employee);
                return EmployeeCollection;
            }
        }



    }

    public class TableSummaries : ReportViewerSampleHelper
    {
        public TableSummaries(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ReportElement";
            this.ReportName = "Table Summaries";
        }

        public override void UpdateDataSet()
        {
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "Sales", Value = SalesDetails.GetData() });
        }

        public class SalesDetails
        {
            public string ProdCat { get; set; }
            public string SubCat { get; set; }
            public double? OrderYear { get; set; }
            public string OrderQtr { get; set; }
            public double? Sales { get; set; }
            public static IList GetData()
            {
                List<SalesDetails> datas = new List<SalesDetails>();
                SalesDetails data = null;
                data = new SalesDetails()
                {
                    ProdCat = "Accessories",
                    SubCat = "Helmets",
                    OrderYear = 2002,
                    OrderQtr = "Q1",
                    Sales = 4945.6925
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 957715.1942
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Forks",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 23543.1060
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = 2002,
                    OrderQtr = "Q1",
                    Sales = 3171787.6112
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Accessories",
                    SubCat = "Helmets",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 33853.1033
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Wheels",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 163921.8870
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = 2003,
                    OrderQtr = "Q2",
                    Sales = 4119658.6506
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Socks",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 6968.6884
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 3734891.6389
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 608352.8754
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Handlebars",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 18309.4452
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 286591.8208
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Accessories",
                    SubCat = "Tires and Tubes",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 41940.3364
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = 2003,
                    OrderQtr = "Q2",
                    Sales = 440260.9831
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = 2003,
                    OrderQtr = "Q2",
                    Sales = 457688.8401
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Vests",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 66882.6450
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Accessories",
                    SubCat = "Pumps",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 3226.3860
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = 2003,
                    OrderQtr = "Q2",
                    Sales = 51600.6190
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Chains",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 3476.0176
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Handlebars",
                    OrderYear = 2003,
                    OrderQtr = "Q2",
                    Sales = 17194.2146
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 565229.8810
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 243.7175
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = 2002,
                    OrderQtr = "Q2",
                    Sales = 155311.4063
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = 2002,
                    OrderQtr = "Q2",
                    Sales = 220935.1648
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Accessories",
                    SubCat = "Locks",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 15.0000
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 827287.5234
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Accessories",
                    SubCat = "Bike Racks",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 75920.4000
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Bottom Brackets",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 17453.6400
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Bikes",
                    SubCat = "Touring Bikes",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 3298006.2858
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Brakes",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 18571.4700
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 56782.4280
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Pedals",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 54185.2014
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Jerseys",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 173041.0492
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Jerseys",
                    OrderYear = 2002,
                    OrderQtr = "Q2",
                    Sales = 16931.2362
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Headsets",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 19701.9001
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 458089.4246
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Shorts",
                    OrderYear = 2003,
                    OrderQtr = "Q1",
                    Sales = 11230.1280
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 4189621.8590
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Brakes",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 26659.0800
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Wheels",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 83.2981
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Vests",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 81085.6900
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Cranksets",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 80244.1372
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Socks",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 6183.1422
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Wheels",
                    OrderYear = 2003,
                    OrderQtr = "Q2",
                    Sales = 163929.9435
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 67088.3037
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 779.8960
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Socks",
                    OrderYear = 2002,
                    OrderQtr = "Q1",
                    Sales = 1273.8550
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 4930692.7825
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Shorts",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 84192.3708
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Jerseys",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 48901.7598
                };
                datas.Add(data);
                return datas;
            }
        }

    }

    public class GroupingTable : ReportViewerSampleHelper
    {
        public GroupingTable(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ReportElement";
            this.ReportName = "Grouping Table";
        }

        public override void UpdateDataSet()
        {
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "Customers", Value = Customers.GetData() });
        }

        public class Customers
        {
            public string SalesOrderNumber { get; set; }
            public string Store { get; set; }
            public DateTime OrderDate { get; set; }
            public string SalesFirstName { get; set; }
            public string SalesLastName { get; set; }
            public string SalesTitle { get; set; }
            public string PurchaseOrderNumber { get; set; }
            public string ShipMethod { get; set; }
            public string BillAddress1 { get; set; }
            public string BillAddress2 { get; set; }
            public string BillCity { get; set; }
            public string BillPostalCode { get; set; }
            public string BillStateProvince { get; set; }
            public string BillCountryRegion { get; set; }
            public string ShipAddress1 { get; set; }
            public string ShipAddress2 { get; set; }
            public string ShipCity { get; set; }
            public string ShipPostalCode { get; set; }
            public string ShipStateProvince { get; set; }
            public string ShipCountryRegion { get; set; }
            public string CustPhone { get; set; }
            public string CustFirstName { get; set; }
            public string CustLastName { get; set; }
            public static IList GetData()
            {
                List<Customers> datas = new List<Customers>();
                Customers data = null;
                data = new Customers()
                {
                    SalesOrderNumber = "SO43666",
                    Store = "Wheel Gallery",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Linda",
                    SalesLastName = "Mitchell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO16008173883",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "9920 Bridgepointe Parkway",
                    BillAddress2 = "",
                    BillCity = "San Mateo",
                    BillPostalCode = "94404",
                    BillStateProvince = "California",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "9920 Bridgepointe Parkway",
                    ShipAddress2 = "",
                    ShipCity = "San Mateo",
                    ShipPostalCode = "94404",
                    ShipStateProvince = "California",
                    ShipCountryRegion = "United States",
                    CustPhone = "926-555-0136",
                    CustFirstName = "Abraham",
                    CustLastName = "Swearengin"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43672",
                    Store = "Red Bicycle Company",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO13862153537",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "99, Rue Saint-pierre",
                    BillAddress2 = "",
                    BillCity = "Pnot-Rouge",
                    BillPostalCode = "J1E 2T7",
                    BillStateProvince = "Quebec",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "99, Rue Saint-pierre",
                    ShipAddress2 = "",
                    ShipCity = "Pnot-Rouge",
                    ShipPostalCode = "J1E 2T7",
                    ShipStateProvince = "Quebec",
                    ShipCountryRegion = "Canada",
                    CustPhone = "667-555-0112",
                    CustFirstName = "Phyllis",
                    CustLastName = "Thomas"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43685",
                    Store = "Simple Bike Parts",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO4176124783",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Silver Sands Factory Outlet",
                    BillAddress2 = "",
                    BillCity = "Destin",
                    BillPostalCode = "32541",
                    BillStateProvince = "Florida",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Silver Sands Factory Outlet",
                    ShipAddress2 = "",
                    ShipCity = "Destin",
                    ShipPostalCode = "32541",
                    ShipStateProvince = "Florida",
                    ShipCountryRegion = "United States",
                    CustPhone = "317-555-0163",
                    CustFirstName = "Abe",
                    CustLastName = "Tramel"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43843",
                    Store = "Catalog Store",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO19923118772",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "855 East Main Avenue",
                    BillAddress2 = "",
                    BillCity = "Zeeland",
                    BillPostalCode = "49464",
                    BillStateProvince = "Michigan",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "855 East Main Avenue",
                    ShipAddress2 = "",
                    ShipCity = "Zeeland",
                    ShipPostalCode = "49464",
                    ShipStateProvince = "Michigan",
                    ShipCountryRegion = "United States",
                    CustPhone = "440-555-0132",
                    CustFirstName = "David",
                    CustLastName = "Liu"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43844",
                    Store = "Two-Wheeled Transit Company",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO19691138342",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "P.O. Box 44000",
                    BillAddress2 = "",
                    BillCity = "Winnipeg",
                    BillPostalCode = "R3",
                    BillStateProvince = "Manitoba",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "P.O. Box 44000",
                    ShipAddress2 = "",
                    ShipCity = "Winnipeg",
                    ShipPostalCode = "R3",
                    ShipStateProvince = "Manitoba",
                    ShipCountryRegion = "Canada",
                    CustPhone = "700-555-0155",
                    CustFirstName = "Joan",
                    CustLastName = "Campbell"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43845",
                    Store = "New and Used Bicycles",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO19546184286",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "3990 Silas Creek Parkway",
                    BillAddress2 = "",
                    BillCity = "Winston-Salem",
                    BillPostalCode = "27104",
                    BillStateProvince = "North Carolina",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "3990 Silas Creek Parkway",
                    ShipAddress2 = "",
                    ShipCity = "Winston-Salem",
                    ShipPostalCode = "27104",
                    ShipStateProvince = "North Carolina",
                    ShipCountryRegion = "United States",
                    CustPhone = "895-555-0160",
                    CustFirstName = "Brannon",
                    CustLastName = "Jones"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43846",
                    Store = "First-Rate Outlet",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Garrett",
                    SalesLastName = "Vargas",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO19430112391",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "25537 Hillside Avenue",
                    BillAddress2 = "",
                    BillCity = "Victoria",
                    BillPostalCode = "V8V",
                    BillStateProvince = "British Columbia",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "25537 Hillside Avenue",
                    ShipAddress2 = "",
                    ShipCity = "Victoria",
                    ShipPostalCode = "V8V",
                    ShipStateProvince = "British Columbia",
                    ShipCountryRegion = "Canada",
                    CustPhone = "277-555-0169",
                    CustFirstName = "Ann",
                    CustLastName = "Beebe"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43847",
                    Store = "Gasless Cycle Shop",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Garrett",
                    SalesLastName = "Vargas",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO19227161888",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "25 First Canadian Place",
                    BillAddress2 = "",
                    BillCity = "Toronto",
                    BillPostalCode = "M4B 1V5",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "25 First Canadian Place",
                    ShipAddress2 = "",
                    ShipCity = "Toronto",
                    ShipPostalCode = "M4B 1V5",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "584-555-0192",
                    CustFirstName = "Josh",
                    CustLastName = "Barnhill"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43848",
                    Store = "Suburban Cycle Shop",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "David",
                    SalesLastName = "Campbell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO18908190536",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "994 Sw Cherry Park Rd",
                    BillAddress2 = "",
                    BillCity = "Troutdale",
                    BillPostalCode = "97060",
                    BillStateProvince = "Oregon",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "994 Sw Cherry Park Rd",
                    ShipAddress2 = "",
                    ShipCity = "Troutdale",
                    ShipPostalCode = "97060",
                    ShipStateProvince = "Oregon",
                    ShipCountryRegion = "United States",
                    CustPhone = "706-555-0140",
                    CustFirstName = "Cindy",
                    CustLastName = "Dodd"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43849",
                    Store = "Brakes and Gears",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Stephen",
                    SalesLastName = "Jiang",
                    SalesTitle = "North American Sales Manager",
                    PurchaseOrderNumber = "PO18676186169",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "9927 N. Main St.",
                    BillAddress2 = "",
                    BillCity = "Tooele",
                    BillPostalCode = "84074",
                    BillStateProvince = "Utah",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "9927 N. Main St.",
                    ShipAddress2 = "",
                    ShipCity = "Tooele",
                    ShipPostalCode = "84074",
                    ShipStateProvince = "Utah",
                    ShipCountryRegion = "United States",
                    CustPhone = "774-555-0133",
                    CustFirstName = "Roger",
                    CustLastName = "Harui"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43850",
                    Store = "Non-Slip Pedal Company",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO18415143340",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "258 King Street East",
                    BillAddress2 = "",
                    BillCity = "Toronto",
                    BillPostalCode = "M4B 1V7",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "258 King Street East",
                    ShipAddress2 = "",
                    ShipCity = "Toronto",
                    ShipPostalCode = "M4B 1V7",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "303-555-0117",
                    CustFirstName = "Sandra",
                    CustLastName = "Kitt"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43851",
                    Store = "Retail Sales and Service",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO18386167654",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Lake Region Factory Outlet",
                    BillAddress2 = "",
                    BillCity = "Tilton",
                    BillPostalCode = "03276",
                    BillStateProvince = "New Hampshire",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Lake Region Factory Outlet",
                    ShipAddress2 = "",
                    ShipCity = "Tilton",
                    ShipPostalCode = "03276",
                    ShipStateProvince = "New Hampshire",
                    ShipCountryRegion = "United States",
                    CustPhone = "607-555-0193",
                    CustFirstName = "Michael",
                    CustLastName = "Allen"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43852",
                    Store = "Economy Center",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO17864179720",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "6th Floor, 25st Canadian Place",
                    BillAddress2 = "",
                    BillCity = "Toronto",
                    BillPostalCode = "M4B 1V5",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "6th Floor, 25st Canadian Place",
                    ShipAddress2 = "",
                    ShipCity = "Toronto",
                    ShipPostalCode = "M4B 1V5",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "555-555-0162",
                    CustFirstName = "Curtis",
                    CustLastName = "Howard"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43853",
                    Store = "Sharp Bikes",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO18270155899",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "52560 Free Street",
                    BillAddress2 = "",
                    BillCity = "Toronto",
                    BillPostalCode = "M4B 1V7",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "52560 Free Street",
                    ShipAddress2 = "",
                    ShipCity = "Toronto",
                    ShipPostalCode = "M4B 1V7",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "926-555-0159",
                    CustFirstName = "Katherine",
                    CustLastName = "Harding"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43854",
                    Store = "Professional Cyclists",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO17777139245",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "2545 King Street West",
                    BillAddress2 = "",
                    BillCity = "Toronto",
                    BillPostalCode = "M4B 1V7",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "2545 King Street West",
                    ShipAddress2 = "",
                    ShipCity = "Toronto",
                    ShipPostalCode = "M4B 1V7",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "154-555-0115",
                    CustFirstName = "Steve",
                    CustLastName = "Masters"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43855",
                    Store = "National Manufacturing",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Garrett",
                    SalesLastName = "Vargas",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO17748116016",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "630 University Avenue",
                    BillAddress2 = "",
                    BillCity = "Toronto",
                    BillPostalCode = "M4B 1V7",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "630 University Avenue",
                    ShipAddress2 = "",
                    ShipCity = "Toronto",
                    ShipPostalCode = "M4B 1V7",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "493-555-0134",
                    CustFirstName = "Linda",
                    CustLastName = "Leste"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43856",
                    Store = "Sixth Bike Store",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO17313123131",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Suite 25800 3401 - 10810th Avenue",
                    BillAddress2 = "",
                    BillCity = "Surrey",
                    BillPostalCode = "V3T 4W3",
                    BillStateProvince = "British Columbia",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "Suite 25800 3401 - 10810th Avenue",
                    ShipAddress2 = "",
                    ShipCity = "Surrey",
                    ShipPostalCode = "V3T 4W3",
                    ShipStateProvince = "British Columbia",
                    ShipCountryRegion = "Canada",
                    CustPhone = "428-555-0176",
                    CustFirstName = "Dorothy",
                    CustLastName = "Contreras"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43857",
                    Store = "Tenth Bike Store",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO16733124458",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Depot 80",
                    BillAddress2 = "",
                    BillCity = "Sillery",
                    BillPostalCode = "G1T",
                    BillStateProvince = "Quebec",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "Depot 80",
                    ShipAddress2 = "",
                    ShipCity = "Sillery",
                    ShipPostalCode = "G1T",
                    ShipStateProvince = "Quebec",
                    ShipCountryRegion = "Canada",
                    CustPhone = "744-555-0123",
                    CustFirstName = "Yuping",
                    CustLastName = "Tian"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43858",
                    Store = "Exotic Bikes",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO16791124272",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "6900 William Richardson Ct.",
                    BillAddress2 = "",
                    BillCity = "South Bend",
                    BillPostalCode = "46601",
                    BillStateProvince = "Indiana",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "6900 William Richardson Ct.",
                    ShipAddress2 = "",
                    ShipCity = "South Bend",
                    ShipPostalCode = "46601",
                    ShipStateProvince = "Indiana",
                    ShipCountryRegion = "United States",
                    CustPhone = "415-555-0147",
                    CustFirstName = "John",
                    CustLastName = "Long"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43859",
                    Store = "Highway Bike Shop",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Linda",
                    SalesLastName = "Mitchell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO16762199940",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Simi @ The Plaza",
                    BillAddress2 = "",
                    BillCity = "Simi Valley",
                    BillPostalCode = "93065",
                    BillStateProvince = "California",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Simi @ The Plaza",
                    ShipAddress2 = "",
                    ShipCity = "Simi Valley",
                    ShipPostalCode = "93065",
                    ShipStateProvince = "California",
                    ShipCountryRegion = "United States",
                    CustPhone = "199-555-0135",
                    CustFirstName = "Lucio",
                    CustLastName = "Iallo"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43860",
                    Store = "A Bike Store",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Pamela",
                    SalesLastName = "Ansman-Wolfe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO16646146654",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "2251 Elliot Avenue",
                    BillAddress2 = "",
                    BillCity = "Seattle",
                    BillPostalCode = "98104",
                    BillStateProvince = "Washington",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "2251 Elliot Avenue",
                    ShipAddress2 = "",
                    ShipCity = "Seattle",
                    ShipPostalCode = "98104",
                    ShipStateProvince = "Washington",
                    ShipCountryRegion = "United States",
                    CustPhone = "245-555-0173",
                    CustFirstName = "Orlando",
                    CustLastName = "Gee"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43861",
                    Store = "Qualified Sales and Repair Services",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO16327172067",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Savannah Festival Outlet",
                    BillAddress2 = "",
                    BillCity = "Savannah",
                    BillPostalCode = "31401",
                    BillStateProvince = "Georgia",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Savannah Festival Outlet",
                    ShipAddress2 = "",
                    ShipCity = "Savannah",
                    ShipPostalCode = "31401",
                    ShipStateProvince = "Georgia",
                    ShipCountryRegion = "United States",
                    CustPhone = "475-555-0188",
                    CustFirstName = "Hanying",
                    CustLastName = "Feng"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43862",
                    Store = "Unified Sports Company",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO16211194171",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "9876 Fruitville Rd",
                    BillAddress2 = "",
                    BillCity = "Sarasota",
                    BillPostalCode = "34236",
                    BillStateProvince = "Florida",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "9876 Fruitville Rd",
                    ShipAddress2 = "",
                    ShipCity = "Sarasota",
                    ShipPostalCode = "34236",
                    ShipStateProvince = "Florida",
                    ShipCountryRegion = "United States",
                    CustPhone = "296-555-0171",
                    CustFirstName = "Gloria",
                    CustLastName = "Lesko"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43863",
                    Store = "Social Activities Club",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Jillian",
                    SalesLastName = "Carson",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO15747169584",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "23025 S.W. Military Rd.",
                    BillAddress2 = "",
                    BillCity = "San Antonio",
                    BillPostalCode = "78204",
                    BillStateProvince = "Texas",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "23025 S.W. Military Rd.",
                    ShipAddress2 = "",
                    ShipCity = "San Antonio",
                    ShipPostalCode = "78204",
                    ShipStateProvince = "Texas",
                    ShipCountryRegion = "United States",
                    CustPhone = "596-555-0153",
                    CustFirstName = "John",
                    CustLastName = "Ford"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43864",
                    Store = "Sparkling Paint and Finishes",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Linda",
                    SalesLastName = "Mitchell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO16037151094",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "San Diego Factory",
                    BillAddress2 = "",
                    BillCity = "San Ysidro",
                    BillPostalCode = "92173",
                    BillStateProvince = "California",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "San Diego Factory",
                    ShipAddress2 = "",
                    ShipCity = "San Ysidro",
                    ShipPostalCode = "92173",
                    ShipStateProvince = "California",
                    ShipCountryRegion = "United States",
                    CustPhone = "787-555-0128",
                    CustFirstName = "Clarence",
                    CustLastName = "Tatman"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43865",
                    Store = "Totes & Baskets Company",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Jillian",
                    SalesLastName = "Carson",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO15689147174",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "72540 Blanco Rd.",
                    BillAddress2 = "",
                    BillCity = "San Antonio",
                    BillPostalCode = "78204",
                    BillStateProvince = "Texas",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "72540 Blanco Rd.",
                    ShipAddress2 = "",
                    ShipCity = "San Antonio",
                    ShipPostalCode = "78204",
                    ShipStateProvince = "Texas",
                    ShipCountryRegion = "United States",
                    CustPhone = "560-555-0171",
                    CustFirstName = "Robert",
                    CustLastName = "Vessa"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43866",
                    Store = "Moderately-Priced Bikes Store",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Pamela",
                    SalesLastName = "Ansman-Wolfe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO14529112624",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "67 Rainer Ave S",
                    BillAddress2 = "",
                    BillCity = "Renton",
                    BillPostalCode = "98055",
                    BillStateProvince = "Washington",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "67 Rainer Ave S",
                    ShipAddress2 = "",
                    ShipCity = "Renton",
                    ShipPostalCode = "98055",
                    ShipStateProvince = "Washington",
                    ShipCountryRegion = "United States",
                    CustPhone = "763-555-0145",
                    CustFirstName = "Gabriel",
                    CustLastName = "Bockenkamp"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43867",
                    Store = "Raw Materials Inc",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Pamela",
                    SalesLastName = "Ansman-Wolfe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO14471123403",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Bldg. 9n/99298",
                    BillAddress2 = "",
                    BillCity = "Redmond",
                    BillPostalCode = "98052",
                    BillStateProvince = "Washington",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Bldg. 9n/99298",
                    ShipAddress2 = "",
                    ShipCity = "Redmond",
                    ShipPostalCode = "98052",
                    ShipStateProvince = "Washington",
                    ShipCountryRegion = "United States",
                    CustPhone = "962-555-0166",
                    CustFirstName = "Ted",
                    CustLastName = "Bremer"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43868",
                    Store = "Major Cycling",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO14848158712",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "46990 Viking Way",
                    BillAddress2 = "",
                    BillCity = "Richmond",
                    BillPostalCode = "V6B 3P7",
                    BillStateProvince = "British Columbia",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "46990 Viking Way",
                    ShipAddress2 = "",
                    ShipCity = "Richmond",
                    ShipPostalCode = "V6B 3P7",
                    ShipStateProvince = "British Columbia",
                    ShipCountryRegion = "Canada",
                    CustPhone = "512-555-0122",
                    CustFirstName = "Ruby Sue",
                    CustLastName = "Styles"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43869",
                    Store = "Permanent Finish Products",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Linda",
                    SalesLastName = "Mitchell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO14500145975",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "62500 Neil Road",
                    BillAddress2 = "",
                    BillCity = "Reno",
                    BillPostalCode = "89502",
                    BillStateProvince = "Nevada",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "62500 Neil Road",
                    ShipAddress2 = "",
                    ShipCity = "Reno",
                    ShipPostalCode = "89502",
                    ShipStateProvince = "Nevada",
                    ShipCountryRegion = "United States",
                    CustPhone = "265-555-0143",
                    CustFirstName = "Margaret",
                    CustLastName = "Vanderkamp"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43870",
                    Store = "Wholesale Bikes",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO14326149236",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "58 Teed Drive",
                    BillAddress2 = "",
                    BillCity = "Randolph",
                    BillPostalCode = "02368",
                    BillStateProvince = "Massachusetts",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "58 Teed Drive",
                    ShipAddress2 = "",
                    ShipCity = "Randolph",
                    ShipPostalCode = "02368",
                    ShipStateProvince = "Massachusetts",
                    ShipCountryRegion = "United States",
                    CustPhone = "652-555-0115",
                    CustFirstName = "Aaron",
                    CustLastName = "Con"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43871",
                    Store = "Fun Times Club",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Shu",
                    SalesLastName = "Ito",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO13572145817",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "8525 South Parker Road",
                    BillAddress2 = "",
                    BillCity = "Parker",
                    BillPostalCode = "80138",
                    BillStateProvince = "Colorado",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "8525 South Parker Road",
                    ShipAddress2 = "",
                    ShipCity = "Parker",
                    ShipPostalCode = "80138",
                    ShipStateProvince = "Colorado",
                    ShipCountryRegion = "United States",
                    CustPhone = "847-555-0184",
                    CustFirstName = "Diane",
                    CustLastName = "Tibbott"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43872",
                    Store = "Wire Baskets and Parts",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Linda",
                    SalesLastName = "Mitchell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO12557127067",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Mall Of Orange",
                    BillAddress2 = "",
                    BillCity = "Orange",
                    BillPostalCode = "92867",
                    BillStateProvince = "California",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Mall Of Orange",
                    ShipAddress2 = "",
                    ShipCity = "Orange",
                    ShipPostalCode = "92867",
                    ShipStateProvince = "California",
                    ShipCountryRegion = "United States",
                    CustPhone = "103-555-0179",
                    CustFirstName = "Jessie",
                    CustLastName = "Valerio"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43873",
                    Store = "Preferred Bikes",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Linda",
                    SalesLastName = "Mitchell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO12499138177",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Incom Sports Center",
                    BillAddress2 = "",
                    BillCity = "Ontario",
                    BillPostalCode = "91764",
                    BillStateProvince = "California",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Incom Sports Center",
                    ShipAddress2 = "",
                    ShipCity = "Ontario",
                    ShipPostalCode = "91764",
                    ShipStateProvince = "California",
                    ShipCountryRegion = "United States",
                    CustPhone = "819-555-0186",
                    CustFirstName = "Stefan",
                    CustLastName = "Delmarco"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43874",
                    Store = "Travel Systems",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Jillian",
                    SalesLastName = "Carson",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO12122162917",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "2505 Gateway Drive",
                    BillAddress2 = "",
                    BillCity = "North Sioux City",
                    BillPostalCode = "57049",
                    BillStateProvince = "South Dakota",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "2505 Gateway Drive",
                    ShipAddress2 = "",
                    ShipCity = "North Sioux City",
                    ShipPostalCode = "57049",
                    ShipStateProvince = "South Dakota",
                    ShipCountryRegion = "United States",
                    CustPhone = "121-555-0121",
                    CustFirstName = "Linda",
                    CustLastName = "Burnett"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43696",
                    Store = "Retail Toy Store",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO9947131800",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Dadeland Mall, Space 25090",
                    BillAddress2 = "",
                    BillCity = "Miami",
                    BillPostalCode = "33127",
                    BillStateProvince = "Florida",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Dadeland Mall, Space 25090",
                    ShipAddress2 = "",
                    ShipCity = "Miami",
                    ShipPostalCode = "33127",
                    ShipStateProvince = "Florida",
                    ShipCountryRegion = "United States",
                    CustPhone = "140-555-0188",
                    CustFirstName = "Barbara",
                    CustLastName = "Hoffman"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43695",
                    Store = "Sports Sales and Rental",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO10179176559",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "100 Fifth Drive",
                    BillAddress2 = "",
                    BillCity = "Millington",
                    BillPostalCode = "38054",
                    BillStateProvince = "Tennessee",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "100 Fifth Drive",
                    ShipAddress2 = "",
                    ShipCity = "Millington",
                    ShipPostalCode = "38054",
                    ShipStateProvince = "Tennessee",
                    ShipCountryRegion = "United States",
                    CustPhone = "284-555-0185",
                    CustFirstName = "Run",
                    CustLastName = "Liu"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43694",
                    Store = "Juvenile Sports Equipment",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO9657130250",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "78025 E. Mercy Island Cswy",
                    BillAddress2 = "",
                    BillCity = "Merritt Island",
                    BillPostalCode = "32952",
                    BillStateProvince = "Florida",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "78025 E. Mercy Island Cswy",
                    ShipAddress2 = "",
                    ShipCity = "Merritt Island",
                    ShipPostalCode = "32952",
                    ShipStateProvince = "Florida",
                    ShipCountryRegion = "United States",
                    CustPhone = "843-555-0175",
                    CustFirstName = "Shane",
                    CustLastName = "Belli"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43693",
                    Store = "Clamps & Brackets Co.",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO8120182325",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Leesburg Premium Outlet Centre",
                    BillAddress2 = "",
                    BillCity = "Leesburg",
                    BillPostalCode = "20176",
                    BillStateProvince = "Virginia",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Leesburg Premium Outlet Centre",
                    ShipAddress2 = "",
                    ShipCity = "Leesburg",
                    ShipPostalCode = "20176",
                    ShipStateProvince = "Virginia",
                    ShipCountryRegion = "United States",
                    CustPhone = "107-555-0138",
                    CustFirstName = "Carla",
                    CustLastName = "Adams"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43692",
                    Store = "Bike Dealers Association",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Shu",
                    SalesLastName = "Ito",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO7859187017",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "9952 E. Lohman Ave.",
                    BillAddress2 = "",
                    BillCity = "Las Cruces",
                    BillPostalCode = "88001",
                    BillStateProvince = "New Mexico",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "9952 E. Lohman Ave.",
                    ShipAddress2 = "",
                    ShipCity = "Las Cruces",
                    ShipPostalCode = "88001",
                    ShipStateProvince = "New Mexico",
                    ShipCountryRegion = "United States",
                    CustPhone = "993-555-0179",
                    CustFirstName = "Sandra",
                    CustLastName = "Maynard"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43691",
                    Store = "Grease and Oil Products Company",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Jillian",
                    SalesLastName = "Carson",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO6409111675",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "9903 Highway 6 South",
                    BillAddress2 = "",
                    BillCity = "Houston",
                    BillPostalCode = "77003",
                    BillStateProvince = "Texas",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "9903 Highway 6 South",
                    ShipAddress2 = "",
                    ShipCity = "Houston",
                    ShipPostalCode = "77003",
                    ShipStateProvince = "Texas",
                    ShipCountryRegion = "United States",
                    CustPhone = "126-555-0172",
                    CustFirstName = "Michael",
                    CustLastName = "Blythe"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43690",
                    Store = "Small Cycle Store",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO6235146326",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Horizon Outlet Center",
                    BillAddress2 = "",
                    BillCity = "Holland",
                    BillPostalCode = "49423",
                    BillStateProvince = "Michigan",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Horizon Outlet Center",
                    ShipAddress2 = "",
                    ShipCity = "Holland",
                    ShipPostalCode = "49423",
                    ShipStateProvince = "Michigan",
                    ShipCountryRegion = "United States",
                    CustPhone = "583-555-0130",
                    CustFirstName = "Douglas",
                    CustLastName = "Baldwin"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43689",
                    Store = "Fitness Toy Store",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Jillian",
                    SalesLastName = "Carson",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO5626159507",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "220 Mercy Drive",
                    BillAddress2 = "",
                    BillCity = "Garland",
                    BillPostalCode = "75040",
                    BillStateProvince = "Texas",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "220 Mercy Drive",
                    ShipAddress2 = "",
                    ShipCity = "Garland",
                    ShipPostalCode = "75040",
                    ShipStateProvince = "Texas",
                    ShipCountryRegion = "United States",
                    CustPhone = "351-555-0131",
                    CustFirstName = "Stacey",
                    CustLastName = "Cereghino"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43688",
                    Store = "Weekend Tours",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO5365136389",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "42522 Northrupp",
                    BillAddress2 = "",
                    BillCity = "Fort Wayne",
                    BillPostalCode = "46807",
                    BillStateProvince = "Indiana",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "42522 Northrupp",
                    ShipAddress2 = "",
                    ShipCity = "Fort Wayne",
                    ShipPostalCode = "46807",
                    ShipStateProvince = "Indiana",
                    ShipCountryRegion = "United States",
                    CustPhone = "754-555-0134",
                    CustFirstName = "John",
                    CustLastName = "Donovan"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43687",
                    Store = "Curbside Universe",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO4959110829",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "25264 E. 260th",
                    BillAddress2 = "",
                    BillCity = "Euclid",
                    BillPostalCode = "44119",
                    BillStateProvince = "Ohio",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "25264 E. 260th",
                    ShipAddress2 = "",
                    ShipCity = "Euclid",
                    ShipPostalCode = "44119",
                    ShipStateProvince = "Ohio",
                    ShipCountryRegion = "United States",
                    CustPhone = "131-555-0171",
                    CustFirstName = "Deanna",
                    CustLastName = "Buskirk"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43686",
                    Store = "Fifth Bike Store",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "David",
                    SalesLastName = "Campbell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO5075125561",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "2502 Evergreen Ste E",
                    BillAddress2 = "",
                    BillCity = "Everett",
                    BillPostalCode = "98201",
                    BillStateProvince = "Washington",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "2502 Evergreen Ste E",
                    ShipAddress2 = "",
                    ShipCity = "Everett",
                    ShipPostalCode = "98201",
                    ShipStateProvince = "Washington",
                    ShipCountryRegion = "United States",
                    CustPhone = "652-555-0132",
                    CustFirstName = "Karren",
                    CustLastName = "Burkhardt"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43684",
                    Store = "Daring Rides",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO3393188842",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Bay Area Outlet Mall",
                    BillAddress2 = "",
                    BillCity = "Clearwater",
                    BillPostalCode = "33755",
                    BillStateProvince = "Florida",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Bay Area Outlet Mall",
                    ShipAddress2 = "",
                    ShipCity = "Clearwater",
                    ShipPostalCode = "33755",
                    ShipStateProvince = "Florida",
                    ShipCountryRegion = "United States",
                    CustPhone = "871-555-0159",
                    CustFirstName = "Russell",
                    CustLastName = "King"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43683",
                    Store = "Great Bikes ",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "David",
                    SalesLastName = "Campbell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO2552113807",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Eastridge Mall",
                    BillAddress2 = "",
                    BillCity = "Casper",
                    BillPostalCode = "82601",
                    BillStateProvince = "Wyoming",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Eastridge Mall",
                    ShipAddress2 = "",
                    ShipCity = "Casper",
                    ShipPostalCode = "82601",
                    ShipStateProvince = "Wyoming",
                    ShipCountryRegion = "United States",
                    CustPhone = "571-555-0128",
                    CustFirstName = "François",
                    CustLastName = "Ferrier"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43682",
                    Store = "Convenient Bike Shop",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO1566124200",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Tree Plaza",
                    BillAddress2 = "",
                    BillCity = "Braintree",
                    BillPostalCode = "02184",
                    BillStateProvince = "Massachusetts",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Tree Plaza",
                    ShipAddress2 = "",
                    ShipCity = "Braintree",
                    ShipPostalCode = "02184",
                    ShipStateProvince = "Massachusetts",
                    ShipCountryRegion = "United States",
                    CustPhone = "721-555-0163",
                    CustFirstName = "Judith",
                    CustLastName = "Frazier"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43681",
                    Store = "Bike Rims Company",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO1189177803",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Edgewater Mall",
                    BillAddress2 = "",
                    BillCity = "Biloxi",
                    BillPostalCode = "39530",
                    BillStateProvince = "Mississippi",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Edgewater Mall",
                    ShipAddress2 = "",
                    ShipCity = "Biloxi",
                    ShipPostalCode = "39530",
                    ShipStateProvince = "Mississippi",
                    ShipCountryRegion = "United States",
                    CustPhone = "334-555-0146",
                    CustFirstName = "Charles",
                    CustLastName = "Christensen"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43680",
                    Store = "Area Bike Accessories",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Shu",
                    SalesLastName = "Ito",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO10730130087",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "6900 Sisk Road",
                    BillAddress2 = "",
                    BillCity = "Modesto",
                    BillPostalCode = "95354",
                    BillStateProvince = "California",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "6900 Sisk Road",
                    ShipAddress2 = "",
                    ShipCity = "Modesto",
                    ShipPostalCode = "95354",
                    ShipStateProvince = "California",
                    ShipCountryRegion = "United States",
                    CustPhone = "991-555-0183",
                    CustFirstName = "Frances",
                    CustLastName = "Adams"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43679",
                    Store = "General Bike Corporation",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Garrett",
                    SalesLastName = "Vargas",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO10527142759",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "69251 Creditview Road",
                    BillAddress2 = "",
                    BillCity = "Mississauga",
                    BillPostalCode = "L5B 3V4",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "69251 Creditview Road",
                    ShipAddress2 = "",
                    ShipCity = "Mississauga",
                    ShipPostalCode = "L5B 3V4",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "994-555-0194",
                    CustFirstName = "Susan",
                    CustLastName = "French"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43678",
                    Store = "Separate Parts Corporation",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Shu",
                    SalesLastName = "Ito",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO10817150168",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "440 West Huntington Dr.",
                    BillAddress2 = "",
                    BillCity = "Monrovia",
                    BillPostalCode = "91016",
                    BillStateProvince = "California",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "440 West Huntington Dr.",
                    ShipAddress2 = "",
                    ShipCity = "Monrovia",
                    ShipPostalCode = "91016",
                    ShipStateProvince = "California",
                    ShipCountryRegion = "United States",
                    CustPhone = "207-555-0129",
                    CustFirstName = "Jean",
                    CustLastName = "Jordan"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43677",
                    Store = "Superb Sales and Repair",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Garrett",
                    SalesLastName = "Vargas",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO11049174786",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "990th Floor 700 De La GauchetiSre Ou",
                    BillAddress2 = "",
                    BillCity = "Montreal",
                    BillPostalCode = "H1Y 2H3",
                    BillStateProvince = "Quebec",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "990th Floor 700 De La GauchetiSre Ou",
                    ShipAddress2 = "",
                    ShipCity = "Montreal",
                    ShipPostalCode = "H1Y 2H3",
                    ShipStateProvince = "Quebec",
                    ShipCountryRegion = "Canada",
                    CustPhone = "393-555-0167",
                    CustFirstName = "Brenda",
                    CustLastName = "Heaney"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43676",
                    Store = "Trusted Catalog Store",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO11861165059",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "9920 Picketts Line Road",
                    BillAddress2 = "",
                    BillCity = "Newport News",
                    BillPostalCode = "23607",
                    BillStateProvince = "Virginia",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "9920 Picketts Line Road",
                    ShipAddress2 = "",
                    ShipCity = "Newport News",
                    ShipPostalCode = "23607",
                    ShipStateProvince = "Virginia",
                    ShipCountryRegion = "United States",
                    CustPhone = "497-555-0147",
                    CustFirstName = "Mark",
                    CustLastName = "Hanson"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43675",
                    Store = "First Bike Store",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Jillian",
                    SalesLastName = "Carson",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO12412186464",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Kansas City Factory Outlet",
                    BillAddress2 = "",
                    BillCity = "Odessa",
                    BillPostalCode = "64076",
                    BillStateProvince = "Missouri",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Kansas City Factory Outlet",
                    ShipAddress2 = "",
                    ShipCity = "Odessa",
                    ShipPostalCode = "64076",
                    ShipStateProvince = "Missouri",
                    ShipCountryRegion = "United States",
                    CustPhone = "859-555-0140",
                    CustFirstName = "Valerie",
                    CustLastName = "Hendricks"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43674",
                    Store = "Requisite Part Supply",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO12760141756",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "600 Slater Street",
                    BillAddress2 = "",
                    BillCity = "Ottawa",
                    BillPostalCode = "K4B 1S2",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "600 Slater Street",
                    ShipAddress2 = "",
                    ShipCity = "Ottawa",
                    ShipPostalCode = "K4B 1S2",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "644-555-0114",
                    CustFirstName = "Eric",
                    CustLastName = "Brumfield"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43673",
                    Store = "Seventh Bike Store",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO13775141242",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Stateline Plaza",
                    BillAddress2 = "",
                    BillCity = "Plaistow",
                    BillPostalCode = "03865",
                    BillStateProvince = "New Hampshire",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Stateline Plaza",
                    ShipAddress2 = "",
                    ShipCity = "Plaistow",
                    ShipPostalCode = "03865",
                    ShipStateProvince = "New Hampshire",
                    ShipCountryRegion = "United States",
                    CustPhone = "860-555-0119",
                    CustFirstName = "Nancy",
                    CustLastName = "Hirota"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43671",
                    Store = "Basic Bike Company",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "David",
                    SalesLastName = "Campbell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO13978119376",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "15 East Main",
                    BillAddress2 = "",
                    BillCity = "Port Orchard",
                    BillPostalCode = "98366",
                    BillStateProvince = "Washington",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "15 East Main",
                    ShipAddress2 = "",
                    ShipCity = "Port Orchard",
                    ShipPostalCode = "98366",
                    ShipStateProvince = "Washington",
                    ShipCountryRegion = "United States",
                    CustPhone = "170-555-0189",
                    CustFirstName = "Peggy",
                    CustLastName = "Justice"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43670",
                    Store = "Historic Bicycle Sales",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO14384116310",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Redford Plaza",
                    BillAddress2 = "",
                    BillCity = "Redford",
                    BillPostalCode = "48239",
                    BillStateProvince = "Michigan",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Redford Plaza",
                    ShipAddress2 = "",
                    ShipCity = "Redford",
                    ShipPostalCode = "48239",
                    ShipStateProvince = "Michigan",
                    ShipCountryRegion = "United States",
                    CustPhone = "264-555-0143",
                    CustFirstName = "Mae",
                    CustLastName = "Black"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43669",
                    Store = "The Bike Shop",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "David",
                    SalesLastName = "Campbell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO14123169936",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "3250 South Meridian",
                    BillAddress2 = "",
                    BillCity = "Puyallup",
                    BillPostalCode = "98371",
                    BillStateProvince = "Washington",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "3250 South Meridian",
                    ShipAddress2 = "",
                    ShipCity = "Puyallup",
                    ShipPostalCode = "98371",
                    ShipStateProvince = "Washington",
                    ShipCountryRegion = "United States",
                    CustPhone = "957-555-0125",
                    CustFirstName = "Carolyn",
                    CustLastName = "Farino"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43668",
                    Store = "Retail Mall",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO14732180295",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "254480 River Rd",
                    BillAddress2 = "",
                    BillCity = "Richmond",
                    BillPostalCode = "V6B 3P7",
                    BillStateProvince = "British Columbia",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "254480 River Rd",
                    ShipAddress2 = "",
                    ShipCity = "Richmond",
                    ShipPostalCode = "V6B 3P7",
                    ShipStateProvince = "British Columbia",
                    ShipCountryRegion = "Canada",
                    CustPhone = "726-555-0155",
                    CustFirstName = "Ryan",
                    CustLastName = "Calafato"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43667",
                    Store = "Yellow Bicycle Company",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Jillian",
                    SalesLastName = "Carson",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO15428132599",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "St. Louis Marketplace",
                    BillAddress2 = "",
                    BillCity = "Saint Louis",
                    BillPostalCode = "63103",
                    BillStateProvince = "Missouri",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "St. Louis Marketplace",
                    ShipAddress2 = "",
                    ShipCity = "Saint Louis",
                    ShipPostalCode = "63103",
                    ShipStateProvince = "Missouri",
                    ShipCountryRegion = "United States",
                    CustPhone = "470-555-0171",
                    CustFirstName = "Scott",
                    CustLastName = "MacDonald"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43665",
                    Store = "Latest Sports Equipment",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "David",
                    SalesLastName = "Campbell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO16588191572",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "4251 First Avenue",
                    BillAddress2 = "",
                    BillCity = "Seattle",
                    BillPostalCode = "98104",
                    BillStateProvince = "Washington",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "4251 First Avenue",
                    ShipAddress2 = "",
                    ShipCity = "Seattle",
                    ShipPostalCode = "98104",
                    ShipStateProvince = "Washington",
                    ShipCountryRegion = "United States",
                    CustPhone = "340-555-0131",
                    CustFirstName = "Richard",
                    CustLastName = "Bready"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43664",
                    Store = "Capable Sales and Service",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Pamela",
                    SalesLastName = "Ansman-Wolfe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO16617121983",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "765 Delridge Way Sw",
                    BillAddress2 = "",
                    BillCity = "Seattle",
                    BillPostalCode = "98104",
                    BillStateProvince = "Washington",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "765 Delridge Way Sw",
                    ShipAddress2 = "",
                    ShipCity = "Seattle",
                    ShipPostalCode = "98104",
                    ShipStateProvince = "Washington",
                    ShipCountryRegion = "United States",
                    CustPhone = "928-555-0117",
                    CustFirstName = "Sandeep",
                    CustLastName = "Katyal"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43663",
                    Store = "World Bike Discount Store",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Linda",
                    SalesLastName = "Mitchell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO18009186470",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "3065 Santa Margarita Parkway",
                    BillAddress2 = "",
                    BillCity = "Trabuco Canyon",
                    BillPostalCode = "92679",
                    BillStateProvince = "California",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "3065 Santa Margarita Parkway",
                    ShipAddress2 = "",
                    ShipCity = "Trabuco Canyon",
                    ShipPostalCode = "92679",
                    ShipStateProvince = "California",
                    ShipCountryRegion = "United States",
                    CustPhone = "992-555-0111",
                    CustFirstName = "Jimmy",
                    CustLastName = "Bischoff"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43662",
                    Store = "Health Spa, Limited",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO18444174044",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "2500 University Avenue",
                    BillAddress2 = "",
                    BillCity = "Toronto",
                    BillPostalCode = "M4B 1V5",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "2500 University Avenue",
                    ShipAddress2 = "",
                    ShipCity = "Toronto",
                    ShipPostalCode = "M4B 1V5",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "431-555-0153",
                    CustFirstName = "Robin",
                    CustLastName = "McGuigan"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43661",
                    Store = "Original Bicycle Supply Company",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO18473189620",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "2573 Dufferin Street",
                    BillAddress2 = "",
                    BillCity = "Toronto",
                    BillPostalCode = "M4B 1V5",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "2573 Dufferin Street",
                    ShipAddress2 = "",
                    ShipCity = "Toronto",
                    ShipPostalCode = "M4B 1V5",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "185-555-0190",
                    CustFirstName = "Jauna",
                    CustLastName = "Elson"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43660",
                    Store = "Pedals Warehouse",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO18850127500",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "6055 Shawnee Industrial Way",
                    BillAddress2 = "",
                    BillCity = "Suwanee",
                    BillPostalCode = "30024",
                    BillStateProvince = "Georgia",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "6055 Shawnee Industrial Way",
                    ShipAddress2 = "",
                    ShipCity = "Suwanee",
                    ShipPostalCode = "30024",
                    ShipStateProvince = "Georgia",
                    ShipCountryRegion = "United States",
                    CustPhone = "987-555-0126",
                    CustFirstName = "Takiko",
                    CustLastName = "Collins"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43659",
                    Store = "Better Bike Shop",
                    OrderDate = new DateTime(2001, 7, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO522145787",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "42525 Austell Road",
                    BillAddress2 = "",
                    BillCity = "Austell",
                    BillPostalCode = "30106",
                    BillStateProvince = "Georgia",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "42525 Austell Road",
                    ShipAddress2 = "",
                    ShipCity = "Austell",
                    ShipPostalCode = "30106",
                    ShipStateProvince = "Georgia",
                    ShipCountryRegion = "United States",
                    CustPhone = "967-555-0129",
                    CustFirstName = "James",
                    CustLastName = "Hendergart"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43875",
                    Store = "Tread Industries",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO12586178184",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "25631 Florida Mall Ave.",
                    BillAddress2 = "",
                    BillCity = "Orlando",
                    BillPostalCode = "32804",
                    BillStateProvince = "Florida",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "9707 Coldwater Drive",
                    ShipAddress2 = "",
                    ShipCity = "Orlando",
                    ShipPostalCode = "32804",
                    ShipStateProvince = "Florida",
                    ShipCountryRegion = "United States",
                    CustPhone = "965-555-0112",
                    CustFirstName = "Joseph",
                    CustLastName = "Cantoni"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43876",
                    Store = "Active Transport Inc.",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO12006119347",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "225200 Miles Ave.",
                    BillAddress2 = "",
                    BillCity = "North Randall",
                    BillPostalCode = "44128",
                    BillStateProvince = "Ohio",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "225200 Miles Ave.",
                    ShipAddress2 = "",
                    ShipCity = "North Randall",
                    ShipPostalCode = "44128",
                    ShipStateProvince = "Ohio",
                    ShipCountryRegion = "United States",
                    CustPhone = "526-555-0155",
                    CustFirstName = "Lynn",
                    CustLastName = "Tsoflias"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43877",
                    Store = "Outdoor Sports Supply",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Pamela",
                    SalesLastName = "Ansman-Wolfe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO11919119101",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Great Northwestern",
                    BillAddress2 = "",
                    BillCity = "North Bend",
                    BillPostalCode = "98045",
                    BillStateProvince = "Washington",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Great Northwestern",
                    ShipAddress2 = "",
                    ShipCity = "North Bend",
                    ShipPostalCode = "98045",
                    ShipStateProvince = "Washington",
                    ShipCountryRegion = "United States",
                    CustPhone = "107-555-0132",
                    CustFirstName = "Margaret",
                    CustLastName = "Krupka"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43878",
                    Store = "Only Bikes and Accessories",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO11716136854",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "123 Union Square South",
                    BillAddress2 = "",
                    BillCity = "New York",
                    BillPostalCode = "10007",
                    BillStateProvince = "New York",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "123 Union Square South",
                    ShipAddress2 = "",
                    ShipCity = "New York",
                    ShipPostalCode = "10007",
                    ShipStateProvince = "New York",
                    ShipCountryRegion = "United States",
                    CustPhone = "539-555-0142",
                    CustFirstName = "Gina",
                    CustLastName = "Clark"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43879",
                    Store = "Designated Distributors",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO11600128380",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "254 Colonnade Road",
                    BillAddress2 = "",
                    BillCity = "Nepean",
                    BillPostalCode = "K2J 2W5",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "254 Colonnade Road",
                    ShipAddress2 = "",
                    ShipCity = "Nepean",
                    ShipPostalCode = "K2J 2W5",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "699-555-0155",
                    CustFirstName = "Cecil",
                    CustLastName = "Allison"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43880",
                    Store = "Primary Bike Distributors",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Garrett",
                    SalesLastName = "Vargas",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO11020127453",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "965 De La Gauchetiere West",
                    BillAddress2 = "",
                    BillCity = "Montreal",
                    BillPostalCode = "H1Y 2H8",
                    BillStateProvince = "Quebec",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "965 De La Gauchetiere West",
                    ShipAddress2 = "",
                    ShipCity = "Montreal",
                    ShipPostalCode = "H1Y 2H8",
                    ShipStateProvince = "Quebec",
                    ShipCountryRegion = "Canada",
                    CustPhone = "495-555-0161",
                    CustFirstName = "Brian",
                    CustLastName = "Goldstein"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43881",
                    Store = "Great Bicycle Supply",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Jillian",
                    SalesLastName = "Carson",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO10759119626",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "455 256th St.",
                    BillAddress2 = "",
                    BillCity = "Moline",
                    BillPostalCode = "61265",
                    BillStateProvince = "Illinois",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "455 256th St.",
                    ShipAddress2 = "",
                    ShipCity = "Moline",
                    ShipPostalCode = "61265",
                    ShipStateProvince = "Illinois",
                    ShipCountryRegion = "United States",
                    CustPhone = "810-555-0160",
                    CustFirstName = "Ranjit",
                    CustLastName = "Varkey Chudukatil"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43882",
                    Store = "Scratch-Resistant Finishes Company",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO10469165208",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "5700 Explorer Drive",
                    BillAddress2 = "",
                    BillCity = "Mississauga",
                    BillPostalCode = "L4W 5J3",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "5700 Explorer Drive",
                    ShipAddress2 = "",
                    ShipCity = "Mississauga",
                    ShipPostalCode = "L4W 5J3",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "156-555-0111",
                    CustFirstName = "John",
                    CustLastName = "Berger"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43883",
                    Store = "Lease-a-Bike Shop",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO10121175623",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Connecticut Post Mall",
                    BillAddress2 = "",
                    BillCity = "Milford",
                    BillPostalCode = "06460",
                    BillStateProvince = "Connecticut",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Connecticut Post Mall",
                    ShipAddress2 = "",
                    ShipCity = "Milford",
                    ShipPostalCode = "06460",
                    ShipStateProvince = "Connecticut",
                    ShipCountryRegion = "United States",
                    CustPhone = "158-555-0188",
                    CustFirstName = "Bernard",
                    CustLastName = "Duerr"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43884",
                    Store = "Hardware Components",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Jillian",
                    SalesLastName = "Carson",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO10440182311",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "City Center",
                    BillAddress2 = "",
                    BillCity = "Minneapolis",
                    BillPostalCode = "55402",
                    BillStateProvince = "Minnesota",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "99 Front Street",
                    ShipAddress2 = "",
                    ShipCity = "Minneapolis",
                    ShipPostalCode = "55402",
                    ShipStateProvince = "Minnesota",
                    ShipCountryRegion = "United States",
                    CustPhone = "153-555-0195",
                    CustFirstName = "Phyllis",
                    CustLastName = "Huntsman"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43885",
                    Store = "Basic Sports Equipment",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Linda",
                    SalesLastName = "Mitchell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO609186449",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "3250 Baldwin Park Blvd",
                    BillAddress2 = "",
                    BillCity = "Baldwin Park",
                    BillPostalCode = "91706",
                    BillStateProvince = "California",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "3250 Baldwin Park Blvd",
                    ShipAddress2 = "",
                    ShipCity = "Baldwin Park",
                    ShipPostalCode = "91706",
                    ShipStateProvince = "California",
                    ShipCountryRegion = "United States",
                    CustPhone = "768-555-0125",
                    CustFirstName = "Garth",
                    CustLastName = "Fort"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43886",
                    Store = "Finer Riding Supplies",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO1827149671",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "#9900 2700 Production Way",
                    BillAddress2 = "",
                    BillCity = "Burnaby",
                    BillPostalCode = "V5A 4X1",
                    BillStateProvince = "British Columbia",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "#9900 2700 Production Way",
                    ShipAddress2 = "",
                    ShipCity = "Burnaby",
                    ShipPostalCode = "V5A 4X1",
                    ShipStateProvince = "British Columbia",
                    ShipCountryRegion = "Canada",
                    CustPhone = "767-555-0151",
                    CustFirstName = "Jacob",
                    CustLastName = "Dean"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43887",
                    Store = "New Bikes Company",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO1276169981",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Hilton Head Factory Outlets No. 25",
                    BillAddress2 = "",
                    BillCity = "Bluffton",
                    BillPostalCode = "29910",
                    BillStateProvince = "South Carolina",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Hilton Head Factory Outlets No. 25",
                    ShipAddress2 = "",
                    ShipCity = "Bluffton",
                    ShipPostalCode = "29910",
                    ShipStateProvince = "South Carolina",
                    ShipCountryRegion = "United States",
                    CustPhone = "453-555-0165",
                    CustFirstName = "Ronald",
                    CustLastName = "Adina"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43888",
                    Store = "Wholesale Parts",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO2088113013",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "655-4th Ave S.W.",
                    BillAddress2 = "",
                    BillCity = "Calgary",
                    BillPostalCode = "T2P 2G8",
                    BillStateProvince = "Alberta",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "655-4th Ave S.W.",
                    ShipAddress2 = "",
                    ShipCity = "Calgary",
                    ShipPostalCode = "T2P 2G8",
                    ShipStateProvince = "Alberta",
                    ShipCountryRegion = "Canada",
                    CustPhone = "674-555-0187",
                    CustFirstName = "Derek",
                    CustLastName = "Graham"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43889",
                    Store = "General Department Stores",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Garrett",
                    SalesLastName = "Vargas",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO2030112412",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "253131 Lake Frasier Drive, Office No. 2",
                    BillAddress2 = "",
                    BillCity = "Calgary",
                    BillPostalCode = "T2P 2G8",
                    BillStateProvince = "Alberta",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "253131 Lake Frasier Drive, Office No. 2",
                    ShipAddress2 = "",
                    ShipCity = "Calgary",
                    ShipPostalCode = "T2P 2G8",
                    ShipStateProvince = "Alberta",
                    ShipCountryRegion = "Canada",
                    CustPhone = "143-555-0129",
                    CustFirstName = "Kari",
                    CustLastName = "Hensien"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43890",
                    Store = "Serious Cycles",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Garrett",
                    SalesLastName = "Vargas",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO2146115360",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Suite 99320 255 - 510th Avenue S.W.",
                    BillAddress2 = "",
                    BillCity = "Calgary",
                    BillPostalCode = "T2P 2G8",
                    BillStateProvince = "Alberta",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "Suite 99320 255 - 510th Avenue S.W.",
                    ShipAddress2 = "",
                    ShipCity = "Calgary",
                    ShipPostalCode = "T2P 2G8",
                    ShipStateProvince = "Alberta",
                    ShipCountryRegion = "Canada",
                    CustPhone = "614-555-0134",
                    CustFirstName = "Maxwell",
                    CustLastName = "Amland"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43891",
                    Store = "Cross-Country Riding Supplies",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO2726163521",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Station E",
                    BillAddress2 = "",
                    BillCity = "Chalk Riber",
                    BillPostalCode = "K0J 1J0",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "Station E",
                    ShipAddress2 = "",
                    ShipCity = "Chalk Riber",
                    ShipPostalCode = "K0J 1J0",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "344-555-0144",
                    CustFirstName = "Bryan",
                    CustLastName = "Hamilton"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43892",
                    Store = "Farthermost Bike Shop",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Shu",
                    SalesLastName = "Ito",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO2523117473",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "99000 S. Avalon Blvd. Suite 750",
                    BillAddress2 = "",
                    BillCity = "Carson",
                    BillPostalCode = "90746",
                    BillStateProvince = "California",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "99000 S. Avalon Blvd. Suite 750",
                    ShipAddress2 = "",
                    ShipCity = "Carson",
                    ShipPostalCode = "90746",
                    ShipStateProvince = "California",
                    ShipCountryRegion = "United States",
                    CustPhone = "156-555-0187",
                    CustFirstName = "Blaine",
                    CustLastName = "Dockter"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43893",
                    Store = "Acceptable Sales & Service",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "José",
                    SalesLastName = "Saraiva",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO2204129382",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "6400, 888 - 3rd Avenue",
                    BillAddress2 = "",
                    BillCity = "Calgary",
                    BillPostalCode = "T2P 2G8",
                    BillStateProvince = "Alberta",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "6400, 888 - 3rd Avenue",
                    ShipAddress2 = "",
                    ShipCity = "Calgary",
                    ShipPostalCode = "T2P 2G8",
                    ShipStateProvince = "Alberta",
                    ShipCountryRegion = "Canada",
                    CustPhone = "656-555-0173",
                    CustFirstName = "Elizabeth",
                    CustLastName = "Keyser"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43894",
                    Store = "Some Discount Store",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Pamela",
                    SalesLastName = "Ansman-Wolfe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO2958194987",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Frontier Mall",
                    BillAddress2 = "",
                    BillCity = "Cheyenne",
                    BillPostalCode = "82001",
                    BillStateProvince = "Wyoming",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Frontier Mall",
                    ShipAddress2 = "",
                    ShipCity = "Cheyenne",
                    ShipPostalCode = "82001",
                    ShipStateProvince = "Wyoming",
                    ShipCountryRegion = "United States",
                    CustPhone = "158-555-0123",
                    CustFirstName = "Nkenge",
                    CustLastName = "McLin"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43895",
                    Store = "Vast Bike Sales and Rental",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Pamela",
                    SalesLastName = "Ansman-Wolfe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO2900121738",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Lewis County Mall",
                    BillAddress2 = "",
                    BillCity = "Chehalis",
                    BillPostalCode = "98532",
                    BillStateProvince = "Washington",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Lewis County Mall",
                    ShipAddress2 = "",
                    ShipCity = "Chehalis",
                    ShipPostalCode = "98532",
                    ShipStateProvince = "Washington",
                    ShipCountryRegion = "United States",
                    CustPhone = "554-555-0124",
                    CustFirstName = "Twanna",
                    CustLastName = "Evans"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43896",
                    Store = "Rental Bikes",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Jillian",
                    SalesLastName = "Carson",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO3857154341",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "99828 Routh Street, Suite 825",
                    BillAddress2 = "",
                    BillCity = "Dallas",
                    BillPostalCode = "75201",
                    BillStateProvince = "Texas",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "99828 Routh Street, Suite 825",
                    ShipAddress2 = "",
                    ShipCity = "Dallas",
                    ShipPostalCode = "75201",
                    ShipStateProvince = "Texas",
                    ShipCountryRegion = "United States",
                    CustPhone = "367-555-0124",
                    CustFirstName = "Richard",
                    CustLastName = "Irwin"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43897",
                    Store = "Resale Services",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Linda",
                    SalesLastName = "Mitchell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO3799116239",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Fox Hills",
                    BillAddress2 = "",
                    BillCity = "Culver City",
                    BillPostalCode = "90232",
                    BillStateProvince = "California",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Fox Hills",
                    ShipAddress2 = "",
                    ShipCity = "Culver City",
                    ShipPostalCode = "90232",
                    ShipStateProvince = "California",
                    ShipCountryRegion = "United States",
                    CustPhone = "226-555-0146",
                    CustFirstName = "Thomas",
                    CustLastName = "Armstrong"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43898",
                    Store = "Rewarding Activities Company",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Garrett",
                    SalesLastName = "Vargas",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO4901196283",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "25575 The Queensway",
                    BillAddress2 = "",
                    BillCity = "Etobicoke",
                    BillPostalCode = "M9W 3P3",
                    BillStateProvince = "Ontario",
                    BillCountryRegion = "Canada",
                    ShipAddress1 = "25575 The Queensway",
                    ShipAddress2 = "",
                    ShipCity = "Etobicoke",
                    ShipPostalCode = "M9W 3P3",
                    ShipStateProvince = "Ontario",
                    ShipCountryRegion = "Canada",
                    CustPhone = "752-555-0185",
                    CustFirstName = "Della",
                    CustLastName = "Demott Jr"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43899",
                    Store = "District Mall",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Jillian",
                    SalesLastName = "Carson",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO5191115657",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "25095 W. Florissant",
                    BillAddress2 = "",
                    BillCity = "Ferguson",
                    BillPostalCode = "63135",
                    BillStateProvince = "Missouri",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "25095 W. Florissant",
                    ShipAddress2 = "",
                    ShipCity = "Ferguson",
                    ShipPostalCode = "63135",
                    ShipStateProvince = "Missouri",
                    ShipCountryRegion = "United States",
                    CustPhone = "249-555-0179",
                    CustFirstName = "Imtiaz",
                    CustLastName = "Khan"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43900",
                    Store = "Consolidated Sales",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO5568199700",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Carolina Factory Shops",
                    BillAddress2 = "",
                    BillCity = "Gaffney",
                    BillPostalCode = "29340",
                    BillStateProvince = "South Carolina",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Carolina Factory Shops",
                    ShipAddress2 = "",
                    ShipCity = "Gaffney",
                    ShipPostalCode = "29340",
                    ShipStateProvince = "South Carolina",
                    ShipCountryRegion = "United States",
                    CustPhone = "762-555-0110",
                    CustFirstName = "Samuel",
                    CustLastName = "Johnson"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43901",
                    Store = "Sturdy Toys",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Linda",
                    SalesLastName = "Mitchell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO5684189260",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Pacific West Outlet",
                    BillAddress2 = "",
                    BillCity = "Gilroy",
                    BillPostalCode = "95020",
                    BillStateProvince = "California",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Pacific West Outlet",
                    ShipAddress2 = "",
                    ShipCity = "Gilroy",
                    ShipPostalCode = "95020",
                    ShipStateProvince = "California",
                    ShipCountryRegion = "United States",
                    CustPhone = "330-555-0116",
                    CustFirstName = "John",
                    CustLastName = "Kelly"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43902",
                    Store = "eCommerce Bikes",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO5858178400",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Gulfport Factory Shops",
                    BillAddress2 = "",
                    BillCity = "Gulfport",
                    BillPostalCode = "39501",
                    BillStateProvince = "Mississippi",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Gulfport Factory Shops",
                    ShipAddress2 = "",
                    ShipCity = "Gulfport",
                    ShipPostalCode = "39501",
                    ShipStateProvince = "Mississippi",
                    ShipCountryRegion = "United States",
                    CustPhone = "695-555-0111",
                    CustFirstName = "Phyllis",
                    CustLastName = "Allen"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43903",
                    Store = "Metro Bike Mart",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Tsvi",
                    SalesLastName = "Reiter",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO5800178059",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Po Box 2257",
                    BillAddress2 = "",
                    BillCity = "Greensboro",
                    BillPostalCode = "27412",
                    BillStateProvince = "North Carolina",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Po Box 2257",
                    ShipAddress2 = "",
                    ShipCity = "Greensboro",
                    ShipPostalCode = "27412",
                    ShipStateProvince = "North Carolina",
                    ShipCountryRegion = "United States",
                    CustPhone = "565-555-0181",
                    CustFirstName = "Helen",
                    CustLastName = "Lutes"
                };
                datas.Add(data);
                data = new Customers()
                {
                    SalesOrderNumber = "SO43904",
                    Store = "Swift Cycles",
                    OrderDate = new DateTime(2001, 8, 1, 0, 0, 0),
                    SalesFirstName = "Jillian",
                    SalesLastName = "Carson",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO6351158788",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "25500 Old Spanish Trail",
                    BillAddress2 = "",
                    BillCity = "Houston",
                    BillPostalCode = "77003",
                    BillStateProvince = "Texas",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "25500 Old Spanish Trail",
                    ShipAddress2 = "",
                    ShipCity = "Houston",
                    ShipPostalCode = "77003",
                    ShipStateProvince = "Texas",
                    ShipCountryRegion = "United States",
                    CustPhone = "184-555-0187",
                    CustFirstName = "Sunil",
                    CustLastName = "Uppal"
                };
                datas.Add(data);
                return datas;
            }
        }


    }

    #endregion


    #region Report Parameters
    public class ConditionalParameter : ReportViewerSampleHelper
    {

        public ConditionalParameter(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ReportParameters";
            this.ReportName = "Conditional Parameter";
        }

        public override void UpdateDataSet()
        {
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "OrderSummary", Value = OrderSummary.GetOrderSummary() });
        }

        public class OrderSummary
        {
            public int OrderId { get; set; }
            public DateTime OrderDate { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public double UnitPrice { get; set; }
            public double Discount { get; set; }
            public double ExtPrice { get; set; }
            public static IList GetOrderSummary()
            {
                string[] prodcutName = new string[] { "Mountain-100 Black, 42", "Mountain-100 Black, 44", "Mountain-100 Black, 48", "Mountain-100 Silver, 38", "Long-Sleeve Logo Jersey, M", "Mountain Bike Socks, M", "Long-Sleeve Logo Jersey, L", "Road-450 Red, 52" };
                double[] prodcutPrice = new double[] { 10, 10.5, 15.25, 25, 10.25, 13, 20, 9.25 };
                List<OrderSummary> OrderSummaryCollection = new List<OrderSummary>();
                OrderSummary orderDetail = null;
                Random ran = new Random();
                int orderNumber = 43659;
                int prodcutCount = prodcutName.Count();
                for (int i = 0; i < 100; i++)
                {
                    int prodcutIndex = ran.Next(prodcutCount);
                    orderDetail = new OrderSummary()
                    {
                        OrderId = orderNumber++,
                        OrderDate = new DateTime(ran.Next(2004, 2010), ran.Next(1, 12), ran.Next(1, 27)),
                        ProductName = prodcutName[prodcutIndex],
                        Quantity = ran.Next(1, 8),
                        UnitPrice = prodcutPrice[prodcutIndex],
                        Discount = 0.0000
                    };
                    orderDetail.ExtPrice = orderDetail.Quantity * orderDetail.UnitPrice;
                    OrderSummaryCollection.Add(orderDetail);
                }
                return OrderSummaryCollection;
            }
        }
    }

    public class QueryParameter : ReportViewerSampleHelper
    {
        public QueryParameter(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ReportParameters";
            this.ReportName = "Query Parameter";
        }

        public override void SetParameter()
        {
            this.ReportViewer.SetParameters(this.GetParameter());
        }

        public override void UpdateDataSet()
        {
            setDataSource();
        }

        public void setDataSource()
        {
            ReportParameterInfoCollection paramCollection = this.ReportViewer.GetParameters();
            string value = paramCollection.Where(p => p.Name.Equals("InvoiceID")).FirstOrDefault().Values.FirstOrDefault();
            if (!string.IsNullOrEmpty(value))
            {
                this.ReportViewer.DataSources.Clear();
                this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "ShipDetails", Value = ShipDetails.GetData(value) });
                this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "OrderDetails", Value = OrderDetails.GetData(value) });
                this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "InvoiceDetails", Value = InvoiceDetails.GetData(value) });
            }
        }
        public IList<ReportParameter> GetParameter()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            ReportParameter param = new ReportParameter();
            param.Labels.Add("10248");
            param.Values.Add("10248");
            param.Name = "InvoiceID";
            parameters.Add(param);

            return parameters;
        }

        public class ShipDetails
        {
            public string ShipName { get; set; }
            public string ShipAddress { get; set; }
            public double Freight { get; set; }
            public DateTime ShippedDate { get; set; }
            public string ShipCity { get; set; }
            public string ShipCountry { get; set; }
            public string OrderID { get; set; }
            public static IList GetData(string orderId)
            {
                List<ShipDetails> ShipDetailsCollection = new List<ShipDetails>();
                ShipDetails shipDetail = null;
                shipDetail = new ShipDetails()
                {
                    ShipName = "Vins Chevalier",
                    ShipAddress = "59 rue de l'Abbaye",
                    Freight = 32.38,
                    ShippedDate = new DateTime(2003, 12, 23),
                    ShipCity = "Reims",
                    ShipCountry = "France",
                    OrderID = "10248"
                };
                ShipDetailsCollection.Add(shipDetail);

                shipDetail = new ShipDetails()
                {
                    ShipName = "Vins",
                    ShipAddress = "59 rue",
                    Freight = 32.38,
                    ShippedDate = new DateTime(2003, 12, 23),
                    ShipCity = "Reims",
                    ShipCountry = "France",
                    OrderID = "10249"
                };
                ShipDetailsCollection.Add(shipDetail);

                return ShipDetailsCollection.Where(sd => sd.OrderID.Equals(orderId)).ToList();
            }
        }

        public class InvoiceDetails
        {
            public string OrderID { get; set; }

            public static IList GetData(string orderId)
            {
                List<InvoiceDetails> invoiceDetailsCollection = new List<InvoiceDetails>();
                InvoiceDetails invoiceDetail = null;
                invoiceDetail = new InvoiceDetails()
                {
                    OrderID = "10248",

                };
                invoiceDetailsCollection.Add(invoiceDetail);
                invoiceDetail = new InvoiceDetails()
                {
                    OrderID = "10249",
                };
                invoiceDetailsCollection.Add(invoiceDetail);
                return invoiceDetailsCollection;
            }
        }

        public class OrderDetails
        {
            public string ProductID { get; set; }
            public string ProductName { get; set; }
            public string Quantity { get; set; }
            public double UnitPrice { get; set; }
            public double Discount { get; set; }
            public string OrderID { get; set; }
            public double Price
            {
                get
                {
                    return (UnitPrice * double.Parse(Quantity));
                }
            }
            public static IList GetData(string orderId)
            {
                List<OrderDetails> OrderDetailsCollection = new List<OrderDetails>();
                OrderDetails orderDetail = null;
                orderDetail = new OrderDetails()
                {
                    ProductID = "11",
                    Quantity = "12",
                    UnitPrice = 14,
                    Discount = 0,
                    OrderID = "10248",
                    ProductName = "Queso Cabrales"

                };
                OrderDetailsCollection.Add(orderDetail);
                orderDetail = new OrderDetails()
                {
                    ProductID = "42",
                    Quantity = "10",
                    UnitPrice = 9.8,
                    Discount = 0,
                    OrderID = "10248",
                    ProductName = "Mozzarella di Giovanni"
                };
                OrderDetailsCollection.Add(orderDetail);
                orderDetail = new OrderDetails()
                {
                    ProductID = "72",
                    Quantity = "5",
                    UnitPrice = 34.8,
                    Discount = 0,
                    OrderID = "10248",
                    ProductName = "Singaporean Hokkien Fried Mee"
                };
                OrderDetailsCollection.Add(orderDetail);

                orderDetail = new OrderDetails()
                {
                    ProductID = "72",
                    Quantity = "5",
                    UnitPrice = 34.8,
                    Discount = 0,
                    OrderID = "10249",
                    ProductName = "Singaporean"
                };
                OrderDetailsCollection.Add(orderDetail);
                return OrderDetailsCollection.Where(od => od.OrderID.Equals(orderId)).ToList();
            }
        }
    }
    #endregion

#else
    public class TableSummaries : ReportViewerSampleHelper
    {
        public TableSummaries(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ReportElement";
            this.ReportName = "Table Summaries";
        }

        public override void UpdateDataSet()
        {
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "Sales", Value = SalesDetails.GetData() });
        }

        public class SalesDetails
        {
            public string ProdCat { get; set; }
            public string SubCat { get; set; }
            public double? OrderYear { get; set; }
            public string OrderQtr { get; set; }
            public double? Sales { get; set; }
            public static IList GetData()
            {
                List<SalesDetails> datas = new List<SalesDetails>();
                SalesDetails data = null;
                data = new SalesDetails()
                {
                    ProdCat = "Accessories",
                    SubCat = "Helmets",
                    OrderYear = 2002,
                    OrderQtr = "Q1",
                    Sales = 4945.6925
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 957715.1942
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Forks",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 23543.1060
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = 2002,
                    OrderQtr = "Q1",
                    Sales = 3171787.6112
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Accessories",
                    SubCat = "Helmets",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 33853.1033
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Wheels",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 163921.8870
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = 2003,
                    OrderQtr = "Q2",
                    Sales = 4119658.6506
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Socks",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 6968.6884
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 3734891.6389
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 608352.8754
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Handlebars",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 18309.4452
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 286591.8208
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Accessories",
                    SubCat = "Tires and Tubes",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 41940.3364
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = 2003,
                    OrderQtr = "Q2",
                    Sales = 440260.9831
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = 2003,
                    OrderQtr = "Q2",
                    Sales = 457688.8401
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Vests",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 66882.6450
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Accessories",
                    SubCat = "Pumps",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 3226.3860
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = 2003,
                    OrderQtr = "Q2",
                    Sales = 51600.6190
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Chains",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 3476.0176
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Handlebars",
                    OrderYear = 2003,
                    OrderQtr = "Q2",
                    Sales = 17194.2146
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 565229.8810
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 243.7175
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = 2002,
                    OrderQtr = "Q2",
                    Sales = 155311.4063
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = 2002,
                    OrderQtr = "Q2",
                    Sales = 220935.1648
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Accessories",
                    SubCat = "Locks",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 15.0000
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Mountain Frames",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 827287.5234
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Accessories",
                    SubCat = "Bike Racks",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 75920.4000
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Bottom Brackets",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 17453.6400
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Bikes",
                    SubCat = "Touring Bikes",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 3298006.2858
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Brakes",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 18571.4700
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 56782.4280
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Pedals",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 54185.2014
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Jerseys",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 173041.0492
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Jerseys",
                    OrderYear = 2002,
                    OrderQtr = "Q2",
                    Sales = 16931.2362
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Headsets",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 19701.9001
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Road Frames",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 458089.4246
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Shorts",
                    OrderYear = 2003,
                    OrderQtr = "Q1",
                    Sales = 11230.1280
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = 2002,
                    OrderQtr = "Q4",
                    Sales = 4189621.8590
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Brakes",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 26659.0800
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Wheels",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 83.2981
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Vests",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 81085.6900
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Cranksets",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 80244.1372
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Socks",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 6183.1422
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Components",
                    SubCat = "Wheels",
                    OrderYear = 2003,
                    OrderQtr = "Q2",
                    Sales = 163929.9435
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 67088.3037
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Tights",
                    OrderYear = 2003,
                    OrderQtr = "Q3",
                    Sales = 779.8960
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Socks",
                    OrderYear = 2002,
                    OrderQtr = "Q1",
                    Sales = 1273.8550
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Bikes",
                    SubCat = "Road Bikes",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 4930692.7825
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Shorts",
                    OrderYear = 2003,
                    OrderQtr = "Q4",
                    Sales = 84192.3708
                };
                datas.Add(data);
                data = new SalesDetails()
                {
                    ProdCat = "Clothing",
                    SubCat = "Jerseys",
                    OrderYear = 2002,
                    OrderQtr = "Q3",
                    Sales = 48901.7598
                };
                datas.Add(data);
                return datas;
            }
        }
    }

    public class ConditionalParameter : ReportViewerSampleHelper
    {

        public ConditionalParameter(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ReportParameters";
            this.ReportName = "Conditional Parameter";
        }

        public override void UpdateDataSet()
        {
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "OrderSummary", Value = OrderSummary.GetOrderSummary() });
        }

        public class OrderSummary
        {
            public int OrderId { get; set; }
            public DateTime OrderDate { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public double UnitPrice { get; set; }
            public double Discount { get; set; }
            public double ExtPrice { get; set; }
            public static IList GetOrderSummary()
            {
                string[] prodcutName = new string[] { "Mountain-100 Black, 42", "Mountain-100 Black, 44", "Mountain-100 Black, 48", "Mountain-100 Silver, 38", "Long-Sleeve Logo Jersey, M", "Mountain Bike Socks, M", "Long-Sleeve Logo Jersey, L", "Road-450 Red, 52" };
                double[] prodcutPrice = new double[] { 10, 10.5, 15.25, 25, 10.25, 13, 20, 9.25 };
                List<OrderSummary> OrderSummaryCollection = new List<OrderSummary>();
                OrderSummary orderDetail = null;
                Random ran = new Random();
                int orderNumber = 43659;
                int prodcutCount = prodcutName.Count();
                for (int i = 0; i < 100; i++)
                {
                    int prodcutIndex = ran.Next(prodcutCount);
                    orderDetail = new OrderSummary()
                    {
                        OrderId = orderNumber++,
                        OrderDate = new DateTime(ran.Next(2004, 2010), ran.Next(1, 12), ran.Next(1, 27)),
                        ProductName = prodcutName[prodcutIndex],
                        Quantity = ran.Next(1, 8),
                        UnitPrice = prodcutPrice[prodcutIndex],
                        Discount = 0.0000
                    };
                    orderDetail.ExtPrice = orderDetail.Quantity * orderDetail.UnitPrice;
                    OrderSummaryCollection.Add(orderDetail);
                }
                return OrderSummaryCollection;
            }
        }
    }


    public class SalesDashboard : ReportViewerSampleHelper
    {
        public SalesDashboard(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ProductShowcase";
            this.ReportName = "Sales Dashboard";
        }

        public override void SetParameter()
        {
            this.ReportViewer.SetParameters(this.GetParameters());
        }

        public override void UpdateDataSet()
        {
            setDataSource();
        }

        public void setDataSource()
        {
            ReportParameterInfoCollection paramCollection = this.ReportViewer.GetParameters();
            string Year = paramCollection.Where(p => p.Name.Equals("SalesYearParameter")).FirstOrDefault().Values.FirstOrDefault();

            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "TopSalesPerson", Value = SalesPersons.GetTopSalesPerson(int.Parse(Year)) });
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "TopStores", Value = Stores.GetTopStores(int.Parse(Year)) });
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "TopProduct", Value = Products.GetTopProducts(int.Parse(Year)) });

        }

        public IList<ReportParameter> GetParameters()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            ReportParameter param = new ReportParameter();
            param.Labels.Add("2001");
            param.Values.Add("2001");
            param.Name = "SalesYearParameter";
            parameters.Add(param);
            return parameters;
        }
        public class SalesPersons
        {
            public string Name { get; set; }
            public double QS1 { get; set; }
            public double QS2 { get; set; }
            public double QS3 { get; set; }
            public double QS4 { get; set; }
            public double Total { get; set; }
            public int Year { get; set; }
            public static IList GetTopSalesPerson(int year)
            {
                List<SalesPersons> SalesPersonCollection = new List<SalesPersons>();
                SalesPersons salesPerson = null;
                salesPerson = new SalesPersons()
                {
                    Name = "Carol Elliott",
                    QS3 = 656287.9884,
                    QS4 = 1006810.7219,
                    Total = 1663098.7103,
                    Year = 2001
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Linda Ecoffey",
                    QS3 = 547589.0181,
                    QS4 = 951811.3768,
                    Total = 1499400.3949,
                    Year = 2001
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Shelley Dyck",
                    QS3 = 551800.5660,
                    QS4 = 823059.5628,
                    Total = 1374860.1288,
                    Year = 2001
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Michael Emanuel",
                    QS3 = 515235.5282,
                    QS4 = 733617.1174,
                    Total = 1248852.6456,
                    Year = 2001
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Jauna Elson",
                    QS3 = 453982.3228,
                    QS4 = 614458.9850,
                    Total = 1068441.3078,
                    Year = 2001
                };
                SalesPersonCollection.Add(salesPerson);

                salesPerson = new SalesPersons()
                {
                    Name = "Linda Ecoffey",
                    QS1 = 781351.2313,
                    QS2 = 1028144.2245,
                    QS3 = 1531824.7788,
                    QS4 = 1263787.6537,
                    Total = 4605107.8883,
                    Year = 2002
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Shelley Dyck",
                    QS1 = 710292.4523,
                    QS2 = 714876.5727,
                    QS3 = 1415932.4341,
                    QS4 = 1142145.8485,
                    Total = 3983247.3076,
                    Year = 2002
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Maciej Dusza",
                    QS1 = 489810.4459,
                    QS2 = 558046.5501,
                    QS3 = 1516710.9716,
                    QS4 = 1179535.1114,
                    Total = 3744103.0790,
                    Year = 2002
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Gail Erickson",
                    QS1 = 0,
                    QS2 = 0,
                    QS3 = 1739975.7306,
                    QS4 = 1306538.9169,
                    Total = 3046514.6475,
                    Year = 2002
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Carol Elliott",
                    QS1 = 744774.7917,
                    QS2 = 834823.5107,
                    QS3 = 812349.5603,
                    QS4 = 653131.5843,
                    Total = 3045079.4470,
                    Year = 2002
                };
                SalesPersonCollection.Add(salesPerson);

                salesPerson = new SalesPersons()
                {
                    Name = "Gail Erickson",
                    QS1 = 981735.7977,
                    QS2 = 1258793.8673,
                    QS3 = 1476692.0028,
                    QS4 = 1356994.7597,
                    Total = 5074216.4275,
                    Year = 2003
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Shelley Dyck",
                    QS1 = 925501.3401,
                    QS2 = 1163645.8204,
                    QS3 = 1622531.0508,
                    QS4 = 1284147.5293,
                    Total = 4995825.740,
                    Year = 2003
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Maciej Dusza",
                    QS1 = 820852.0529,
                    QS2 = 1226808.7576,
                    QS3 = 1497942.9170,
                    QS4 = 1200475.2000,
                    Total = 4746078.9275,
                    Year = 2003
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Linda Ecoffey",
                    QS1 = 917504.6713,
                    QS2 = 1278750.6036,
                    QS3 = 1252426.1780,
                    QS4 = 953423.5345,
                    Total = 4402104.9874,
                    Year = 2003
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Jauna Elson",
                    QS1 = 524679.7564,
                    QS2 = 626051.3386,
                    QS3 = 948047.6240,
                    QS4 = 777144.9274,
                    Total = 2875923.6464,
                    Year = 2003
                };
                SalesPersonCollection.Add(salesPerson);

                salesPerson = new SalesPersons()
                {
                    Name = "Shelley Dyck",
                    QS1 = 1026805.5472,
                    QS2 = 1266991.1038,
                    Total = 2293796.6510,
                    Year = 2004
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Gail Erickson",
                    QS1 = 934697.3750,
                    QS2 = 1247298.2376,
                    Total = 2181995.6126,
                    Year = 2004
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Maciej Dusza",
                    QS1 = 875242.441,
                    QS2 = 983384.4873,
                    Total = 1858626.9289,
                    Year = 2004
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Mark Erickson",
                    QS1 = 715362.2737,
                    QS2 = 943155.7965,
                    Total = 1658518.0702,
                    Year = 2004
                };
                SalesPersonCollection.Add(salesPerson);
                salesPerson = new SalesPersons()
                {
                    Name = "Linda Ecoffey",
                    QS1 = 731927.6488,
                    QS2 = 919386.2718,
                    Total = 1651313.9206,
                    Year = 2004
                };
                SalesPersonCollection.Add(salesPerson);

                return SalesPersonCollection.Where(spc => spc.Year == year).ToList();
            }
        }

        public class Stores
        {
            public string Name { get; set; }
            public double QS1 { get; set; }
            public double QS2 { get; set; }
            public double QS3 { get; set; }
            public double QS4 { get; set; }
            public double Total { get; set; }
            public int year { get; set; }
            public static IList GetTopStores(int year)
            {
                List<Stores> StoreCollection = new List<Stores>();
                Stores store = null;
                store = new Stores()
                {
                    Name = "Bicycle Company",
                    QS3 = 656287.9884,
                    QS4 = 1006810.7219,
                    Total = 1663098.7103,
                    year = 2001
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Activity Center",
                    QS3 = 656287.9884,
                    QS4 = 1006810.7219,
                    Total = 1663098.7103,
                    year = 2001
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Bike Shop",
                    QS3 = 656287.9884,
                    QS4 = 1006810.7219,
                    Total = 1663098.7103,
                    year = 2001
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Bike Goods ",
                    QS3 = 656287.9884,
                    QS4 = 1006810.7219,
                    Total = 1663098.7103,
                    year = 2001
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Bike Rims",
                    QS3 = 656287.9884,
                    QS4 = 1006810.7219,
                    Total = 1663098.7103,
                    year = 2001
                };
                StoreCollection.Add(store);

                store = new Stores()
                {
                    Name = "Great Bicycle",
                    QS1 = 781351.2313,
                    QS2 = 1028144.2245,
                    QS3 = 1531824.7788,
                    QS4 = 1263787.6537,
                    Total = 4605107.8883,
                    year = 2002
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Bike Shop",
                    QS1 = 781351.2313,
                    QS2 = 1028144.2245,
                    QS3 = 1531824.7788,
                    QS4 = 1263787.6537,
                    Total = 4605107.8883,
                    year = 2002
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Active Systems",
                    QS1 = 781351.2313,
                    QS2 = 1028144.2245,
                    QS3 = 1531824.7788,
                    QS4 = 1263787.6537,
                    Total = 4605107.8883,
                    year = 2002
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Advanced Bike",
                    QS1 = 781351.2313,
                    QS2 = 1028144.2245,
                    QS3 = 1531824.7788,
                    QS4 = 1263787.6537,
                    Total = 4605107.8883,
                    year = 2002
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Seasons Sports",
                    QS1 = 781351.2313,
                    QS2 = 1028144.2245,
                    QS3 = 1531824.7788,
                    QS4 = 1263787.6537,
                    Total = 4605107.8883,
                    year = 2002
                };
                StoreCollection.Add(store);

                store = new Stores()
                {
                    Name = "Action Bicycle",
                    QS1 = 981735.7977,
                    QS2 = 1258793.8673,
                    QS3 = 1476692.0028,
                    QS4 = 1356994.7597,
                    Total = 5074216.4275,
                    year = 2003
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Area Sheet",
                    QS1 = 981735.7977,
                    QS2 = 1258793.8673,
                    QS3 = 1476692.0028,
                    QS4 = 1356994.7597,
                    Total = 5074216.4275,
                    year = 2003
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Authentic Sales",
                    QS1 = 981735.7977,
                    QS2 = 1258793.8673,
                    QS3 = 1476692.0028,
                    QS4 = 1356994.7597,
                    Total = 5074216.4275,
                    year = 2003
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Discount Stores",
                    QS1 = 981735.7977,
                    QS2 = 1258793.8673,
                    QS3 = 1476692.0028,
                    QS4 = 1356994.7597,
                    Total = 5074216.4275,
                    year = 2003
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Central Bicycle",
                    QS1 = 981735.7977,
                    QS2 = 1258793.8673,
                    QS3 = 1476692.0028,
                    QS4 = 1356994.7597,
                    Total = 5074216.4275,
                    year = 2003
                };
                StoreCollection.Add(store);

                store = new Stores()
                {
                    Name = "Aerobic Exercise",
                    QS1 = 1026805.5472,
                    QS2 = 1266991.1038,
                    Total = 2293796.6510,
                    year = 2004
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Alpine House",
                    QS1 = 1026805.5472,
                    QS2 = 1266991.1038,
                    Total = 2293796.6510,
                    year = 2004
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Sporting Goods",
                    QS1 = 1026805.5472,
                    QS2 = 1266991.1038,
                    Total = 2293796.6510,
                    year = 2004
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Basic Sports",
                    QS1 = 1026805.5472,
                    QS2 = 1266991.1038,
                    Total = 2293796.6510,
                    year = 2004
                };
                StoreCollection.Add(store);
                store = new Stores()
                {
                    Name = "Bold Bikes",
                    QS1 = 1026805.5472,
                    QS2 = 1266991.1038,
                    Total = 2293796.6510,
                    year = 2004
                };
                StoreCollection.Add(store);

                return StoreCollection.Where(sc => sc.year == year).ToList();
            }
        }

        public class Products
        {
            public string Name { get; set; }
            public double QS1 { get; set; }
            public double QS2 { get; set; }
            public double QS3 { get; set; }
            public double QS4 { get; set; }
            public double Total { get; set; }
            public int Year { get; set; }
            public static IList GetTopProducts(int year)
            {
                List<Products> ProductCollection = new List<Products>();
                Products product = null;
                product = new Products()
                {
                    Name = "AWC Logo Cap",
                    QS3 = 2104165.3554,
                    QS4 = 3308323.5938,
                    Total = 5412488.9492,
                    Year = 2001
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, B",
                    QS3 = 1988382.4844,
                    QS4 = 3279281.6611,
                    Total = 5267664.1455,
                    Year = 2001
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "L-Sleeve Jrsey",
                    QS3 = 1974839.7878,
                    QS4 = 3282139.2608,
                    Total = 5256979.0486,
                    Year = 2001
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, R",
                    QS3 = 1971943.0387,
                    QS4 = 3140308.6115,
                    Total = 5112251.6502,
                    Year = 2001
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, Bk",
                    QS3 = 2008908.3029,
                    QS4 = 3096292.8994,
                    Total = 5105201.2023,
                    Year = 2001
                };
                ProductCollection.Add(product);

                product = new Products()
                {
                    Name = "AWC Logo Cap",
                    QS1 = 2479585.3057,
                    QS2 = 3246499.0536,
                    QS3 = 6803560.4591,
                    QS4 = 5176061.7865,
                    Total = 17705706.6049,
                    Year = 2002
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "L-Sleeve Jersey",
                    QS1 = 2506478.8038,
                    QS2 = 3451364.1049,
                    QS3 = 6640485.7449,
                    QS4 = 5081732.7792,
                    Total = 17680061.4328,
                    Year = 2002
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, B",
                    QS1 = 1591213.0306,
                    QS2 = 3019373.2897,
                    QS3 = 6681639.5312,
                    QS4 = 4970609.3327,
                    Total = 16262835.1842,
                    Year = 2002
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, R",
                    QS1 = 1200410.2689,
                    QS2 = 2986523.1779,
                    QS3 = 6704405.3528,
                    QS4 = 4796516.5549,
                    Total = 15687855.3545,
                    Year = 2002
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, Bk",
                    QS1 = 1326226.1287,
                    QS2 = 2785577.5765,
                    QS3 = 6702788.2116,
                    QS4 = 4830625.4549,
                    Total = 15645217.3717,
                    Year = 2002
                };
                ProductCollection.Add(product);

                product = new Products()
                {
                    Name = "AWC Logo Cap",
                    QS1 = 3767204.2532,
                    QS2 = 5144768.8936,
                    QS3 = 6850399.6554,
                    QS4 = 5329958.1031,
                    Total = 21092330.9053,
                    Year = 2003
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "L-Sleeve Jersey",
                    QS1 = 3718432.7781,
                    QS2 = 5082651.5234,
                    QS3 = 6479515.2897,
                    QS4 = 4995648.8312,
                    Total = 20276248.4224,
                    Year = 2003
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, B",
                    QS1 = 3001922.0189,
                    QS2 = 4938119.6587,
                    QS3 = 6504481.8462,
                    QS4 = 5092806.3654,
                    Total = 19537329.8892,
                    Year = 2003
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, Bk",
                    QS1 = 2532321.1181,
                    QS2 = 4896278.2250,
                    QS3 = 6517251.1923,
                    QS4 = 5152659.6077,
                    Total = 19098510.1431,
                    Year = 2003
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, R",
                    QS1 = 1444428.4599,
                    QS2 = 5020718.6434,
                    QS3 = 6451496.9938,
                    QS4 = 5015496.1295,
                    Total = 17932140.2266,
                    Year = 2003
                };
                ProductCollection.Add(product);

                product = new Products()
                {
                    Name = "Water Bottle",
                    QS1 = 4121940.2137,
                    QS2 = 5866889.4854,
                    QS3 = 7450.1100,
                    Total = 9996279.8091,
                    Year = 2004
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "AWC Logo Cap",
                    QS1 = 3676842.2034,
                    QS2 = 5407113.3880,
                    QS3 = 5465.0800,
                    Total = 9089420.6714,
                    Year = 2004
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "Classic Vest, S",
                    QS1 = 3794769.6580,
                    QS2 = 4936799.8561,
                    QS3 = 979.0100,
                    Total = 8732548.5241,
                    Year = 2004
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-SC Jersey, XL",
                    QS1 = 3568526.5876,
                    QS2 = 5130984.2023,
                    QS3 = 1761.0300,
                    Total = 8701271.8199,
                    Year = 2004
                };
                ProductCollection.Add(product);
                product = new Products()
                {
                    Name = "S-100 Helmet, B",
                    QS1 = 3156520.2089,
                    QS2 = 5249154.4748,
                    QS3 = 6531.6000,
                    Total = 8412206.2837,
                    Year = 2004
                };
                ProductCollection.Add(product);

                return ProductCollection.Where(pc => pc.Year == year).ToList();
            }
        }
    }


    public class IndicatorDemo : ReportViewerSampleHelper
    {
        public IndicatorDemo(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ReportElement";
            this.ReportName = "IndicatorReport";
        }

        public override void UpdateDataSet()
        {
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet1", Value = Billionaires.GetList_2013() });
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet2", Value = Billionaires.GetList_2012() });
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet3", Value = Indicator.GetIndicator() });
        }

        public class Billionaires
        {
            public int No { get; set; }
            public string Name { get; set; }
            public double NetWorth { get; set; }
            public int Age { get; set; }
            public string CitizenShip { get; set; }
            public string Source { get; set; }
            public int RankingStatus { get; set; }
            public int ProfitStatus { get; set; }

            public Billionaires(int no, string name, double netWorth, int age, string citizenShip, string source, int status, int profit)
            {
                this.No = no;
                this.Name = name;
                this.NetWorth = netWorth;
                this.Age = age;
                this.CitizenShip = citizenShip;
                this.Source = source;
                this.RankingStatus = status;
                this.ProfitStatus = profit;
            }

            public static List<Billionaires> GetList_2013()
            {
                List<Billionaires> list_2013 = new List<Billionaires>();
                list_2013.Add(new Billionaires(1, "Carlos Slim", 73.0, 73, "Mexico", "Telmex,América Móvil, Grupo Carso", 50, 75));
                list_2013.Add(new Billionaires(2, "Bill Gates", 67.0, 57, "United States", "Microsoft", 50, 75));
                list_2013.Add(new Billionaires(3, "Amancio Ortega", 57.0, 76, "Spain", "Inditex Group", 75, 75));
                list_2013.Add(new Billionaires(4, "Warren Buffett", 53.0, 82, "United States", "Berkshire Hathaway", 25, 75));
                list_2013.Add(new Billionaires(5, "Larry Ellison", 43.0, 68, "United States", "Oracle Corporation", 75, 75));
                list_2013.Add(new Billionaires(6, "Charles Koch", 34.0, 77, "United States", "Koch Industries", 75, 75));
                list_2013.Add(new Billionaires(7, "David Koch", 34.0, 72, "United States", "Koch Industries", 75, 75));
                list_2013.Add(new Billionaires(8, "Li Ka-shing", 32.0, 84, "Hong Kong/ Canada", "Cheung Kong Holdings", 75, 75));
                list_2013.Add(new Billionaires(9, "Liliane Bettencourt", 30.0, 90, "France", "L'Oréal", 75, 75));
                list_2013.Add(new Billionaires(10, "Bernard Arnault", 29.0, 63, "France", "LVMH Moët Hennessy Louis Vuitton", 25, 25));
                return list_2013;
            }

            public static List<Billionaires> GetList_2012()
            {
                List<Billionaires> list_2012 = new List<Billionaires>();
                list_2012.Add(new Billionaires(1, "Carlos Slim", 69.0, 72, "Mexico", "Telmex,América Móvil, Grupo Carso", 50, 25));
                list_2012.Add(new Billionaires(2, "Bill Gates", 61.0, 56, "United States", "Microsoft", 50, 75));
                list_2012.Add(new Billionaires(3, "Warren Buffett", 44.0, 81, "United States", "Berkshire Hathaway", 50, 25));
                list_2012.Add(new Billionaires(4, "Bernard Arnault", 41.0, 63, "France", "LVMH Moët Hennessy Louis Vuitton", 50, 75));
                list_2012.Add(new Billionaires(5, "Amancio Ortega", 37.5, 75, "Spain", "Inditex Group", 75, 75));
                list_2012.Add(new Billionaires(6, "Larry Ellison", 36.0, 67, "United States", "Oracle Corporation", 25, 75));
                list_2012.Add(new Billionaires(7, "Eike Batista", 30.0, 55, " Brazil", "EBX Group", 75, 75));
                list_2012.Add(new Billionaires(8, "Stefan Persson", 26.0, 64, "Sweden", "H&M", 75, 75));
                list_2012.Add(new Billionaires(9, "Li Ka-shing", 25.5, 83, "Hong Kong/ Canada", "Cheung Kong Holdings", 75, 75));
                list_2012.Add(new Billionaires(10, "Karl Albrecht", 25.4, 92, "Germany", "Aldi", 75, 75));
                return list_2012;
            }
        }

        public class Indicator
        {
            public int Status { get; set; }
            public string Description { get; set; }

            public Indicator(int status, string description)
            {
                this.Status = status;
                this.Description = description;
            }

            public static List<Indicator> GetIndicator()
            {
                List<Indicator> ind = new List<Indicator>();
                ind.Add(new Indicator(25, "Has not changed from the previous ranking."));
                ind.Add(new Indicator(50, "Has increased from the previous ranking."));
                ind.Add(new Indicator(75, "Has decreased from the previous ranking."));
                return ind;
            }
        }
    }

#endif

    public class SalesOrderDemo : ReportViewerSampleHelper
    {
        public SalesOrderDemo(SfReportViewer reportViewerControl)
        {
            this.ReportViewer = reportViewerControl;
            this.FolderPath = "ProductShowcase";
            this.ReportName = "SalesOrderDetail";
        }

        public override void SetParameter()
        {
            this.ReportViewer.SetParameters(this.GetParameters());
        }

        public override void UpdateDataSet()
        {
            SetDataSource();
        }

        void SetDataSource()
        {
            ReportParameterInfoCollection paramCollection = this.ReportViewer.GetParameters();
            string salesOrderNumber = paramCollection.Where(param => param.Name.Equals("SalesOrderNumber")).FirstOrDefault().Values.FirstOrDefault();
            this.ReportViewer.DataSources.Clear();
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "SalesOrder", Value = SalesOrder.GetsalesOrderDetail(salesOrderNumber) });
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "SalesOrderDetail", Value = SalesOrderDetail.GetsalesOrderDetail(salesOrderNumber.TrimStart("SO".ToCharArray())) });
            this.ReportViewer.DataSources.Add(new ReportDataSource { Name = "SalesOrderNumber", Value = SalesOrderNumbers.GetSalesOrderNumbers() });
        }

        IList<ReportParameter> GetParameters()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            ReportParameter param = new ReportParameter();
            param.Labels.Add("SO50750");
            param.Values.Add("SO50750");
            param.Name = "SalesOrderNumber";
            parameters.Add(param);
            return parameters;
        }

        public class SalesOrder
        {
            public string SalesOrderNumber { get; set; }
            public string Store { get; set; }
            public string OrderDate { get; set; }
            public string SalesFirstName { get; set; }
            public string SalesLastName { get; set; }
            public string SalesTitle { get; set; }
            public string PurchaseOrderNumber { get; set; }
            public string ShipMethod { get; set; }
            public string BillAddress1 { get; set; }
            public string BillCity { get; set; }
            public string BillPostalCode { get; set; }
            public string BillStateProvince { get; set; }
            public string BillCountryRegion { get; set; }
            public string ShipAddress1 { get; set; }
            public string ShipCity { get; set; }
            public string ShipPostalCode { get; set; }
            public string ShipStateProvince { get; set; }
            public string ShipCountryRegion { get; set; }
            public string CustPhone { get; set; }
            public string CustFirstName { get; set; }
            public string CustLastName { get; set; }
            public static IList GetsalesOrderDetail(string salesOrderId)
            {
                List<SalesOrder> SalesOrderCollection = new List<SalesOrder>();
                SalesOrder salesOrder = null;
                salesOrder = new SalesOrder()
                {
                    SalesOrderNumber = "SO50750",
                    Store = "Central Discount Store",
                    OrderDate = "2003-06-01T00:00:00-04:00",
                    SalesFirstName = "David",
                    SalesLastName = "Campbell",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO7192170677",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "259826 Russell Rd. South",
                    BillCity = "Kent",
                    BillPostalCode = "98031",
                    BillStateProvince = "Washington",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "259826 Russell Rd. South",
                    ShipCity = "Kent",
                    ShipPostalCode = "98031",
                    ShipStateProvince = "Washington",
                    ShipCountryRegion = "United States",
                    CustPhone = "582-555-0113",
                    CustFirstName = "Jean",
                    CustLastName = "Handley"
                };
                SalesOrderCollection.Add(salesOrder);

                salesOrder = new SalesOrder()
                {
                    SalesOrderNumber = "SO50751",
                    Store = "Responsible Bike Dealers",
                    OrderDate = "2003-06-01T00:00:00-04:00",
                    SalesFirstName = "Michael",
                    SalesLastName = "Blythe",
                    SalesTitle = "Sales Representative",
                    PurchaseOrderNumber = "PO7018191419",
                    ShipMethod = "CARGO TRANSPORT 5",
                    BillAddress1 = "Ward Parkway Center",
                    BillCity = "Kansas City",
                    BillPostalCode = "64106",
                    BillStateProvince = "Missouri",
                    BillCountryRegion = "United States",
                    ShipAddress1 = "Ward Parkway Center",
                    ShipCity = "Kansas City",
                    ShipPostalCode = "64106",
                    ShipStateProvince = "Missouri",
                    ShipCountryRegion = "United States",
                    CustPhone = "620-555-0117",
                    CustFirstName = "Rob",
                    CustLastName = "Caron"
                };
                SalesOrderCollection.Add(salesOrder);
                return SalesOrderCollection.Where(soid => soid.SalesOrderNumber.Equals(salesOrderId)).ToList();
            }
        }
        public class SalesOrderDetail
        {
            public string SalesOrderDetailID { get; set; }
            public string OrderQty { get; set; }
            public double UnitPrice { get; set; }
            public string UnitPriceDiscount { get; set; }
            public double LineTotal { get; set; }
            public string CarrierTrackingNumber { get; set; }
            public string SalesOrderID { get; set; }
            public string Name { get; set; }
            public string ProductNumber { get; set; }
            public static IList GetsalesOrderDetail(string salesOrderId)
            {
                List<SalesOrderDetail> SalesOrderDetailCollection = new List<SalesOrderDetail>();
                SalesOrderDetail salesOrderDetail = null;
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35136",
                    OrderQty = "2",
                    UnitPrice = 5.1865,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 10.373000,
                    CarrierTrackingNumber = "373D-417C-AE",
                    SalesOrderID = "50750",
                    Name = "AWC Logo Cap",
                    ProductNumber = "CA-1098"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35137",
                    OrderQty = "4",
                    UnitPrice = 22.7940,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 91.176000,
                    CarrierTrackingNumber = "373D-417C-AE",
                    SalesOrderID = "50750",
                    Name = "Full-Finger Gloves, M",
                    ProductNumber = "GL-F110-M"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35138",
                    OrderQty = "4",
                    UnitPrice = 28.8404,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 115.361600,
                    CarrierTrackingNumber = "373D-417C-AE",
                    SalesOrderID = "50750",
                    Name = "Long-Sleeve Logo Jersey, L",
                    ProductNumber = "LJ-0192-L"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35139",
                    OrderQty = "3",
                    UnitPrice = 22.7940,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 68.382000,
                    CarrierTrackingNumber = "373D-417C-AE",
                    SalesOrderID = "50750",
                    Name = "Full-Finger Gloves, L",
                    ProductNumber = "GL-F110-L"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35140",
                    OrderQty = "1",
                    UnitPrice = 1229.4589,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 1229.458900,
                    CarrierTrackingNumber = "373D-417C-AE",
                    SalesOrderID = "50750",
                    Name = "Mountain-200 Black, 42",
                    ProductNumber = "BK-M68B-42"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35141",
                    OrderQty = "1",
                    UnitPrice = 44.9940,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 44.994000,
                    CarrierTrackingNumber = "373D-417C-AE",
                    SalesOrderID = "50750",
                    Name = "Women's Tights, L",
                    ProductNumber = "TG-W091-L"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35142",
                    OrderQty = "1",
                    UnitPrice = 1242.8518,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 1242.851800,
                    CarrierTrackingNumber = "373D-417C-AE",
                    SalesOrderID = "50750",
                    Name = "Mountain-200 Silver, 38",
                    ProductNumber = "BK-M68S-38"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35143",
                    OrderQty = "2",
                    UnitPrice = 22.7940,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 45.588000,
                    CarrierTrackingNumber = "5750-45C6-9C",
                    SalesOrderID = "50751",
                    Name = "Full-Finger Gloves, M",
                    ProductNumber = "GL-F110-M"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35144",
                    OrderQty = "6",
                    UnitPrice = 5.1865,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 31.119000,
                    CarrierTrackingNumber = "5750-45C6-9C",
                    SalesOrderID = "50751",
                    Name = "AWC Logo Cap",
                    ProductNumber = "CA-1098"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35145",
                    OrderQty = "1",
                    UnitPrice = 44.9940,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 44.994000,
                    CarrierTrackingNumber = "5750-45C6-9C",
                    SalesOrderID = "50751",
                    Name = "Women's Tights, L",
                    ProductNumber = "TG-W091-L"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35146",
                    OrderQty = "3",
                    UnitPrice = 22.7940,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 68.382000,
                    CarrierTrackingNumber = "5750-45C6-9C",
                    SalesOrderID = "50751",
                    Name = "Full-Finger Gloves, L",
                    ProductNumber = "GL-F110-L"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35147",
                    OrderQty = "1",
                    UnitPrice = 1229.4589,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 1229.458900,
                    CarrierTrackingNumber = "5750-45C6-9C",
                    SalesOrderID = "50751",
                    Name = "Mountain-200 Black, 38",
                    ProductNumber = "BK-M68B-38"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35148",
                    OrderQty = "2",
                    UnitPrice = 22.7940,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 45.588000,
                    CarrierTrackingNumber = "5750-45C6-9C",
                    SalesOrderID = "50751",
                    Name = "Full-Finger Gloves, S",
                    ProductNumber = "GL-F110-S"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                salesOrderDetail = new SalesOrderDetail()
                {
                    SalesOrderDetailID = "35149",
                    OrderQty = "1",
                    UnitPrice = 28.8404,
                    UnitPriceDiscount = "0.0000",
                    LineTotal = 28.840400,
                    CarrierTrackingNumber = "5750-45C6-9C",
                    SalesOrderID = "50751",
                    Name = "Long-Sleeve Logo Jersey, L",
                    ProductNumber = "LJ-0192-L"
                };
                SalesOrderDetailCollection.Add(salesOrderDetail);
                return SalesOrderDetailCollection.Where(soid => soid.SalesOrderID.Equals(salesOrderId)).ToList();
            }
        }

        public class SalesOrderNumbers
        {
            public string SalesOrderNumber { get; set; }
            public static IList GetSalesOrderNumbers()
            {
                List<SalesOrderNumbers> salesNumberCollection = new List<SalesOrderNumbers>();
                SalesOrderNumbers orderNumber = null;
                orderNumber = new SalesOrderNumbers()
                {
                    SalesOrderNumber = "SO50750"
                };
                salesNumberCollection.Add(orderNumber);
                orderNumber = new SalesOrderNumbers()
                {
                    SalesOrderNumber = "SO50751"
                };
                salesNumberCollection.Add(orderNumber);
                return salesNumberCollection;
            }
        }
    }
}
