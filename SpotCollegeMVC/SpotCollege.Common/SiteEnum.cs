using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotCollege.Common
{
    public enum LoginTypes
    {
        Student = 1,
        University = 2,
        Admin = 3,
        SubAdmin = 4
    }

    public enum CurrencyTypes
    {
        [Description("Abkhazia(RUB)")]
        Abkhazia = 1,
        [Description("Afghanistan(AFN)")]
        Afghanistan = 2,
        [Description("Akrotiri and Dhekelia(EUR)")]
        Akrotiri_and_Dhekelia = 3,
        [Description("Albania(ALL)")]
        Albania = 4,
        [Description("Alderney(GBP)")]
        Alderney = 5,
        [Description("Algeria(DZD)")]
        Algeria = 6,
        [Description("Andorra(EUR)")]
        Andorra = 7,
        [Description("Angola(AOA)")]
        Angola = 8,
        [Description("Anguilla(XCD)")]
        Anguilla = 9,
        [Description("Antigua and Barbuda(XCD)")]
        Antigua_and_Barbuda = 10,
        [Description("Argentina(ARS)")]
        Argentina = 11,
        [Description("Armenia(AMD)")]
        Armenia = 12,
        [Description("Aruba(AWG)")]
        Aruba = 13,
        [Description("Ascension Island(SHP)")]
        Ascension_Island = 14,
        [Description("Australia(AUD)")]
        Australia = 15,
        [Description("Austria(EUR)")]
        Austria = 16,
        [Description("Azerbaijan(AZN)")]
        Azerbaijan = 17,
        [Description("Bahamas(BSD)")]
        Bahamas = 18,
        [Description("Bahrain(BHD)")]
        Bahrain = 19,
        [Description("Bangladesh(BDT)")]
        Bangladesh = 20,
        [Description("Barbados(BBD)")]
        Barbados = 21,
        [Description("Belarus(BYR)")]
        Belarus = 22,
        [Description("Belgium(EUR)")]
        Belgium = 23,
        [Description("Belize(BZD)")]
        Belize = 24,
        [Description("Benin(XOF)")]
        Benin = 25,
        [Description("Bermuda(BMD)")]
        Bermuda = 26,
        [Description("Bhutan(BTN)")]
        Bhutan = 27,
        [Description("Bolivia(BOB)")]
        Bolivia = 28,
        [Description("Bonaire(USD)")]
        Bonaire = 29,
        [Description("Bosnia Herzegovina(BAM)")]
        Bosnia_Herzegovina = 30,
        [Description("Botswana(BWP)")]
        Botswana = 31,
        [Description("Brazil(BRL)")]
        Brazil = 32,
        [Description("British Indian Ocean Territory(USD)")]
        British_Indian_Ocean_Territory = 33,
        [Description("British Virgin Islands(USD)")]
        British_Virgin_Islands = 34,
        [Description("Brunei(BND)")]
        Brunei = 35,
        [Description("Bulgaria(BGN)")]
        Bulgaria = 36,
        [Description("Burkina Faso(XOF)")]
        Burkina_Faso = 37,
        [Description("Burma(MMK)")]
        Burma = 38,
        [Description("Burundi(BIF)")]
        Burundi = 39,
        [Description("Cambodia(KHR)")]
        Cambodia = 40,
        [Description("Cameroon(XAF)")]
        Cameroon = 41,
        [Description("Canada(CAD)")]
        Canada = 42,
        [Description("Cape Verde(CVE)")]
        Cape_Verde = 43,
        [Description("Cayman Islands(KYD)")]
        Cayman_Islands = 44,
        [Description("Central African Republic(XAF)")]
        Central_African_Republic = 45,
        [Description("Chad(XAF)")]
        Chad = 46,
        [Description("Chile(CLP)")]
        Chile = 47,
        [Description("China(CNY)")]
        China = 48,
        [Description("Christmas Island(AFN)")]
        Christmas_Island = 49,
        [Description("Cocos Keeling Islands(AUD)")]
        Cocos_Keeling_Islands = 50,
        [Description("Colombia(COP)")]
        Colombia = 51,
        [Description("Comoros(KMF)")]
        Comoros = 52,
        [Description("Congo Democratic Republic of the Zaire(CDF)")]
        Congo_Democratic_Republic_of_the_Zaire = 53,
        [Description("Congo Republic of(XAF)")]
        Congo_Republic_of = 54,
        [Description("Cook Islands(NZD)")]
        Cook_Islands = 55,
        [Description("Costa Rica(CRC)")]
        Costa_Rica = 56,
        [Description("Croatia(HRK)")]
        Croatia = 57,
        [Description("Cuba(CUC)")]
        Cuba = 58,
        [Description("Cyprus(EUR)")]
        Cyprus = 59,
        [Description("Czech Republic(CZK)")]
        Czech_Republic = 60,
        [Description("Denmark(DKK)")]
        Denmark = 61,
        [Description("Djibouti(DJF)")]
        Djibouti = 62,
        [Description("Dominica(XCD)")]
        Dominica = 63,
        [Description("Dominican Republic(DOP)")]
        Dominican_Republic = 64,
        [Description("Ecuador(USD)")]
        Ecuador = 65,
        [Description("Egypt(USD)")]
        Egypt = 66,
        [Description("El Salvador(SVC)")]
        El_Salvador = 67,
        [Description("Equatorial Guinea(XAF)")]
        Equatorial_Guinea = 68,
        [Description("Eritrea(ERN)")]
        Eritrea = 69,
        [Description("Estonia(EUR)")]
        Estonia = 70,
        [Description("Ethiopia(ETB)")]
        Ethiopia = 71,
        [Description("Falkland Islands(FKP)")]
        Falkland_Islands = 72,
        [Description("Faroe Islands(AFN)")]
        Faroe_Islands = 73,
        [Description("Fiji(FJD)")]
        Fiji = 74,
        [Description("Finland(EUR)")]
        Finland = 75,
        [Description("France(EUR)")]
        France = 76,

        [Description("French Polynesia(XPF)")]
        French_Polynesia = 77,
        [Description("Gabon(XAF)")]
        Gabon = 78,
        [Description("Gambia(GMD)")]
        Gambia = 79,
        [Description("Georgia(GEL)")]
        Georgia = 80,
        [Description("Germany(EUR)")]
        Germany = 81,
        [Description("Ghana(GHS)")]
        Ghana = 82,
        [Description("Gibraltar(GIP)")]
        Gibraltar = 83,
        [Description("Greece(EUR)")]
        Greece = 84,

        [Description("Grenada(XCD)")]
        Grenada = 85,

        [Description("Guatemala(GTQ)")]
        Guatemala = 86,
        [Description("Guinea(GNF)")]
        Guinea = 87,
        [Description("Guinea Bissau(XOF)")]
        Guinea_Bissau = 88,
        [Description("Guyana(GYD)")]
        Guyana = 89,
        [Description("Haiti(HTG)")]
        Haiti = 90,
        [Description("Honduras(HNL)")]
        Honduras = 91,
        [Description("Hong Kong(HKD)")]
        Hong_Kong = 92,
        [Description("Hungary(HUF)")]
        Hungary = 93,
        [Description("Iceland(ISK)")]
        Iceland = 94,
        [Description("India(INR)")]
        India = 95,
        [Description("Indonesia(IDR)")]
        Indonesia = 96,
        [Description("Iran(IRR)")]
        Iran = 97,
        [Description("Iraq(IQD)")]
        Iraq = 98,
        [Description("Ireland(EUR)")]
        Ireland = 99,
        [Description("Israel(ILS)")]
        Israel = 100,
        [Description("Italy(EUR)")]
        Italy = 101,

        [Description("Jamaica(JMD)")]
        Jamaica = 102,
        [Description("Japan(JPY)")]
        Japan = 103,
        [Description("Jordan(JOD)")]
        Jordan = 104,
        [Description("Kazakhstan(KZT)")]
        Kazakhstan = 105,
        [Description("Kenya(KES)")]
        Kenya = 106,

        [Description("Kuwait(KWD)")]
        Kuwait = 107,
        [Description("Kyrgyzstan(KGS)")]
        Kyrgyzstan = 108,
        [Description("Laos(LAK)")]
        Laos = 109,
        [Description("Latvia(LVL)")]
        Latvia = 110,
        [Description("Lebanon(LBP)")]
        Lebanon = 111,
        [Description("Lesotho(LSL)")]
        Lesotho = 112,
        [Description("Liberia(LRD)")]
        Liberia = 113,
        [Description("Libya(LYD)")]
        Libya = 114,
        [Description("Liechtenstein(CHF)")]
        Liechtenstein = 115,
        [Description("Lithuania(LTL)")]
        Lithuania = 116,
        [Description("Luxembourg(EUR)")]
        Luxembourg = 117,
        [Description("Macau(MOP)")]
        Macau = 118,
        [Description("Macedonia(MKD)")]
        Macedonia = 119,
        [Description("Madagascar(MGA)")]
        Madagascar = 120,
        [Description("Malawi(MWK)")]
        Malawi = 121,
        [Description("Malaysia(MYR)")]
        Malaysia = 122,
        [Description("Maldives(MVR)")]
        Maldives = 123,
        [Description("Mali(XOF)")]
        Mali = 124,
        [Description("Malta(EUR)")]
        Malta = 125,
        [Description("Marshall Islands(USD)")]
        Marshall_Islands = 126,
        [Description("Kiribati(AUD)")]
        Kiribati = 127,
        [Description("Mauritania(MRO)")]
        Mauritania = 128,
        [Description("Mauritius(MUR)")]
        Mauritius = 129,
        [Description("Mexico(MXN)")]
        Mexico = 131,
        [Description("Micronesia(USD)")]
        Micronesia = 132,
        [Description("Moldova(MDL)")]
        Moldova = 133,
        [Description("Monaco(EUR)")]
        Monaco = 134,
        [Description("Mongolia(MNT)")]
        Mongolia = 135,
        [Description("Montenegro(EUR)")]
        Montenegro = 136,
        [Description("Montserrat(XCD)")]
        Montserrat = 137,
        [Description("Morocco(MAD)")]
        Morocco = 138,
        [Description("Mozambique(MZN)")]
        Mozambique = 139,

        [Description("Namibia(NAD)")]
        Namibia = 141,
        [Description("Nauru(AUD)")]
        Nauru = 142,
        [Description("Nepal(NPR)")]
        Nepal = 143,
        [Description("Netherlands(EUR)")]
        Netherlands = 144,

        [Description("New Caledonia French(XPF)")]
        New_Caledonia_French = 146,
        [Description("New Zealand(NZD)")]
        New_Zealand = 147,
        [Description("Nicaragua(NIO)")]
        Nicaragua = 148,
        [Description("Niger(XOF)")]
        Niger = 149,
        [Description("Nigeria(NGN)")]
        Nigeria = 150,
        [Description("Niue(NZD)")]
        Niue = 151,

        [Description("Norway(NOK)")]
        Norway = 155,
        [Description("Oman(OMR)")]
        Oman = 156,
        [Description("Pakistan(PKR)")]
        Pakistan = 157,
        [Description("Palau(USD)")]
        Palau = 158,
        [Description("Panama(PAB)")]
        Panama = 159,
        [Description("Papua New Guinea(PGK)")]
        Papua_New_Guinea = 160,
        [Description("Paraguay(PYG)")]
        Paraguay = 161,
        [Description("Peru(PEN)")]
        Peru = 162,
        [Description("Philippines(PHP)")]
        Philippines = 163,
        [Description("Pitcairn Island(NZD)")]
        Pitcairn_Island = 164,
        [Description("Poland()")]
        Poland = 165,
        [Description("Polynesia French(AFN)")]
        Polynesia_French = 166,
        [Description("Portugal(EUR)")]
        Portugal = 167,

        [Description("Qatar(QAR)")]
        Qatar = 169,

        [Description("Romania(RON)")]
        Romania = 171,
        [Description("Russia(RUB)")]
        Russia = 172,
        [Description("Rwanda(RWF)")]
        Rwanda = 173,
        [Description("Saint Helena(SHP)")]
        Saint_Helena = 174,
        [Description("Saint Kitts and Nevis(XCD)")]
        Saint_Kitts_and_Nevis = 175,
        [Description("Saint Lucia(XCD)")]
        Saint_Lucia = 176,
        [Description("Saint Pierre and Miquelon(XCD)")]
        Saint_Pierre_and_Miquelon = 177,
        [Description("Saint Vincent and Grenadines(XCD)")]
        Saint_Vincent_and_Grenadines = 178,
        [Description("Samoa(WST)")]
        Samoa = 179,
        [Description("San Marino(EUR)")]
        San_Marino = 180,
        [Description("Sao Tome and Principe(STD)")]
        Sao_Tome_and_Principe = 181,
        [Description("Saudi Arabia(SAR)")]
        Saudi_Arabia = 182,
        [Description("Senegal(XOF)")]
        Senegal = 183,
        [Description("Serbia(RSD)")]
        Serbia = 184,
        [Description("Seychelles(SCR)")]
        Seychelles = 185,
        [Description("Sierra Leone(SLL)")]
        Sierra_Leone = 186,
        [Description("Singapore(BND)")]
        Singapore = 187,
        [Description("Slovakia(EUR)")]
        Slovakia = 188,
        [Description("Slovenia(EUR)")]
        Slovenia = 189,
        [Description("Solomon Islands(AFN)")]
        Solomon_Islands = 190,
        [Description("Somalia(SBD)")]
        Somalia = 191,
        [Description("South Africa(ZAR)")]
        South_Africa = 192,
        [Description("South Georgia and South Sandwich Islands(GBP)")]
        South_Georgia_and_South_Sandwich_Islands = 193,

        [Description("Spain(EUR)")]
        Spain = 195,
        [Description("Sri Lanka(LKR)")]
        Sri_Lanka = 196,
        [Description("Sudan(SDG)")]
        Sudan = 197,
        [Description("Suriname(SRD)")]
        Suriname = 198,

        [Description("Swaziland(SZL)")]
        Swaziland = 200,
        [Description("Sweden(SEK)")]
        Sweden = 201,
        [Description("Switzerland(CHF)")]
        Switzerland = 202,
        [Description("Syria(SYP)")]
        Syria = 203,
        [Description("Taiwan(TWD)")]
        Taiwan = 204,
        [Description("Tajikistan(TJS)")]
        Tajikistan = 205,
        [Description("Tanzania(TZS)")]
        Tanzania = 206,
        [Description("Thailand(THB)")]
        Thailand = 207,

        [Description("Togo(XOF)")]
        Togo = 209,

        [Description("Tonga(TOP)")]
        Tonga = 211,
        [Description("Trinidad and Tobago(TTD)")]
        Trinidad_and_Tobago = 212,
        [Description("Tunisia(TND)")]
        Tunisia = 213,
        [Description("Turkey(TRY)")]
        Turkey = 214,
        [Description("Turkmenistan(TMT)")]
        Turkmenistan = 215,
        [Description("Turks and Caicos Islands(USD)")]
        Turks_and_Caicos_Islands = 216,
        [Description("Tuvalu(AUD)")]
        Tuvalu = 217,
        [Description("Uganda(UGX)")]
        Uganda = 218,
        [Description("Ukraine(UAH)")]
        Ukraine = 219,
        [Description("United Arab Emirates(AED)")]
        United_Arab_Emirates = 220,
        [Description("United Kingdom(GBP)")]
        United_Kingdom = 221,
        [Description("United States(USD)")]
        United_States = 222,
        [Description("Uruguay(UYU)")]
        Uruguay = 223,
        [Description("Uzbekistan(UZS)")]
        Uzbekistan = 224,
        [Description("Vanuatu(VUV)")]
        Vanuatu = 225,
        [Description("Venezuela(VEF)")]
        Venezuela = 226,
        [Description("Vietnam(VND)")]
        Vietnam = 227,

        [Description("Wallis and Futuna Islands(XPF)")]
        Wallis_and_Futuna_Islands = 229,
        [Description("Yemen(YER)")]
        Yemen = 230,
        [Description("Zambia(ZMW)")]
        Zambia = 231,
        [Description("Zimbabwe(ZWL)")]
        Zimbabwe = 232
    }

    public enum StudentTypes
    {
        [Description("Current High School Student")]
        Current_High_School_Student = 1,
        Current_College_Student = 2,
        Adult_Learner = 3,
        Parent_of_Student = 4,
        High_School_Counselor = 5,
        Admissions_Counselor = 6
    }

    //public enum CoursesOffered
    //{
    //    Accountancy = 1,
    //    Administration = 2,
    //    [Description("Adult Education")]
    //    Adult_Education = 3,
    //    [Description("Advanced Study")]
    //    Advanced_Study = 4,
    //    [Description("Applied Anthropology")]
    //    Applied_Anthropology = 5,
    //    [Description("Applied Politics")]
    //    Applied_Politics = 6,
    //    [Description("Applied Sciences")]
    //    Applied_Sciences = 7,
    //    Architecture = 8,
    //    [Description("Archival Studies")]
    //    Archival_Studies = 9,
    //    [Description("Christian Education")]
    //    Christian_Education = 10,
    //    [Description("City and Regional Planning")]
    //    City_and_Regional_Planning = 11,
    //    [Description("Clinical Medical Science")]
    //    Clinical_Medical_Science = 12,
    //    Communication = 13,
    //    [Description("Computer Science")]
    //    Computer_Science = 14,
    //    [Description("Criminal Justice")]
    //    Criminal_Justice = 15,
    //    [Description("Cross-Cultural and International Education")]
    //    Cross_Cultural_and_International_Education = 16,
    //    [Description("Dentistry")]
    //    Dentistry = 17,
    //    [Description("Digital Media")]
    //    Digital_Media = 18,
    //    [Description(" Dispute Resolution")]
    //    Dispute_Resolution = 19,
    //    Divinity = 20,
    //    Education = 21,
    //    Finance = 22,
    //    [Description("Educational Technology")]
    //    Educational_Technology = 23,
    //    [Description("Electronic Business Technologies")]
    //    Electronic_Business_Technologies = 24,
    //    Engineering = 25,
    //    Environment = 26,
    //    [Description("Environmental Design")]
    //    Environmental_Design = 27,
    //    [Description("Environmental Management")]
    //    Environmental_Management = 28,
    //    [Description("Environmental Science")]
    //    Environmental_Science = 29,
    //    [Description("Environmental Studies")]
    //    Environmental_Studies = 30,
    //    [Description("Fine Arts")]
    //    Fine_Arts = 31,
    //    [Description("Forensic Science and Law")]
    //    Forensic_Science_and_Law = 32,
    //    [Description("Forensic Sciences")]
    //    Forensic_Sciences = 33,
    //    [Description("Forestry")]
    //    Forestry = 34,
    //    [Description("Global Affairs")]
    //    Global_Affairs = 35,
    //    [Description("Health Administration")]
    //    Health_Administration = 36,
    //    [Description("Health Science")]
    //    Health_Science = 37,
    //    [Description("Historic Preservation")]
    //    Historic_Preservation = 38,
    //    [Description("History")]
    //    History = 39,
    //    [Description("Humanities")]
    //    Humanities = 40,
    //    [Description("Industrial and Labor Relations")]
    //    Industrial_and_Labor_Relations = 41,
    //    [Description("Industrial Design")]
    //    Industrial_Design = 42,
    //    [Description("Information")]
    //    Information = 43,
    //    [Description("Information Management")]
    //    Information_Management = 44,
    //    [Description("Information Systems")]
    //    Information_Systems = 45,
    //    [Description("Information Technology")]
    //    Information_Technology = 46,
    //    [Description("Interactive Media")]
    //    Interactive_Media = 47,
    //    [Description("International Business")]
    //    International_Business = 48,
    //    [Description("International Development")]
    //    International_Development = 49,
    //    [Description("International Economics and Finance")]
    //    International_Economics_and_Finance = 50,
    //    [Description("International Hotel Management")]
    //    International_Hotel_Management = 51,
    //    [Description("Jurisprudence")]
    //    Jurisprudence = 52,
    //    [Description("Landscape Architecture")]
    //    Landscape_Architecture = 53,
    //    [Description("Laws")]
    //    Laws = 54,
    //    [Description("Leadership")]
    //    Leadership = 55,
    //    [Description("Liberal Studies")]
    //    Liberal_Studies = 56,
    //    [Description("Library Science")]
    //    Library_Science = 57,
    //    [Description("Logistics, Trade, and Transportation")]
    //    Logistics_Trade_and_Transportation = 58,
    //    [Description("Management")]
    //    Management = 59,
    //    [Description("Management in the Network Economy")]
    //    Management_in_the_Network_Economy = 60,
    //    [Description("Management Sciences")]
    //    Management_Sciences = 70,
    //    [Description("Marketing and Communication")]
    //    Marketing_and_Communication = 71,
    //    [Description("Marketing Research")]
    //    Marketing_Research = 72,
    //    [Description("Mass Communication")]
    //    Mass_Communication = 74,
    //    [Description("Medical Education")]
    //    Medical_Education = 75,
    //    [Description("Medical Science")]
    //    Medical_Science = 76,
    //    [Description("Ministry")]
    //    Ministry = 77,
    //    [Description("Music")]
    //    Music = 78,
    //    [Description("Music Education")]
    //    Music_Education = 79,
    //    [Description("Natural Sciences")]
    //    Natural_Sciences = 80,
    //    [Description("Nonprofit Management")]
    //    Nonprofit_Management = 81,
    //    [Description("Nonprofit Organizations")]
    //    Nonprofit_Organizations = 82,
    //    [Description("Nurse Anesthesia")]
    //    Nurse_Anesthesia = 83,
    //    [Description("Nursing")]
    //    Nursing = 84,
    //    [Description("Occupational Therapy")]
    //    Occupational_Therapy = 85,
    //    [Description("Organizational Leadership")]
    //    Organizational_Leadership = 86,
    //    [Description("Pacific International Affairs")]
    //    Pacific_International_Affairs = 87,
    //    [Description("Pharmacy")]
    //    Pharmacy = 88,
    //    [Description("Philosophy")]
    //    Philosophy = 89,
    //    [Description("Physician Assistant Studies")]
    //    Physician_Assistant_Studies = 90,
    //    [Description("Professional Counseling")]
    //    Professional_Counseling = 91,
    //    [Description("Professional Studies")]
    //    Professional_Studies = 92,
    //    [Description("Professional Writing")]
    //    Professional_Writing = 93,
    //    [Description("Project Management")]
    //    Project_Management = 94,
    //    [Description("Public Administration")]
    //    Public_Administration = 95,
    //    [Description("Public Health")]
    //    Public_Health = 96,
    //    [Description("Public Management")]
    //    Public_Management = 97,
    //    [Description("Public Policy")]
    //    Public_Policy = 98,
    //    [Description("Public Service")]
    //    Public_Service = 99,
    //    [Description("Resource Management")]
    //    Resource_Management = 100,
    //    [Description("Sacred Music")]
    //    Sacred_Music = 101,
    //    [Description("Security Technologies")]
    //    Security_Technologies = 102,
    //    [Description("Social Work")]
    //    Social_Work = 103,
    //    [Description("Strategic Leadership")]
    //    Strategic_Leadership = 104,
    //    [Description("Taxation")]
    //    Taxation = 105,
    //    [Description("Teaching")]
    //    Teaching = 106,
    //    [Description("Theology")]
    //    Theology = 107,
    //    [Description("Urban Planning")]
    //    Urban_Planning = 108,
    //    [Description("Urban Studies")]
    //    Urban_Studies = 109

    //}

    public enum CoursesOffered
    {
        AeroSpace = 1,
        [Description("Airline Pilot Training")]
        Airline_Pilot_Training = 2,
        Architecture = 3,
        Anthropology = 4,
        [Description("Business Administration (General , Accounting, HR, Marketing, Energy, Finance, Global management, Health care etc)")]
        Business_Administration = 5,
        Cinema = 6,
        [Description("Communication & Journalism")]
        Communication_Journalism = 7,
        Drama = 8,
        Economics = 9,
        [Description("Engineering(Computer, Computer science, Information technology )")]
        Engineering_Computer = 10,
        [Description("Engineering (Electrical)")]
        Engineering_Electrical = 11,
        [Description("Engineering (Chemical)")]
        Engineering_Chemical = 12,
        [Description("Engineering (Supply chain & production)")]
        Engineering_Supply_chain_production = 13,
        [Description("Engineering (Mechanical)")]
        Engineering_Mechanical = 14,
        [Description("Engineering (Others)")]
        Engineering_Others = 15,
        [Description("Environmental Studies")]
        Environmental_Studies = 16,
        [Description("Fine Arts")]
        Fine_Arts = 17,
        Law = 18,
        [Description("Health, Medicine or Occupational Therapy")]
        Health_Medicine = 19,
        Music = 20,
        [Description("Sciences (Biology, Chemistry, Physics, Mathematics )")]
        Sciences = 21,
        Teaching = 22,
        Others = 23

    }

    public enum Programs
    {
        [Description("Associates/Diploma")]
        Associates_Diploma = 1,
        Bachelors = 2,
        Masters = 3,
        PHD = 4,

    }

    public enum PhoneTypes
    {
        Home = 1,
        Work = 2,
        Mobile = 3,
        Other = 4
    }

    public enum CountryList
    {
        Afghanistan = 1,
        Albania = 2,
        Algeria = 3,
        [Description("American Samoa")]
        American_Samoa = 4,
        Andorra = 5,
        Angola = 6,
        Anguilla = 7,
        [Description("Antigua and Barbuda")]
        Antigua_and_Barbuda = 8,
        Argentina = 9,
        Armenia = 10,
        Aruba = 11,
        Australia = 12,
        Austria = 13,
        Azerbaijan = 14,
        Bahamas = 15,
        Bahrain = 16,
        Bangladesh = 17,
        Barbados = 18,
        Belarus = 19,
        Belgium = 20,
        Belize = 21,
        Benin = 22,
        Bermuda = 23,
        Bhutan = 24,
        Bolivia = 25,
        [Description("Bosnia Herzegovina")]
        Bosnia_Herzegovina = 26,
        Botswana = 27,
        [Description("Bouvet Island")]
        Bouvet_Island = 28,
        Brazil = 29,
        Brunei = 30,
        Bulgaria = 31,
        [Description("Burkina Faso")]
        Burkina_Faso = 32,
        Burundi = 33,
        Cambodia = 34,
        Cameroon = 35,
        Canada = 36,
        [Description("Cape Verde")]
        Cape_Verde = 37,
        [Description("Cayman Islands")]
        Cayman_Islands = 38,
        [Description("Central African Republic")]
        Central_African_Republic = 39,
        Chad = 40,
        Chile = 41,
        China = 42,
        [Description("Christmas Island")]
        Christmas_Island = 43,
        [Description("Cocos Keeling Islands")]
        Cocos_Keeling_Islands = 44,
        Colombia = 45,
        Comoros = 46,
        [Description("Congo Democratic Republic of the Zaire")]
        Congo_Democratic_Republic_of_the_Zaire = 47,
        [Description("Congo Republic of")]
        Congo_Republic_of = 48,
        [Description("Cook Islands")]
        Cook_Islands = 49,
        [Description("Costa Rica")]
        Costa_Rica = 50,
        Croatia = 51,
        Cuba = 52,
        Cyprus = 53,
        [Description("Czech Republic")]
        Czech_Republic = 54,
        Denmark = 55,
        Djibouti = 56,
        Dominica = 57,
        [Description("Dominican Republic")]
        Dominican_Republic = 58,
        Ecuador = 59,
        Egypt = 60,
        [Description("El Salvador")]
        El_Salvador = 61,
        [Description("Equatorial Guinea")]
        Equatorial_Guinea = 62,
        Eritrea = 63,
        Estonia = 64,
        Ethiopia = 65,
        [Description("Falkland Islands")]
        Falkland_Islands = 66,
        [Description("Faroe Islands")]
        Faroe_Islands = 67,
        Fiji = 68,
        Finland = 69,
        France = 70,
        [Description("French Guiana")]
        French_Guiana = 71,
        Gabon = 72,
        Gambia = 73,
        Georgia = 74,
        Germany = 75,
        Ghana = 76,
        Gibraltar = 77,
        Greece = 78,
        Greenland = 79,
        Grenada = 80,
        [Description("Guadeloupe French")]
        Guadeloupe_French = 81,
        [Description("Guam USA")]
        Guam_USA = 82,
        Guatemala = 83,
        Guinea = 84,
        [Description("Guinea Bissau")]
        Guinea_Bissau = 85,
        Guyana = 86,
        Haiti = 87,
        [Description("Holy See")]
        Holy_See = 88,
        Honduras = 89,
        [Description("Hong Kong")]
        Hong_Kong = 90,
        Hungary = 91,
        Iceland = 92,
        India = 93,
        Indonesia = 94,
        Iran = 95,
        Iraq = 96,
        Ireland = 97,
        Israel = 98,
        Italy = 99,
        [Description("Ivory Coast Cote DIvoire")]
        Ivory_Coast_Cote_DIvoire = 100,
        Jamaica = 101,
        Japan = 102,
        Jordan = 103,
        Kazakhstan = 104,
        Kenya = 105,
        Kiribati = 106,
        Kuwait = 107,
        Kyrgyzstan = 108,
        Laos = 109,
        Latvia = 110,
        Lebanon = 111,
        Lesotho = 112,
        Liberia = 113,
        Libya = 114,
        Liechtenstein = 115,
        Lithuania = 116,
        Luxembourg = 117,
        Macau = 118,
        Macedonia = 119,
        Madagascar = 120,
        Malawi = 121,
        Malaysia = 122,
        Maldives = 123,
        Mali = 124,
        Malta = 125,
        [Description("Marshall Islands")]
        Marshall_Islands = 126,
        [Description("Martinique French")]
        Martinique_French = 127,
        Mauritania = 128,
        Mauritius = 129,
        Mayotte = 130,
        Mexico = 131,
        Micronesia = 132,
        Moldova = 133,
        Monaco = 134,
        Mongolia = 135,
        Montenegro = 136,
        Montserrat = 137,
        Morocco = 138,
        Mozambique = 139,
        Myanmar = 140,
        Namibia = 141,
        Nauru = 142,
        Nepal = 143,
        Netherlands = 144,
        [Description("Netherlands Antilles")]
        Netherlands_Antilles = 145,
        [Description("New Caledonia French")]
        New_Caledonia_French = 146,
        [Description("New Zealand")]
        New_Zealand = 147,
        Nicaragua = 148,
        Niger = 149,
        Nigeria = 150,
        Niue = 151,
        [Description("Norfolk Island")]
        Norfolk_Island = 152,
        [Description("North Korea")]
        North_Korea = 153,
        [Description("Northern Mariana Islands")]
        Northern_Mariana_Islands = 154,
        Norway = 155,
        Oman = 156,
        Pakistan = 157,
        Palau = 158,
        Panama = 159,
        [Description("Papua New Guinea")]
        Papua_New_Guinea = 160,
        Paraguay = 161,
        Peru = 162,
        Philippines = 163,
        [Description("Pitcairn Island")]
        Pitcairn_Island = 164,
        Poland = 165,
        [Description("Polynesia French")]
        Polynesia_French = 166,
        Portugal = 167,
        [Description("Puerto Rico")]
        Puerto_Rico = 168,
        Qatar = 169,
        Reunion = 170,
        Romania = 171,
        Russia = 172,
        Rwanda = 173,
        [Description("Saint Helena")]
        Saint_Helena = 174,
        [Description("Saint Kitts and Nevis")]
        Saint_Kitts_and_Nevis = 175,
        [Description("Saint Lucia")]
        Saint_Lucia = 176,
        [Description("Saint Pierre and Miquelon")]
        Saint_Pierre_and_Miquelon = 177,
        [Description("Saint Vincent and Grenadines")]
        Saint_Vincent_and_Grenadines = 178,
        Samoa = 179,
        [Description("San Marino")]
        San_Marino = 180,
        [Description("Sao Tome and Principe")]
        Sao_Tome_and_Principe = 181,
        [Description("Saudi Arabia")]
        Saudi_Arabia = 182,
        Senegal = 183,
        Serbia = 184,
        Seychelles = 185,
        [Description("Sierra Leone")]
        Sierra_Leone = 186,
        Singapore = 187,
        Slovakia = 188,
        Slovenia = 189,
        [Description("Solomon Islands")]
        Solomon_Islands = 190,
        Somalia = 191,
        [Description("South Africa")]
        South_Africa = 192,
        [Description("South Georgia and South Sandwich Islands")]
        South_Georgia_and_South_Sandwich_Islands = 193,
        [Description("South Korea")]
        South_Korea = 194,
        Spain = 195,
        [Description("Sri Lanka")]
        Sri_Lanka = 196,
        Sudan = 197,
        Suriname = 198,
        [Description("Svalbard and Jan Mayen Islands")]
        Svalbard_and_Jan_Mayen_Islands = 199,
        Swaziland = 200,
        Sweden = 201,
        Switzerland = 202,
        Syria = 203,
        Taiwan = 204,
        Tajikistan = 205,
        Tanzania = 206,
        Thailand = 207,
        [Description("Timor Leste East Timor")]
        Timor_Leste_East_Timor = 208,
        Togo = 209,
        Tokelau = 210,
        Tonga = 211,
        [Description("Trinidad and Tobago")]
        Trinidad_and_Tobago = 212,
        Tunisia = 213,
        Turkey = 214,
        Turkmenistan = 215,
        [Description("Turks and Caicos Islands")]
        Turks_and_Caicos_Islands = 216,
        Tuvalu = 217,
        Uganda = 218,
        Ukraine = 219,
        [Description("United Arab Emirates")]
        United_Arab_Emirates = 220,
        [Description("United Kingdom")]
        United_Kingdom = 221,
        [Description("United States")]
        United_States = 222,
        Uruguay = 223,
        Uzbekistan = 224,
        Vanuatu = 225,
        Venezuela = 226,
        Vietnam = 227,
        [Description("Virgin Islands")]
        Virgin_Islands = 228,
        [Description("Wallis and Futuna Islands")]
        Wallis_and_Futuna_Islands = 229,
        Yemen = 230,
        Zambia = 231,
        Zimbabwe = 232,
    }

    public enum CurrentlyIn
    {
        //twelveth_grade = 1,
        //bachelors = 2,
        //Masters = 3,
        //Phd = 4,
        //Working = 5,
        //Other = 6
        [Description("High School (8th - 12th)")]
        twelveth_grade = 1,
        [Description("Pursuing a Bachelor's Degree (B.S, B.A, B.com, B.E etc)")]
        bachelors = 2,
        [Description("Pursuing a Master's Degree (M.S, M.A, M.com, M.E etc)")]
        Masters = 3,
        [Description("Pursuing a Phd")]
        Phd = 4,
        Working = 5,
        Other = 6

    }

    public enum ProgramLookingFor
    {
        [Description("Bachelor's program")]
        Bachelors = 1,
        [Description("Master's program")]
        Masters = 2,
        [Description("Phd Program")]
        PHD = 3,
        Other = 4
    }

    public enum PreferedStudyIn
    {
        Afghanistan = 1,
        Albania = 2,
        Algeria = 3,
        [Description("American Samoa")]
        American_Samoa = 4,
        Andorra = 5,
        Angola = 6,
        Anguilla = 7,
        [Description("Antigua and Barbuda")]
        Antigua_and_Barbuda = 8,
        Argentina = 9,
        Armenia = 10,
        Aruba = 11,
        Australia = 12,
        Austria = 13,
        Azerbaijan = 14,
        Bahamas = 15,
        Bahrain = 16,
        Bangladesh = 17,
        Barbados = 18,
        Belarus = 19,
        Belgium = 20,
        Belize = 21,
        Benin = 22,
        Bermuda = 23,
        Bhutan = 24,
        Bolivia = 25,
        [Description("Bosnia Herzegovina")]
        Bosnia_Herzegovina = 26,
        Botswana = 27,
        [Description("Bouvet Island")]
        Bouvet_Island = 28,
        Brazil = 29,
        Brunei = 30,
        Bulgaria = 31,
        [Description("Burkina Faso")]
        Burkina_Faso = 32,
        Burundi = 33,
        Cambodia = 34,
        Cameroon = 35,
        Canada = 36,
        [Description("Cape Verde")]
        Cape_Verde = 37,
        [Description("Cayman Islands")]
        Cayman_Islands = 38,
        [Description("Central African Republic")]
        Central_African_Republic = 39,
        Chad = 40,
        Chile = 41,
        China = 42,
        [Description("Christmas Island")]
        Christmas_Island = 43,
        [Description("Cocos Keeling Islands")]
        Cocos_Keeling_Islands = 44,
        Colombia = 45,
        Comoros = 46,
        [Description("Congo Democratic Republic of the Zaire")]
        Congo_Democratic_Republic_of_the_Zaire = 47,
        [Description("Congo Republic of")]
        Congo_Republic_of = 48,
        [Description("Cook Islands")]
        Cook_Islands = 49,
        [Description("Costa Rica")]
        Costa_Rica = 50,
        Croatia = 51,
        Cuba = 52,
        Cyprus = 53,
        [Description("Czech Republic")]
        Czech_Republic = 54,
        Denmark = 55,
        Djibouti = 56,
        Dominica = 57,
        [Description("Dominican Republic")]
        Dominican_Republic = 58,
        Ecuador = 59,
        Egypt = 60,
        [Description("El Salvador")]
        El_Salvador = 61,
        [Description("Equatorial Guinea")]
        Equatorial_Guinea = 62,
        Eritrea = 63,
        Estonia = 64,
        Ethiopia = 65,
        [Description("Falkland Islands")]
        Falkland_Islands = 66,
        [Description("Faroe Islands")]
        Faroe_Islands = 67,
        Fiji = 68,
        Finland = 69,
        France = 70,
        [Description("French Guiana")]
        French_Guiana = 71,
        Gabon = 72,
        Gambia = 73,
        Georgia = 74,
        Germany = 75,
        Ghana = 76,
        Gibraltar = 77,
        Greece = 78,
        Greenland = 79,
        Grenada = 80,
        [Description("Guadeloupe French")]
        Guadeloupe_French = 81,
        [Description("Guam USA")]
        Guam_USA = 82,
        Guatemala = 83,
        Guinea = 84,
        [Description("Guinea Bissau")]
        Guinea_Bissau = 85,
        Guyana = 86,
        Haiti = 87,
        [Description("Holy See")]
        Holy_See = 88,
        Honduras = 89,
        [Description("Hong Kong")]
        Hong_Kong = 90,
        Hungary = 91,
        Iceland = 92,
        India = 93,
        Indonesia = 94,
        Iran = 95,
        Iraq = 96,
        Ireland = 97,
        Israel = 98,
        Italy = 99,
        [Description("Ivory Coast Cote DIvoire")]
        Ivory_Coast_Cote_DIvoire = 100,
        Jamaica = 101,
        Japan = 102,
        Jordan = 103,
        Kazakhstan = 104,
        Kenya = 105,
        Kiribati = 106,
        Kuwait = 107,
        Kyrgyzstan = 108,
        Laos = 109,
        Latvia = 110,
        Lebanon = 111,
        Lesotho = 112,
        Liberia = 113,
        Libya = 114,
        Liechtenstein = 115,
        Lithuania = 116,
        Luxembourg = 117,
        Macau = 118,
        Macedonia = 119,
        Madagascar = 120,
        Malawi = 121,
        Malaysia = 122,
        Maldives = 123,
        Mali = 124,
        Malta = 125,
        [Description("Marshall Islands")]
        Marshall_Islands = 126,
        [Description("Martinique French")]
        Martinique_French = 127,
        Mauritania = 128,
        Mauritius = 129,
        Mayotte = 130,
        Mexico = 131,
        Micronesia = 132,
        Moldova = 133,
        Monaco = 134,
        Mongolia = 135,
        Montenegro = 136,
        Montserrat = 137,
        Morocco = 138,
        Mozambique = 139,
        Myanmar = 140,
        Namibia = 141,
        Nauru = 142,
        Nepal = 143,
        Netherlands = 144,
        [Description("Netherlands Antilles")]
        Netherlands_Antilles = 145,
        [Description("New Caledonia French")]
        New_Caledonia_French = 146,
        [Description("New Zealand")]
        New_Zealand = 147,
        Nicaragua = 148,
        Niger = 149,
        Nigeria = 150,
        Niue = 151,
        [Description("Norfolk Island")]
        Norfolk_Island = 152,
        [Description("North Korea")]
        North_Korea = 153,
        [Description("Northern Mariana Islands")]
        Northern_Mariana_Islands = 154,
        Norway = 155,
        Oman = 156,
        Pakistan = 157,
        Palau = 158,
        Panama = 159,
        [Description("Papua New Guinea")]
        Papua_New_Guinea = 160,
        Paraguay = 161,
        Peru = 162,
        Philippines = 163,
        [Description("Pitcairn Island")]
        Pitcairn_Island = 164,
        Poland = 165,
        [Description("Polynesia French")]
        Polynesia_French = 166,
        Portugal = 167,
        [Description("Puerto Rico")]
        Puerto_Rico = 168,
        Qatar = 169,
        Reunion = 170,
        Romania = 171,
        Russia = 172,
        Rwanda = 173,
        [Description("Saint Helena")]
        Saint_Helena = 174,
        [Description("Saint Kitts and Nevis")]
        Saint_Kitts_and_Nevis = 175,
        [Description("Saint Lucia")]
        Saint_Lucia = 176,
        [Description("Saint Pierre and Miquelon")]
        Saint_Pierre_and_Miquelon = 177,
        [Description("Saint Vincent and Grenadines")]
        Saint_Vincent_and_Grenadines = 178,
        Samoa = 179,
        [Description("San Marino")]
        San_Marino = 180,
        [Description("Sao Tome and Principe")]
        Sao_Tome_and_Principe = 181,
        [Description("Saudi Arabia")]
        Saudi_Arabia = 182,
        Senegal = 183,
        Serbia = 184,
        Seychelles = 185,
        [Description("Sierra Leone")]
        Sierra_Leone = 186,
        Singapore = 187,
        Slovakia = 188,
        Slovenia = 189,
        [Description("Solomon Islands")]
        Solomon_Islands = 190,
        Somalia = 191,
        [Description("South Africa")]
        South_Africa = 192,
        [Description("South Georgia and South Sandwich Islands")]
        South_Georgia_and_South_Sandwich_Islands = 193,
        [Description("South Korea")]
        South_Korea = 194,
        Spain = 195,
        [Description("Sri Lanka")]
        Sri_Lanka = 196,
        Sudan = 197,
        Suriname = 198,
        [Description("Svalbard and Jan Mayen Islands")]
        Svalbard_and_Jan_Mayen_Islands = 199,
        Swaziland = 200,
        Sweden = 201,
        Switzerland = 202,
        Syria = 203,
        Taiwan = 204,
        Tajikistan = 205,
        Tanzania = 206,
        Thailand = 207,
        [Description("Timor Leste East Timor")]
        Timor_Leste_East_Timor = 208,
        Togo = 209,
        Tokelau = 210,
        Tonga = 211,
        [Description("Trinidad and Tobago")]
        Trinidad_and_Tobago = 212,
        Tunisia = 213,
        Turkey = 214,
        Turkmenistan = 215,
        [Description("Turks and Caicos Islands")]
        Turks_and_Caicos_Islands = 216,
        Tuvalu = 217,
        Uganda = 218,
        Ukraine = 219,
        [Description("United Arab Emirates")]
        United_Arab_Emirates = 220,
        [Description("United Kingdom")]
        United_Kingdom = 221,
        [Description("United States")]
        United_States = 222,
        Uruguay = 223,
        Uzbekistan = 224,
        Vanuatu = 225,
        Venezuela = 226,
        Vietnam = 227,
        [Description("Virgin Islands")]
        Virgin_Islands = 228,
        [Description("Wallis and Futuna Islands")]
        Wallis_and_Futuna_Islands = 229,
        Yemen = 230,
        Zambia = 231,
        Zimbabwe = 232,
        Other = 233
    }

    public enum expectedStart
    {
        [Description("Fall 2013")]
        Fall_2013 = 1,
        [Description("Spring 2014")]
        Spring_2014 = 2,
        [Description("Summer 2014")]
        Summer_2014 = 3,
        [Description("Fall 2014")]
        Fall_2014 = 4,
        [Description("Spring 2015")]
        Spring_2015 = 5,
        [Description("Summer 2015")]
        Summer_2015 = 6,
        [Description("Fall 2015")]
        Fall_2016 = 7,
        Other = 8
    }

    public enum GraduateStatus
    {
        Yes = 1,
        No = 2,
        OnGoing = 3
    }

    public enum LevelCompleted
    {
        [Description("Upto 10th grade")]
        Upto_10th_grade = 1,
        [Description("Upto 12th grade")]
        Upto_12th_grade = 2,
        Bachelors = 3,
        Master = 4,
        PHD = 5,
        other = 6
    }

    public enum StudentInterestApproved
    {
        Applied = 1,
        Approved = 2,
        Decline = 3
    }

    public enum Degreepursued
    {
        [Description("High Schol (8-12th)")]
        High_School = 1,
        [Description("Bachelor's Degree (B.S, B.A, B.com, B.E etc)")]
        Bachelor = 2,
        [Description("Master's Degree (M.S, M.A, M.com, M.E etc)")]
        Master = 3,
        [Description("Pursuing a Phd")]
        Phd = 5,
        other = 6
    }

    public enum InternationalTestRecord
    {
        ACT = 2,
        SAT = 3,
        AP = 4,
        GRE = 5,
        GMAT = 6,
        IB = 7,
        IELTS = 8,
        LSAT = 9,
        MCAT = 10,
        PSAT = 11,
        SAT_II = 12,
        [Description("TOEFL Internet based")]
        TOEFL_Internet_based = 13,
        [Description("TOEFL Paper based")]
        TOEFL_Paper_based = 14
    }

    public enum Sector
    {
        Public = 1,
        Private = 2,
        HomeSchool = 3,
    }

    public enum CountryCode
    {
        [Description("Afghanistan[+93]")]
        Afghanistan = 1,
        [Description("Albania[+355]")]
        Albania = 2,
        [Description("Armenia[+374]")]
        Armenia = 3,
        [Description("Antilles Netherlands[+599]")]
        Netherlands_Antilles = 4,
        [Description("Angola[+244]")]
        Angola = 5,
        [Description("Antarctica[+672]")]
        Antarctica = 6,
        [Description("Argentina[+54]")]
        Argentina = 7,
        [Description("American Samoa[+1684]")]
        American_Samoa = 8,
        [Description("Austria[+43]")]
        Austria = 9,
        [Description("Australia[+61]")]
        Australia = 10,
        [Description("Aruba[+297]")]
        Aruba = 11,
        [Description("Azerbaijan[+994]")]
        Azerbaijan = 12,
        [Description("Bosnia Herzegovina[+387]")]
        Bosnia_Herzegovina = 13,
        [Description("Barbados[+246]")]
        Barbados = 14,
        [Description("Bangladesh[+880]")]
        Bangladesh = 15,
        [Description("Belgium[+32]")]
        Belgium = 16,
        [Description("Burkina Faso[+226]")]
        Burkina_Faso = 17,
        [Description("Bulgaria[+359]")]
        Bulgaria = 18,
        [Description("Bahrain[+973]")]
        Bahrain = 19,
        [Description("Burundi[+257]")]
        Burundi = 20,
        [Description("Benin[+229]")]
        Benin = 21,
        [Description("Guadeloupe French[+590]")]
        Guadeloupe_French = 22,
        [Description("Bermuda[+1441]")]
        Bermuda = 23,
        [Description("Brunei Darussalam[+673]")]
        Brunei_Darussalam = 24,
        [Description("Bolivia[+591]")]
        Bolivia = 25,
        [Description("Brazil[+55]")]
        Brazil = 26,
        [Description("Bahamas[+1242]")]
        Bahamas = 27,
        [Description("Bhutan[+975]")]
        Bhutan = 28,
        [Description("Botswana[+267]")]
        Botswana = 29,
        [Description("Belarus[+375]")]
        Belarus = 30,
        [Description("Belize[+501]")]
        Belize = 31,
        [Description("Canada[+1]")]
        Canada = 32,
        [Description("Cocos Islands[+61]")]
        Cocos_Islands = 33,
        [Description("Congo[+243]")]
        Congo = 33,
        [Description("Central African Republic[+236]")]
        Central_African_Republic = 34,
        [Description("Congo Dem Republic[+242]")]
        Congo_Dem_Republic = 35,
        [Description("Switzerland[+41]")]
        Switzerland = 36,
        [Description("Ivory Coast[+225]")]
        Ivory_Coast = 37,
        [Description("Cook Islands[+682]")]
        Cook_Islands = 38,
        [Description("Chile[+56]")]
        Chile = 39,
        [Description("Cameroon[+237]")]
        Cameroon = 40,
        [Description("China[+86]")]
        China = 41,
        [Description("Colombia[+57]")]
        Colombia = 42,
        [Description("Costa Rica[+506]")]
        Costa_Rica = 43,
        [Description("Cuba[+53]")]
        Cuba = 44,
        [Description("Cape Verde[+238]")]
        Cape_Verde = 45,
        [Description("Christmas Island[+61]")]
        Christmas_Island = 46,
        [Description("Cyprus[+357]")]
        Cyprus = 47,
        [Description("Czech Rep[+420]")]
        Czech_Rep = 48,
        [Description("Germany[+49]")]
        Germany = 49,
        [Description("Djibouti[+253]")]
        Djibouti = 50,
        [Description("Denmark[+45]")]
        Denmark = 51,
        [Description("Dominica[+1767]")]
        Dominica = 52,
        [Description("Dominican Republic[+809]")]
        Dominican_Republic = 53,
        [Description("Algeria[+213]")]
        Algeria = 54,
        [Description("Ecuador[+593]")]
        Ecuador = 55,
        [Description("Estonia[+372]")]
        Estonia = 56,
        [Description("Egypt[+20]")]
        Egypt = 57,
        [Description("Eritrea[+291]")]
        Eritrea = 58,
        [Description("Spain[+34]")]
        Spain = 59,
        [Description("Ethiopia[+251]")]
        Ethiopia = 60,
        [Description("Finland[+358]")]
        Finland = 61,
        [Description("Fiji[+679]")]
        Fiji = 62,
        [Description("Falkland Islands[+500]")]
        Falkland_Islands = 63,
        [Description("Micronesia[+691]")]
        Micronesia = 64,
        [Description("Faroe Islands[+298]")]
        Faroe_Islands = 65,
        [Description("France[+33]")]
        France = 66,
        [Description("Gabon[+241]")]
        Gabon = 67,
        [Description("UK[+44]")]
        UK = 68,
        [Description("Grenada[+1473]")]
        Grenada = 69,
        [Description("Georgia[+995]")]
        Georgia = 70,
        [Description("Ghana[+233]")]
        Ghana = 71,
        [Description("Gibraltar[+350]")]
        Gibraltar = 72,
        [Description("Greenland[+299]")]
        Greenland = 73,
        [Description("Gambia[+220]")]
        Gambia = 74,
        [Description("Guinea[+224]")]
        Guinea = 75,
        [Description("Equatorial Guinea[+240]")]
        Equatorial_Guinea = 76,
        [Description("Greece[+30]")]
        Greece = 77,
        [Description("Guatemala[+502]")]
        Guatemala = 78,
        [Description("Guam USA[+1671]")]
        Guam_USA = 79,
        [Description("Guinea Bissau[+245]")]
        Guinea_Bissau = 80,
        [Description("Guyana[+592]")]
        Guyana = 81,
        [Description("Hong Kong[+852]")]
        Hong_Kong = 82,
        [Description("Honduras[+504]")]
        Honduras = 83,
        [Description("Croatia[+385]")]
        Croatia = 84,
        [Description("Haiti[+509]")]
        Haiti = 85,
        [Description("Hungary[+509]")]
        Hungary = 86,
        [Description("Indonesia[+62]")]
        Indonesia = 87,
        [Description("Ireland[+353]")]
        Ireland = 88,
        [Description("Israel[+972]")]
        Israel = 89,
        [Description("sle of Man[+44]")]
        sle_of_Man = 90,
        [Description("India[+91]")]
        India = 91,
        [Description("Iraq[+964]")]
        Iraq = 92,
        [Description("Iran[+98]")]
        Iran = 93,
        [Description("Iceland[+354]")]
        Iceland = 94,
        [Description("Italy[+39]")]
        Italy = 95,
        [Description("Jamaica[+1876]")]
        Jamaica = 96,
        [Description("Jordan[+962]")]
        Jordan = 97,
        [Description("Japan[+81]")]
        Japan = 98,
        [Description("Kenya[+254]")]
        Kenya = 99,
        [Description("Kyrgyzstan[+996]")]
        Kyrgyzstan = 100,
        [Description("Cambodia[+855]")]
        Cambodia = 101,
        [Description("Kiribati[+686]")]
        Kiribati = 102,
        [Description("Comoros[+269]")]
        Comoros = 103,
        [Description("Saint Kitts and Nevis Country[+1869]")]
        Saint_Kitts_and_Nevis_Country = 104,
        [Description("North Korea[+850]")]
        North_Korea = 105,
        [Description("South Korea[+82]")]
        South_Korea = 106,
        [Description("Kuwait[+965]")]
        Kuwait = 107,
        [Description("Cayman Islands[+1345]")]
        Cayman_Islands = 108,
        [Description("Kazakhstan[+7]")]
        Kazakhstan = 109,
        [Description("Laos[+856]")]
        Laos = 110,
        [Description("Lebanon[+961]")]
        Lebanon = 111,
        [Description("Saint Lucia[+1758]")]
        Saint_Lucia = 112,
        [Description("Lechtenstein[+423]")]
        Lechtenstein = 113,
        [Description("Sri Lanka[+94]")]
        Sri_Lanka = 114,
        [Description("Liberia[+231]")]
        Liberia = 115,
        [Description("Lesotho[+266]")]
        Lesotho = 116,
        [Description("Lithuania[+370]")]
        Lithuania = 117,
        [Description("Luxembourg[+352]")]
        Luxembourg = 118,
        [Description("Latvia[+371]")]
        Latvia = 119,
        [Description("Libya[+218]")]
        Libya = 120,
        [Description("Morocco[+212]")]
        Morocco = 121,
        [Description("Monaco[+377]")]
        Monaco = 122,
        [Description("Moldova[+373]")]
        Moldova = 123,
        [Description("Montenegro[+382]")]
        Montenegro = 124,
        [Description("Saint Martin[+1599]")]
        Saint_Martin = 125,
        [Description("Madaqascar[+261]")]
        Madaqascar = 126,
        [Description("Marshall Islnads[+692]")]
        Marshall_Islnads = 127,
        [Description("Macedonia[+389]")]
        Macedonia = 128,
        [Description("Mali[+223]")]
        Mali = 129,
        [Description("Burma[+95]")]
        Burma = 130,
        [Description("Monqolia[+976]")]
        Monqolia = 131,
        [Description("Macau[+853]")]
        Macau = 132,
        [Description("MP[+1670]")]
        Northern_Mariana_Islands = 133,
        [Description("Mauritania[+222]")]
        Mauritania = 134,
        [Description("Montserrat[+1664]")]
        Montserrat = 135,
        [Description("Malta[+356]")]
        Malta = 136,
        [Description("Mauritius[+230]")]
        Mauritius = 137,
        [Description("Maldives[+960]")]
        Maldives = 138,
        [Description("Malawi[+265]")]
        Malawi = 139,
        [Description("Mexico[+52]")]
        Mexico = 140,
        [Description("Malaysia[+60]")]
        Malaysia = 141,
        [Description("Mozambique[+258]")]
        Mozambique = 142,
        [Description("Namibia[+264]")]
        Namibia = 143,
        [Description("New Caledonia[+687]")]
        New_Caledonia = 144,
        [Description("Niqer[+227]")]
        Niqer = 145,
        [Description("Niqeria[+234]")]
        Niqeria = 146,
        [Description("Nicaraqua[+505]")]
        Nicaraqua = 147,
        [Description("Netherlands[+31]")]
        Netherlands = 148,
        [Description("Norway[+47]")]
        Norway = 149,
        [Description("Nepal[+977]")]
        Nepal = 150,
        [Description("Nauru[+674]")]
        Nauru = 151,
        [Description("Niue[+683]")]
        Niue = 152,
        [Description("New Zealand[+64]")]
        New_Zealand = 153,
        [Description("Oman[+968]")]
        Oman = 154,
        [Description("Panama[+507]")]
        Panama = 155,
        [Description("Peru[+51]")]
        Peru = 156,
        [Description("French Polynesia[+689]")]
        French_Polynesia = 157,
        [Description("Papua New Guinea[+675]")]
        Papua_New_Guinea = 158,
        [Description("Philippines[+63]")]
        Philippines = 159,
        [Description("Pakistan[+92]")]
        Pakistan = 160,
        [Description("Poland[+48]")]
        Poland = 161,
        [Description("Saint Pierre and Miquelon[+508]")]
        Saint_Pierre_and_Miquelon = 162,
        [Description("Pitcairn Islands[+870]")]
        Pitcairn_Islands = 163,
        [Description("Puerto Rico[+1]")]
        Puerto_Rico = 164,
        [Description("Portuqal[+351]")]
        Portuqal = 165,
        [Description("Palau[+680]")]
        Palau = 166,
        [Description("Paraquay[+680]")]
        Paraquay = 167,
        [Description("Qatar[+974]")]
        Qatar = 168,
        [Description("Romania[+40]")]
        Romania = 169,
        [Description("Serbia Country[+381]")]
        Serbia_Country = 170,
        [Description("Russia[+7]")]
        Russia = 171,
        [Description("Rwanda[+250]")]
        Rwanda = 172,
        [Description("Saudi Arabia[+966]")]
        Saudi_Arabia = 173,
        [Description("Solomon Isalands[+677]")]
        Solomon_Isalands = 174,
        [Description("Seychelles[+248]")]
        Seychelles = 175,
        [Description("Sudan[+249]")]
        Sudan = 176,
        [Description("Sweden[+46]")]
        Sweden = 177,
        [Description("Singapore[+65]")]
        Singapore = 178,
        [Description("Saint Helena[+290]")]
        Saint_Helena = 179,
        [Description("Slovenia[+386]")]
        Slovenia = 180,
        [Description("Slovakia[+421]")]
        Slovakia = 181,
        [Description("Sierra Leone[+232]")]
        Sierra_Leone = 182,
        [Description("San Marino[+378]")]
        San_Marino = 183,
        [Description("Seneqal[+321]")]
        Seneqal = 184,
        [Description("Somalia[+252]")]
        Somalia = 185,
        [Description("Suriname[+597]")]
        Suriname = 186,
        [Description("Sao Tome and Principe[+239]")]
        Sao_Tome_and_Principe = 187,
        [Description("El Salvador[+503]")]
        El_Salvador = 188,
        [Description("Syria[+963]")]
        Syria = 189,
        [Description("Swaziland[+268]")]
        Swaziland = 190,
        [Description("Turks and Caicos Islands[+1649]")]
        Turks_and_Caicos_Islands = 191,
        [Description("Chad[+235]")]
        Chad = 192,
        [Description("Toqo[+228]")]
        Toqo = 193,
        [Description("Thailand[+66]")]
        Thailand = 194,
        [Description("Tajikistan[+992]")]
        Tajikistan = 195,
        [Description("Tokelau[+690]")]
        Tokelau = 196,
        [Description("Timor Leste[+670]")]
        Timor_Leste = 197,
        [Description("Turkmenistan[+993]")]
        Turkmenistan = 198,
        [Description("Tunisia[+216]")]
        Tunisia = 199,
        [Description("Tonga[+676]")]
        Tonga = 200,
        [Description("Turkey[+690]")]
        Turkey = 201,
        [Description("Trinidad and Tobago[+1868]")]
        Trinidad_and_Tobago = 202,
        [Description("Tuvalu[+688]")]
        Tuvalu = 203,
        [Description("Taiwan[+886]")]
        Taiwan = 204,
        [Description("Tanzania[+255]")]
        Tanzania = 205,
        [Description("Ukraine[+380]")]
        Ukraine = 206,
        [Description("Uganda[+256]")]
        Uganda = 207,
        [Description("United States[+1]")]
        United_States = 208,
        [Description("Uruguay[+598]")]
        Uruguay = 209,
        [Description("Uzbekistan[+998]")]
        Uzbekistan = 210,
        [Description("Vatican City[39]")]
        Vatican_City = 211,
        [Description("Saint Vincent and the Grenadines[+1784]")]
        Saint_Vincent_and_the_Grenadines = 212,
        [Description("Venezuela[+58]")]
        Venezuela = 213,
        [Description("British Virgin Islands[+1284]")]
        British_Virgin_Islands = 214,
        [Description("US Virgin Islands[+1284]")]
        US_Virgin_Islands = 215,
        [Description("Vietnam[+84]")]
        Vietnam = 216,
        [Description("Vanuatu[+678]")]
        Vanuatu = 217,
        [Description("Wallis and Futuna[+681]")]
        Wallis_and_Futuna = 218,
        [Description("Samoa[+685]")]
        Samoa = 219,
        [Description("Yemen[+967]")]
        Yemen = 220,
        [Description("Mayotte[+262]")]
        Mayotte = 221,
        [Description("South Africa[+27]")]
        South_Africa = 222,
        [Description("Zambia[+260]")]
        Zambia = 223,
        [Description("Zimbabwe[+263]")]
        Zimbabwe = 224,
    }

    public enum QuestionSection
    {
        [Description("Basic Questions")]
        Basic_Questions = 1,
        [Description("Application process")]
        Application_process = 2,
        [Description("Feedback of Housing")]
        Feedback_of_Housing = 3,
        [Description("Feedback on job Situation")]
        Feedback_on_job_Situation = 4,
        [Description("Campus Safety")]
        Campus_Safety = 5,
        [Description("Scholarships")]
        Scholarships = 6,
        [Description("Food Situation")]
        Food_Situation = 7,
        [Description("Test scores")]
        Test_scores = 8,
        [Description("Miscellaneous Questions")]
        Miscellaneous_Questions = 9,
        [Description("Suggestions")]
        suggestions = 10,
        [Description("Email address")]
        email_address = 11
    }

    public enum Questions
    {
        [Description("Please enter your Name (will not be displayed or shared):")]
        Please_enter_your_Name = 1,
        [Description("Which country are you from:")]
        Which_country_are_you_from = 2,
        [Description("Which city:")]
        Which_city = 3,
        [Description("University you attended:")]
        University_you_attended = 4,
        [Description("Year of gradution:")]
        Year_of_gradution = 5,
        [Description("Course:")]
        Course = 6,
        [Description("Degree:")]
        Degree = 7,
        [Description("Did you apply on your own or use a agent:")]
        Did_you_apply_on_your_own_or_use_a_agent = 8,
        [Description("If you used a local agent, What fees did the agent charge:")]
        If_you_used_a_local_agent_What_fees_did_the_agent_charge = 9,
        [Description("Name of agent:")]
        Name_of_agent = 10,
        [Description("where did you first live when you landed at your university:")]
        where_did_you_first_live_when_you_landed_at_your_university = 11,

        [Description("Does the unviersity provide assistance in looking for housing:")]
        Does_the_unviersity_provide_assistance_in_looking_for_housing = 12,

        [Description("Did you have your own room or you shared it with others:")]
        Did_you_have_your_own_room_or_you_shared_it_with_others = 13,
        [Description("Monthly rent:")]
        Monthly_rent = 14,
        [Description("Where did you live:")]
        Where_did_you_live = 15,
        [Description("Address:")]
        Address = 16,
        [Description("How would you rate your building:")]
        rate_your_building = 17,
        [Description("Any feedback on the landlord:")]
        Any_feedback_on_the_landlord = 18,
        [Description("Did your landlord return your deposit:")]
        return_your_deposit = 19,
        [Description("Any Suggestions about housing for future students:")]
        Any_Suggestions_about_housing_for_future_students = 20,
        [Description("Did you look for on-campus jobs:")]
        Did_you_look_for_oncampus_jobs = 21,
        [Description("Were you able to find an on-campus job:")]
        Were_you_able_to_find_an_oncampus_job = 22,
        [Description("How soon did you get one:")]
        How_soon_did_you_get_one = 23,
        [Description("Which department:")]
        Which_department = 24,
        [Description("Hourly pay:")]
        Hourly_pay = 25,
        [Description("Any Suggestions for future students:")]
        Any_Suggestions_for_future_students = 26,
        [Description("Did you feel safe on-campus:")]
        Did_you_feel_safe_oncampus = 27,
        [Description("Did you feel safe just outside the university area:")]
        Did_you_feel_safe_just_outside_the_university_area = 28,
        [Description("Are there a lot of thefts/violent incidents happening (in/around) the campus: ")]
        Are_theralot_of_theftsviolent_incidents_happening_inaround_the_campus = 29,
        [Description("How visible are the campus police on campus:")]
        How_visible_are_the_campus_police_on_campus = 30,
        [Description("Were any of your things stolen:")]
        Were_any_of_your_things_stolen = 31,
        [Description("what was it:")]
        what_was_it = 32,
        [Description("Were you able to retrieve it back:")]
        Were_you_able_to_retrieve_it_back = 33,
        [Description("Did you get a scholarship before you joined:")]
        Did_you_get_a_scholarship_before_you_joined = 34,
        [Description("Did you get one afterwards:")]
        Did_you_get_one_afterwards = 35,
        [Description("How easy is it to get one after joining the university:")]
        How_easy_is_it_to_get_one_after_joining_the_university = 36,
        [Description("Suggestions for future students:")]
        Suggestions_for_future_students = 37,
        [Description("Are there sufficient eating options on/around the campus:")]
        Are_there_sufficient_eating_options_onaround_the_campus = 38,
        [Description("Are there sufficient markets in/around the campus:")]
        Are_there_sufficient_markets_inaround_the_campus = 39,
        [Description("Please share with us your test scores:")]
        Please_share_with_us_your_test_scores = 40,
        [Description("What other universities did you apply to:")]
        What_other_universities_did_you_apply_to = 41,
        [Description("Which all universities did you get into:")]
        Which_all_universities_did_you_get_into = 42,
        [Description("How helpful is the career center in helping students find jobs or internships?")]
        How_helpful_is_the_career_center_in_helping_students_find_jobs_or_internships = 43,
        [Description("Do you want to give any suggestions to the university:")]
        Do_you_want_to_give_any_suggestions_to_the_university = 44,
        [Description("Any suggestions for future students:")]
        Any_suggestions_for_future_students = 45,
        [Description("Please enter your email address. Your email address will not be displayed to universities or students.")]
        Email = 46,
        [Description("Forward me any questions that students have:")]
        Forward_me_any_questions_that_students_have = 47

    }
}


