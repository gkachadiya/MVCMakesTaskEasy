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
        Admin = 3
    }
    //public enum CurrencyTypes
    //{
    //    USD = 1,
    //    AUD = 2,
    //    GBP = 3,
    //    INR = 4,
    //    EURO = 5
    //}

    public enum CurrencyTypes
    {
        [Description("Abkhazia(RUB)")]
        Abkhazia=1,
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

    public enum CoursesOffered
    {
        Accountancy = 1,
        Administration = 2,
        [Description("Adult Education")]
        Adult_Education = 3,
        [Description("Advanced Study")]
        Advanced_Study = 4,
        [Description("Applied Anthropology")]
        Applied_Anthropology = 5,
        [Description("Applied Politics")]
        Applied_Politics = 6,
        [Description("Applied Sciences")]
        Applied_Sciences = 7,
        Architecture = 8,
        [Description("Archival Studies")]
        Archival_Studies = 9,
        [Description("Christian Education")]
        Christian_Education = 10,
        [Description("City and Regional Planning")]
        City_and_Regional_Planning = 11,
        [Description("Clinical Medical Science")]
        Clinical_Medical_Science = 12,
        Communication = 13,
        [Description("Computer Science")]
        Computer_Science = 14,
        [Description("Criminal Justice")]
        Criminal_Justice = 15,
        [Description("Cross-Cultural and International Education")]
        Cross_Cultural_and_International_Education = 16,
        [Description("Dentistry")]
        Dentistry = 17,
        [Description("Digital Media")]
        Digital_Media = 18,
        [Description(" Dispute Resolution")]
        Dispute_Resolution = 19,
        Divinity = 20,
        Education = 21,
        Finance = 22,
        [Description("Educational Technology")]
        Educational_Technology = 23,
        [Description("Electronic Business Technologies")]
        Electronic_Business_Technologies = 24,
        Engineering = 25,
        Environment = 26,
        [Description("Environmental Design")]
        Environmental_Design = 27,
        [Description("Environmental Management")]
        Environmental_Management = 28,
        [Description("Environmental Science")]
        Environmental_Science = 29,
        [Description("Environmental Studies")]
        Environmental_Studies = 30,
        [Description("Fine Arts")]
        Fine_Arts = 31,
        [Description("Forensic Science and Law")]
        Forensic_Science_and_Law = 32,
        [Description("Forensic Sciences")]
        Forensic_Sciences = 33,
        [Description("Forestry")]
        Forestry = 34,
        [Description("Global Affairs")]
        Global_Affairs = 35,
        [Description("Health Administration")]
        Health_Administration = 36,
        [Description("Health Science")]
        Health_Science = 37,
        [Description("Historic Preservation")]
        Historic_Preservation = 38,
        [Description("History")]
        History = 39,
        [Description("Humanities")]
        Humanities = 40,
        [Description("Industrial and Labor Relations")]
        Industrial_and_Labor_Relations = 41,
        [Description("Industrial Design")]
        Industrial_Design = 42,
        [Description("Information")]
        Information = 43,
        [Description("Information Management")]
        Information_Management = 44,
        [Description("Information Systems")]
        Information_Systems = 45,
        [Description("Information Technology")]
        Information_Technology = 46,
        [Description("Interactive Media")]
        Interactive_Media = 47,
        [Description("International Business")]
        International_Business = 48,
        [Description("International Development")]
        International_Development = 49,
        [Description("International Economics and Finance")]
        International_Economics_and_Finance = 50,
        [Description("International Hotel Management")]
        International_Hotel_Management = 51,
        [Description("Jurisprudence")]
        Jurisprudence = 52,
        [Description("Landscape Architecture")]
        Landscape_Architecture = 53,
        [Description("Laws")]
        Laws = 54,
        [Description("Leadership")]
        Leadership = 55,
        [Description("Liberal Studies")]
        Liberal_Studies = 56,
        [Description("Library Science")]
        Library_Science = 57,
        [Description("Logistics, Trade, and Transportation")]
        Logistics_Trade_and_Transportation = 58,
        [Description("Management")]
        Management = 59,
        [Description("Management in the Network Economy")]
        Management_in_the_Network_Economy = 60,
        [Description("Management Sciences")]
        Management_Sciences = 70,
        [Description("Marketing and Communication")]
        Marketing_and_Communication = 71,
        [Description("Marketing Research")]
        Marketing_Research = 72,
        [Description("Mass Communication")]
        Mass_Communication = 74,
        [Description("Medical Education")]
        Medical_Education = 75,
        [Description("Medical Science")]
        Medical_Science = 76,
        [Description("Ministry")]
        Ministry = 77,
        [Description("Music")]
        Music = 78,
        [Description("Music Education")]
        Music_Education = 79,
        [Description("Natural Sciences")]
        Natural_Sciences = 80,
        [Description("Nonprofit Management")]
        Nonprofit_Management = 81,
        [Description("Nonprofit Organizations")]
        Nonprofit_Organizations = 82,
        [Description("Nurse Anesthesia")]
        Nurse_Anesthesia = 83,
        [Description("Nursing")]
        Nursing = 84,
        [Description("Occupational Therapy")]
        Occupational_Therapy = 85,
        [Description("Organizational Leadership")]
        Organizational_Leadership = 86,
        [Description("Pacific International Affairs")]
        Pacific_International_Affairs = 87,
        [Description("Pharmacy")]
        Pharmacy = 88,
        [Description("Philosophy")]
        Philosophy = 89,
        [Description("Physician Assistant Studies")]
        Physician_Assistant_Studies = 90,
        [Description("Professional Counseling")]
        Professional_Counseling = 91,
        [Description("Professional Studies")]
        Professional_Studies = 92,
        [Description("Professional Writing")]
        Professional_Writing = 93,
        [Description("Project Management")]
        Project_Management = 94,
        [Description("Public Administration")]
        Public_Administration = 95,
        [Description("Public Health")]
        Public_Health = 96,
        [Description("Public Management")]
        Public_Management = 97,
        [Description("Public Policy")]
        Public_Policy = 98,
        [Description("Public Service")]
        Public_Service = 99,
        [Description("Resource Management")]
        Resource_Management = 100,
        [Description("Sacred Music")]
        Sacred_Music = 101,
        [Description("Security Technologies")]
        Security_Technologies = 102,
        [Description("Social Work")]
        Social_Work = 103,
        [Description("Strategic Leadership")]
        Strategic_Leadership = 104,
        [Description("Taxation")]
        Taxation = 105,
        [Description("Teaching")]
        Teaching = 106,
        [Description("Theology")]
        Theology = 107,
        [Description("Urban Planning")]
        Urban_Planning = 108,
        [Description("Urban Studies")]
        Urban_Studies = 109

    }

    public enum Programs
    {
        Bachelors = 1,
        Masters = 2,
        PHD = 3,
        Associates = 4
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
        [Description("Pursuing s Bachelor's Degree (B.S, B.A, B.com, B.E etc)")]
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
        bachelors = 1,
        [Description("Master's program")]
        Masters = 2,
        [Description("Phd Program")]
        Phd = 3,
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
        Bachelor = 3,
        Master = 4,
        Phd = 5,
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
        Afghanistan = +93,
        Albania = +355,
        Armenia = +374,
        Netherlands_Antilles = +599,
        Angola = +244,
        Antarctica = +672,
        Argentina = +54,
        American_Samoa = +1684,
        Austria  = +43,
        Australia = +61,
        Aruba = +297,
        Azerbaijan = +994,
        Bosnia_Herzegovina = +387,
        Barbados = +246,
        Bangladesh = +880,
        Belgium = +32,
        Burkina_Faso = +226,
        Bulgaria = +359,
        Bahrain = +973,
        Burundi = +257,
        Benin = +229,
        Guadeloupe_French= +590,
        Bermuda = +1441,
        Brunei_Darussalam = +673,
        Bolivia = +591,
        Brazil = +55,
        Bahamas = +1242,
        Bhutan = +975,
        Botswana = +267,
        Belarus = +375,
        Belize = +501,
        Canada = +1,
        Cocos_Islands = +61,
        Congo = +243,
        Central_African_Republic = +236,
        Congo_Dem_Republic = +242,
        Switzerland = +41,
        Ivory_Coast = +225,
        Cook_Islands = +682,
        Chile = +56,
        Cameroon = +237,
        China = +86,
        Colombia = +57,
        Costa_Rica = +506,
        Cuba = +53,
        Cape_Verde = +238,
        Christmas_Island = +61,
        Cyprus = +357,
        Czech_Rep = +420,
        Germany = +49,
        Djibouti = +253,
        Denmark = +45,
        Dominica = +1767,
        Dominican_Republic = +809,
        Algeria = +213,
        Ecuador = +593,
        Estonia = +372,
        Egypt = +20,
        Eritrea = +291,
        Spain = +34,
        Ethiopia = +251,
        Finland = +358,
        Fiji = +679,
        Falkland_Islands  = +500,
        Micronesia = +691,
        Faroe_Islands = +298,
        France = +33,
        Gabon = +241,
        UK = +44,
        Grenada = +1473,
        Georgia = +995,
        Ghana = +233,
        Gibraltar = +350,
        Greenland = +299,
        Gambia = +220,
        Guinea = +224,
        Equatorial_Guinea = +240,
        Greece = +30,
        Guatemala = +502,
        Guam_USA = +1671,
        Guinea_Bissau = +245,
        Guyana = +592,
        Hong_Kong = +852,
        Honduras = +504,
        Croatia = +385,
        Haiti = +509,
        Hungary = +36,
        Indonesia = +62,
        IE = +353,
        IL = +972,
        IM = +44,
        IN = +91,
        IQ = +964,
        IR = +98,
        IS = +354,
        IT = +39,
        JM = +1876,
        JO = +962,
        JP = +81,
        KE = +254,
        KG = +996,
        KH = +855,
        KI = +686,
        KM = +269,
        KN = +1869,
        KP = +850,
        KR = +82,
        KW = +965,
        KY = +1345,
        KZ = +7,
        LA = +856,
        LB = +961,
        LC = +1758,
        LI = +423,
        LK = +94,
        LR = +231,
        LS = +266,
        LT = +370,
        LU = +352,
        LV = +371,
        LY = +218,
        MA = +212,
        MC = +377,
        MD = +373,
        ME = +382,
        MF = +1599,
        MG = +261,
        MH = +692,
        MK = +389,
        ML = +223,
        MM = +95,
        MN = +976,
        MO = +853,
        MP = +1670,
        MR = +222,
        MS = +1664,
        MT = +356,
        MU = +230,
        MV = +960,
        MW = +265,
        MX = +52,
        MY = +60,
        MZ = +258,
        NA = +264,
        NC = +687,
        NE = +227,
        NG = +234,
        NI = +505,
        NL = +31,
        NO = +47,
        NP = +977,
        NR = +674,
        NU = +683,
        NZ = +64,
        OM = +968,
        PA = +507,
        PE = +51,
        PF = +689,
        PG = +675,
        PH = +63,
        PK = +92,
        PL = +48,
        PM = +508,
        PN = +870,
        PR = +1,
        PT = +351,
        PW = +680,
        PY = +595,
        QA = +974,
        RO = +40,
        RS = +381,
        RU = +7,
        RW = +250,
        SA = +966,
        SB = +677,
        SC = +248,
        SD = +249,
        SE = +46,
        SG = +65,
        SH = +290,
        SI = +386,
        SK = +421,
        SL = +232,
        SM = +378,
        SN = +321,
        SO = +252,
        SR = +597,
        ST = +239,
        SV = +503,
        SY = +963,
        SZ = +268,
        TC = +1649,
        TD = +235,
        TG = +228,
        TH = +66,
        TJ = +992,
        TK = +690,
        TL = +670,
        TM = +993,
        TN = +216,
        TO = +676,
        TR = +690,
        TT = +1868,
        TV = +688,
        TW = +886,
        TZ = +255,
        UA = +380,
        UG = +256,
        US = +1,
        UY = +598,
        UZ = +998,
        VA = +39,
        VC = +1784,
        VE = +58,
        VG = +1284,
        VI = +1340,
        VN = +84,
        VU = +678,
        WF = +681,
        WS = +685,
        YE = +967,
        YT = +262,
        ZA = +27,
        ZM = +260,
        ZW = +263,
    }
}


