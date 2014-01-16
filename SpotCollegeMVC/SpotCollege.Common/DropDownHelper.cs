/// <summary>
/// This class is useful for getting selectlistitem. That is useful for 
/// binding DropDown in view.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SpotCollege.Common
{
    public class DropDownHelper
    {
        /// <summary>
        /// This method is useful to bind currently in dropdown
        /// </summary>
        public static List<SelectListItem> getCurrentlyIn(string selectValue)
        {
            string[] names = null;
            Array value = null;
            int[] Values = null;
            names = Enum.GetNames(typeof(CurrentlyIn));
            value = Enum.GetValues(typeof(CurrentlyIn));
            Values = (int[])value;
            List<SelectListItem> spm = new List<SelectListItem>();
            spm.Add(new SelectListItem
            {
                Text = "Select Any",
                Value = "0"
            });
            for (int i = 0; i < names.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((CurrentlyIn)Values[i]),
                    Value = Values[i].ToString(),
                    Selected = Values[i].ToString() == selectValue
                });
            }
            return spm;
        }

        public static List<SelectListItem> getUsers(string selectValue)
        {
            List<SelectListItem> spm = new List<SelectListItem>();
            spm.Add(new SelectListItem
            {
                Text = "University",
                Value = "University",
                Selected = "University" == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "Student",
                Value = "Student",
                Selected = "Student" == selectValue
            });
            return spm;
        }

        /// <summary>
        /// This method is useful to bind coutnry list dropdown
        /// </summary>
        public static List<SelectListItem> getCountry(string selectValue)
        {
            string[] Countrynames = Enum.GetNames(typeof(CountryList));
            Array Countryvalue = Enum.GetValues(typeof(CountryList));
            int[] CountryValues = (int[])Countryvalue;
            List<SelectListItem> spm = new List<SelectListItem>();

            spm.Add(new SelectListItem
            {
                Text = "Select Country",
                Value = "0"
            });
            for (int i = 0; i < Countrynames.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]),
                    Value = CountryValues[i].ToString(),
                    Selected = EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]) == selectValue
                });
            }
           
            return spm;
        }
        public static List<SelectListItem> getCountryFeedback(string selectValue)
        {
            string[] Countrynames = Enum.GetNames(typeof(CountryList));
            Array Countryvalue = Enum.GetValues(typeof(CountryList));
            int[] CountryValues = (int[])Countryvalue;
            List<SelectListItem> spm = new List<SelectListItem>();

            
            for (int i = 0; i < Countrynames.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]),
                    Value = CountryValues[i].ToString(),
                    Selected = EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]) == selectValue
                });
            }

            return spm;
        }

        public static List<SelectListItem> getCountryAdmin()
        {
            string[] Countrynames = Enum.GetNames(typeof(CountryList));
            Array Countryvalue = Enum.GetValues(typeof(CountryList));
            int[] CountryValues = (int[])Countryvalue;
            List<SelectListItem> spm = new List<SelectListItem>();

            spm.Add(new SelectListItem
            {
                Text = "Select Country",
                Value = "0"
            });
            for (int i = 0; i < Countrynames.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]),
                    Value = CountryValues[i].ToString(),
                });
            }
            spm.Add(new SelectListItem
            {
                Text = "Other",
                Value = Countrynames.Length.ToString()
            });
            return spm;
        }

        public static List<SelectListItem> getCountryAll(string selectValue)
        {
            string[] Countrynames = Enum.GetNames(typeof(CountryList));
            Array Countryvalue = Enum.GetValues(typeof(CountryList));
            int[] CountryValues = (int[])Countryvalue;
            List<SelectListItem> spm = new List<SelectListItem>();

            spm.Add(new SelectListItem
            {
                Text = "ALL",
                Value = "0"
            });
            for (int i = 0; i < Countrynames.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]),
                    Value = CountryValues[i].ToString(),
                    Selected = EnumHelper.GetDescriptionFromEnumValue((CountryList)CountryValues[i]) == selectValue
                });
            }

            return spm;
        }

        /// <summary>
        /// This method is useful to bind currency dropdown
        /// </summary>
        public static List<SelectListItem> getCurrency(string selectValue)
        {
            string[] currnecyNames = Enum.GetNames(typeof(CurrencyTypes));
            Array currncyValue = Enum.GetValues(typeof(CurrencyTypes));
            int[] currncyValues = (int[])currncyValue;

            List<SelectListItem> sli = new List<SelectListItem>();
            for (int i = 0; i < currnecyNames.Length; i++)
            {
                sli.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((CurrencyTypes)currncyValues[i]),
                    Value = currncyValues[i].ToString(),
                    Selected = currncyValues[i].ToString() == selectValue
                });
            }
            return sli;
        }

        /// <summary>
        /// This method is useful to bind currency dropdown
        /// </summary>
        public static List<SelectListItem> getUnitsForCost(string selectValue)
        {
            List<SelectListItem> sli = new List<SelectListItem>();

            sli.Add(new SelectListItem
            {
                Text = "Per Year",
                Value = "Per Year",
                Selected = "Per Year" == selectValue
            });
            sli.Add(new SelectListItem
            {
                Text = "Per Unit",
                Value = "Per Unit",
                Selected = "Per Unit" == selectValue
            });
            sli.Add(new SelectListItem
            {
                Text = "Per Semester",
                Value = "Per Semester",
                Selected = "Per Semester" == selectValue
            });
            return sli;
        }
        /// <summary>
        /// This method is useful to bind available programs
        /// </summary>
        public static List<SelectListItem> getPrograms(int selectValue)
        {
            string[] names = null;
            Array value = null;
            int[] Values = null;

            names = Enum.GetNames(typeof(ProgramLookingFor));
            value = Enum.GetValues(typeof(ProgramLookingFor));
            Values = (int[])value;
            List<SelectListItem> spm = new List<SelectListItem>();
            spm.Add(new SelectListItem
            {
                Text = "Select Program",
                Value = "0"
            });
            for (int i = 0; i < names.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((ProgramLookingFor)Values[i]),
                    Value = Values[i].ToString(),
                    Selected = Values[i] == selectValue
                });
            }

            return spm;
        }

        public static List<SelectListItem> getDegreeProgram(int selectValue)
        {
            string[] names = null;
            Array value = null;
            int[] Values = null;

            names = Enum.GetNames(typeof(Programs));
            value = Enum.GetValues(typeof(Programs));
            Values = (int[])value;
            List<SelectListItem> spm = new List<SelectListItem>();
            spm.Add(new SelectListItem
            {
                Text = "Select Degree",
                Value = "0"
            });
            for (int i = 0; i < names.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((Programs)Values[i]),
                    Value = Values[i].ToString(),
                    Selected = Values[i] == selectValue
                });
            }

            return spm;
        }


        /// <summary>
        /// This method is useful to bind available expected start dates
        /// </summary>
        public static List<SelectListItem> getExpectedDates(int selectValue)
        {
            string[] names = null;
            Array value = null;
            int[] Values = null;

            names = Enum.GetNames(typeof(expectedStart));
            value = Enum.GetValues(typeof(expectedStart));
            Values = (int[])value;

            List<SelectListItem> spm = new List<SelectListItem>();
            spm.Add(new SelectListItem
            {
                Text = "Select Any",
                Value = "0"
            });

            for (int i = 0; i < names.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((expectedStart)Values[i]),
                    Value = Values[i].ToString(),
                    Selected = Values[i] == selectValue
                });
            }
            return spm;
        }

        /// <summary>
        /// This method is useful to bind available expected start dates
        /// </summary>
        public static List<SelectListItem> getPayouts(int selectValue)
        {
            List<SelectListItem> spm = new List<SelectListItem>();
            spm.Add(new SelectListItem
            {
                Text = "Select Any",
                Value = "0",
                Selected = 0 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "$3000-5000",
                Value = "1",
                Selected = 1 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "$5000-10000",
                Value = "2",
                Selected = 2 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "$10000-15000",
                Value = "3",
                Selected = 3 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "$15000+",
                Value = "4",
                Selected = 4 == selectValue
            });
            return spm;
        }


        /// <summary>
        /// This method is useful to bind available cources
        /// </summary>
        public static List<SelectListItem> getAvailableCources(string selectValue)
        {
            string[] names = null;
            Array value = null;
            int[] Values = null;

            names = Enum.GetNames(typeof(CoursesOffered));
            value = Enum.GetValues(typeof(CoursesOffered));
            Values = (int[])value;

            List<SelectListItem> spm = new List<SelectListItem>();
            spm.Add(new SelectListItem
            {
                Text = "Select Any",
                Value = "0",
            });
            for (int i = 0; i < names.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((CoursesOffered)Values[i]),
                    Value = Values[i].ToString(),
                    Selected = Values[i].ToString() == selectValue
                });
            }
            return spm;
        }



        /// <summary>
        /// This method is useful to bind available programs
        /// </summary>
        public static List<SelectListItem> getLevelsofEducation(int selectValue)
        {
            string[] names = null;
            Array value = null;
            int[] Values = null;

            names = Enum.GetNames(typeof(LevelCompleted));
            value = Enum.GetValues(typeof(LevelCompleted));
            Values = (int[])value;

            List<SelectListItem> spm = new List<SelectListItem>();
            spm.Add(new SelectListItem
            {
                Text = "Select Any",
                Value = "0"
            });
            for (int i = 0; i < names.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((LevelCompleted)Values[i]),
                    Value = Values[i].ToString(),
                    Selected = Values[i] == selectValue
                });
            }
            return spm;

        }

        /// <summary>
        /// This method is useful to bind available programs
        /// </summary>
        public static List<SelectListItem> getDegree(int selectValue)
        {
            string[] names = null;
            Array value = null;
            int[] Values = null;

            names = Enum.GetNames(typeof(Degreepursued));
            value = Enum.GetValues(typeof(Degreepursued));
            Values = (int[])value;

            List<SelectListItem> spm = new List<SelectListItem>();
            spm.Add(new SelectListItem
            {
                Text = "Select Any",
                Value = "0"
            });
            for (int i = 0; i < names.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((Degreepursued)Values[i]),
                    Value = Values[i].ToString(),
                    Selected = Values[i] == selectValue
                });
            }
            return spm;
        }
        /// <summary>
        /// This method is useful to bind available Courses in Peticular University
        /// </summary>
        public static List<SelectListItem> getCourses(string selectValue)
        {
            string[] Coursesnames = Enum.GetNames(typeof(CoursesOffered));
            Array Coursesvalue = Enum.GetValues(typeof(CoursesOffered));
            int[] CoursesValues = (int[])Coursesvalue;
            List<SelectListItem> spm = new List<SelectListItem>();

            spm.Add(new SelectListItem
            {
                Text = "Select Course",
                Value = "0"
            });
            for (int i = 0; i < Coursesnames.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((CoursesOffered)CoursesValues[i]),
                    Value = CoursesValues[i].ToString(),
                    Selected = EnumHelper.GetDescriptionFromEnumValue((CoursesOffered)CoursesValues[i]) == selectValue
                });
            }
            return spm;
        }

        /// <summary>
        /// This method is useful to bind available phone types
        /// </summary>
        public static List<SelectListItem> getPhoneType(string selectValue)
        {
            List<SelectListItem> spm = new List<SelectListItem>();
            spm.Add(new SelectListItem
            {
                Text = "Select One",
                Value = "0"
            });
            string[] PhoneTypenames = Enum.GetNames(typeof(PhoneTypes));
            Array value = Enum.GetValues(typeof(PhoneTypes));
            int[] Values = (int[])value;
            for (int i = 0; i < PhoneTypenames.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = PhoneTypenames[i],
                    Value = Values[i].ToString(),
                    Selected = Values[i].ToString() == selectValue
                });
            }
            return spm;
        }

        /// <summary>
        /// This method is useful to bind available country code 
        /// </summary>
        //public static List<SelectListItem> getCountryCode(string selectValue)
        //{
        //    List<SelectListItem> spm = new List<SelectListItem>();
        //    spm.Add(new SelectListItem
        //    {
        //        Text = "Select Country Code",
        //        Value = "0"
        //    });

        //    string[] CountryCodenames = Enum.GetNames(typeof(CountryCode));
        //    Array value1 = Enum.GetValues(typeof(CountryCode));
        //    int[] Values1 = (int[])value1;
        //    for (int i = 0; i < CountryCodenames.Length; i++)
        //    {
        //        spm.Add(new SelectListItem
        //        {
        //            Text = CountryCodenames[i] ,
        //            Value = Values1[i].ToString(),
        //            Selected = Values1[i].ToString() == selectValue
        //        });
        //    }
        //    return spm;
        //}

        public static List<SelectListItem> getCountryCode(string selectValue)
        {
            string[] names = null;
            Array value = null;
            int[] Values = null;

            names = Enum.GetNames(typeof(CountryCode));
            value = Enum.GetValues(typeof(CountryCode));
            Values = (int[])value;

            List<SelectListItem> spm = new List<SelectListItem>();
            

            for (int i = 0; i < names.Length; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionFromEnumValue((CountryCode)Values[i]),
                    Value = Values[i].ToString(),
                    Selected = Values[i].ToString() == selectValue
                });
            }
            spm = spm.OrderBy(x => x.Text).ToList();
            //spm.Add(new SelectListItem
            //{
            //    Text = "Select Country Code",
            //    Value = "0"
            //});
            spm.Insert(0, (new SelectListItem { Text = "Select Country Code", Value = "0" }));
            return spm;
        }
        /// <summary>
        /// This method is useful to bind Day to dropdown 
        /// </summary>
        public static List<SelectListItem> getDays(int selectValue)
        {
            List<SelectListItem> spm = new List<SelectListItem>();
            spm.Add(new SelectListItem
            {
                Text = "Select Day",
                Value = "0"
            });
            for (int i = 1; i <= 31; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = i == selectValue
                });
            }
            return spm;
        }

        /// <summary>
        /// This method is useful to bind Year to dropdown 
        /// </summary>
        public static List<SelectListItem> getYears(int selectValue, int start = 1950, int end = 2021)
        {
            List<SelectListItem> spm = new List<SelectListItem>();
            spm.Add(new SelectListItem
            {
                Text = "Select Year",
                Value = "0"
            });
            for (int i = start; i <= end; i++)
            {
                spm.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = i == selectValue
                });
            }
            return spm;
        }

        /// <summary>
        /// This method is useful to bind Month to dropdown 
        /// </summary>
        public static List<SelectListItem> getMonths(int selectValue)
        {
            List<SelectListItem> spm = new List<SelectListItem>();
            spm.Add(new SelectListItem
            {
                Text = "Select Month",
                Value = "0",
                Selected = 0 == selectValue
            });

            spm.Add(new SelectListItem
            {
                Text = "Jan",
                Value = "01",
                Selected = 01 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "Feb",
                Value = "02",
                Selected = 02 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "Mar",
                Value = "03",
                Selected = 03 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "April",
                Value = "04",
                Selected = 04 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "May",
                Value = "05",
                Selected = 05 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "Jun",
                Value = "06",
                Selected = 06 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "July",
                Value = "07",
                Selected = 07 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "Aug",
                Value = "08",
                Selected = 08 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "Sep",
                Value = "09",
                Selected = 09 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "Oct",
                Value = "10",
                Selected = 10 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "Nov",
                Value = "11",
                Selected = 11 == selectValue
            });
            spm.Add(new SelectListItem
            {
                Text = "Dec",
                Value = "12",
                Selected = 12 == selectValue
            });
            return spm;
        }

    }
}
