using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Ex03.GarageLogic.EnumClasses;
using Ex03.GarageLogic.GarageManagement;
using Ex03.GarageLogic.ExceptionsFolder;

namespace Ex03.ConsoleUI
{
    internal class UI
    {
        private const int k_OneFlag = 1;
        private const float k_ZeroFlag = 0.0f;
        private const int k_MinusOneFlag = -1;
        private const int k_TimeOutPeriod = 1000;
        private const string k_QuitFlag = "Q";
        private readonly Garage r_Garage;
        
        internal UI()
        {
            r_Garage = new Garage();
        }

        internal bool RunGarageRequest()
        {
            askForTreatment(out GarageOptions.eOptions decision);
            bool returnValue = true;
            switch (decision)
            {
                case GarageOptions.eOptions.Exit:
                    returnValue = false;
                    break;
                case GarageOptions.eOptions.InsertANewVehicle:
                    insertANewVehicle();
                    break;
                case GarageOptions.eOptions.DisplayLicenseNumbers:
                    displayLicenseNumbers();
                    break;
                case GarageOptions.eOptions.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case GarageOptions.eOptions.InflateTiresToMaximum:
                    inflateTiresToMaximum();
                    break;
                case GarageOptions.eOptions.RefuelVehicle:
                    refuelVehicle(string.Empty, k_MinusOneFlag);
                    break;
                case GarageOptions.eOptions.ChargeElectricVehicle:
                    chargeElectricVehicle(string.Empty);
                    break;
                default:
                    displayInformation();
                    break;
            }

            return returnValue;
        }

        private static void askForTreatment(out GarageOptions.eOptions o_Decision)
        {
            Console.Clear();
            StringBuilder startSentence = new StringBuilder();
            StringBuilder inValidChoiceSentence = new StringBuilder();
            StringBuilder optionsSentence = new StringBuilder();
            startSentence.AppendFormat("Welcome to our garage, please choose one of the following options:{0}", Environment.NewLine);
           optionsSentence.AppendFormat("press 0 to Exit{0}", Environment.NewLine);
            optionsSentence.AppendFormat("press 1 to insert a new vehicle into the garage{0}", Environment.NewLine);
            optionsSentence.AppendFormat("press 2 to display a list of license numbers currently in the garage{0}", Environment.NewLine);
            optionsSentence.AppendFormat("press 3 to change a certain vehicle’s status{0}", Environment.NewLine);
            optionsSentence.AppendFormat("press 4 to inflate tires to maximum{0}", Environment.NewLine);
            optionsSentence.AppendFormat("press 5 to refuel a fuel-based vehicle{0}", Environment.NewLine);
            optionsSentence.AppendFormat("press 6 to charge an electric-based vehicle{0}", Environment.NewLine);
            optionsSentence.AppendFormat("press 7 to display vehicle information");
            inValidChoiceSentence.AppendFormat("Input is not valid please choose only one of the following options:{0}", Environment.NewLine);
            startSentence.Append(optionsSentence);
            inValidChoiceSentence.Append(optionsSentence);
            Console.WriteLine(startSentence);
            int decisionNumber;
            while (!int.TryParse(Console.ReadLine(), out decisionNumber) || ((decisionNumber < k_ZeroFlag) || (decisionNumber >= Enum.GetValues(typeof(GarageOptions.eOptions)).Length)))
            {
                Console.WriteLine(inValidChoiceSentence);
            }

            o_Decision = (GarageOptions.eOptions)decisionNumber;
        }

        private static bool checkCellPhone(string i_Name)
        {
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(i_Name);
        }

        private static bool checkName(string i_Name)
        {
            Regex regex = new Regex("^[a-zA-Z]+$");
            bool checkIfAccept = regex.IsMatch(i_Name);
            return checkIfAccept;
        }

        private void insertANewVehicle()
        {
            StringBuilder askForModelName = new StringBuilder("please enter your model name");
            StringBuilder askForLicenseNumber = new StringBuilder("please enter your license number");
            StringBuilder askForWheelManufacturerName = new StringBuilder("please enter your Wheel Manufacturer Name");
            StringBuilder askForName = new StringBuilder("Please enter Your name");
            StringBuilder askForOwnerPhone = new StringBuilder("Please enter Your cell phone");
            StringBuilder insertSentence = new StringBuilder();
            StringBuilder inValidChoiceSentence = new StringBuilder();
            StringBuilder optionsSentence = new StringBuilder();
            string ownerName;
            string ownerCellPhone;
            string modelName;
            string licenseNumber;
            string wheelManufacturerName;
            insertSentence.AppendFormat("Lets register your vehicle, please enter your type of vehicle:{0}", Environment.NewLine);
            inValidChoiceSentence.Append(optionsSentence);
            inValidChoiceSentence.AppendFormat("Please choose only from the option below");
            Console.WriteLine(askForName);
            while(checkName(ownerName = Console.ReadLine()) == false)
            {
                Console.WriteLine("please enter valid name");
            }

            Console.WriteLine(askForOwnerPhone);
            while (checkCellPhone(ownerCellPhone = Console.ReadLine()) == false)
            {
                Console.WriteLine("please enter valid cell phone");
            }

            Console.Write(insertSentence);
            printEnums(typeof(Registration.eVehiclesType));
            int decisionNumber;
            while (!int.TryParse(Console.ReadLine(), out decisionNumber) || ((decisionNumber < k_ZeroFlag) || (decisionNumber >= Enum.GetValues(typeof(Registration.eVehiclesType)).Length)))
            {
                Console.WriteLine(inValidChoiceSentence);
                printEnums(typeof(Registration.eVehiclesType));
            }

            Console.WriteLine(askForModelName);
            while ((modelName = Console.ReadLine()) == null || modelName.Equals(string.Empty))
            {
                Console.WriteLine("please enter valid model name");
            }
            
            Console.WriteLine(askForWheelManufacturerName);
            while (checkName(wheelManufacturerName = Console.ReadLine()) == false)
            {
                Console.WriteLine("please enter valid wheel manufacturer name");
            }

            Console.WriteLine(askForLicenseNumber);
            while ((licenseNumber = Console.ReadLine()) == null || licenseNumber.Equals(string.Empty))
            {
                Console.WriteLine("please enter valid license number");
            }

            try
            {
                r_Garage.InsertANewVehicleCardIntoTheGarage(ownerName, ownerCellPhone, modelName, licenseNumber, wheelManufacturerName, (Registration.eVehiclesType)decisionNumber);
            }
            catch (ArgumentException error)
            {
                Console.WriteLine(error.Message);
                System.Threading.Thread.Sleep(5 * k_TimeOutPeriod);
                return;
            }

            insertAmountOfEnergyLeftToVehicleCard();
            insertAmountOfWheelPressure();
            insertAllSpecialAttribute();
            r_Garage.MoveCurrentVehicleCardOutOfRegistration();
            Console.WriteLine("Thank you for you patient, lets check if you need any more assistance");
            System.Threading.Thread.Sleep(3 * k_TimeOutPeriod);
        }

        private void insertAmountOfEnergyLeftToVehicleCard()
        {
            StringBuilder invalidFloat = new StringBuilder("Please enter a number");
            string typeOfEnergy = r_Garage.TypeOfCurrentCarEnergy();
            StringBuilder askForEnergy = new StringBuilder();
            askForEnergy.AppendFormat("please insert how much {0} you have left", typeOfEnergy);
            Console.WriteLine(askForEnergy);
            try
            {
                float energyLeft;
                while (float.TryParse(Console.ReadLine(), out energyLeft) == false)
                {
                    Console.WriteLine(invalidFloat);
                }

                r_Garage.InsertAmountOfEnergyVehicleCard(energyLeft);
            }
            catch(ValueOutOfRangeException error)
            {
                Console.WriteLine(error.Message);
                insertAmountOfEnergyLeftToVehicleCard();
            }
        }

        private void insertAmountOfWheelPressure()
        {
            StringBuilder invalidFloat = new StringBuilder("Please enter a number");
            StringBuilder askForWheelPressure = new StringBuilder("Please insert how much air pressure your wheels have");
            Console.WriteLine(askForWheelPressure);
            try
            {
                float currentAmountOfWheelPressure;
                while (float.TryParse(Console.ReadLine(), out currentAmountOfWheelPressure) == false)
                {
                    Console.WriteLine(invalidFloat);
                }

                r_Garage.InsertWheelPressureToCurrentVehicle(currentAmountOfWheelPressure);
            }
            catch(ValueOutOfRangeException error)
            {
                Console.WriteLine(error.Message);
                insertAmountOfWheelPressure();
            }
        }

        private void printEnums(Type i_EnumType)
        {
            Dictionary<int, string> enumDict = getEnumDict(i_EnumType);
            StringBuilder stringToReturn = new StringBuilder();
            int totalAmountDictionary = enumDict.Count;
            foreach (KeyValuePair<int, string> pair in enumDict)
            {
                if (totalAmountDictionary == k_OneFlag)
                {
                    stringToReturn.AppendFormat("{0}. {1}", pair.Key, pair.Value);
                }
                else
                {
                    stringToReturn.AppendFormat("{0}. {1}{2}", pair.Key, pair.Value, Environment.NewLine);
                }

                totalAmountDictionary--;
            }

            Console.WriteLine(stringToReturn);
        }
       
        private Dictionary<int, string> getEnumDict(Type i_EnumType)
        {
            Dictionary<int, string> enumDict = new Dictionary<int, string>();
            string[] namesWithOutSpaces = Enum.GetNames(i_EnumType);
            foreach (string nameToCheck in namesWithOutSpaces)
            {
                int enumValue = (int)Enum.Parse(i_EnumType, nameToCheck);
                StringBuilder stringToReturn  = new StringBuilder();
                stringToReturn.Append(nameToCheck[0]);
                foreach (char charToCheck in nameToCheck.Substring(1, nameToCheck.Length - 1))
                {
                    if (char.IsUpper(charToCheck))
                    {
                        stringToReturn.Append(" ");
                    }

                    stringToReturn.Append(charToCheck);
                }

                enumDict[enumValue] = stringToReturn.ToString();
            }

            return enumDict;
        }
        
        private void printByIndex(int i_Index)
        {
            StringBuilder stringToReturn = new StringBuilder();
            KeyValuePair<string, Type> currentAttribute = r_Garage.GetCurrentCarKeyValuePairByIndex(i_Index);
            Type typeToCheck = currentAttribute.Value;
            if (typeToCheck.IsEnum)
            {
                Console.WriteLine("Please choose the matching attribute for your vehicle {0}", currentAttribute.Key);
                printEnums(typeToCheck);
            }
            else if(typeToCheck == typeof(string))
            {
                Console.WriteLine("Please enter your vehicle {0}", currentAttribute.Key);
            }
            else if(typeToCheck == typeof(bool))
            {
                Console.WriteLine("Please enter if your vehicle has {0}, choose Yes/No", currentAttribute.Key);
            }
            else if (typeToCheck == typeof(int) || typeToCheck == typeof(float))
            {
                Console.WriteLine("Please enter the amount of {0} your vehicle has", currentAttribute.Key);
            }
        }
        
        private void insertSpecialAttributeByIndex(int i_Index)
        {
            printByIndex(i_Index);
            try
            {
                string toInsert = Console.ReadLine();
                r_Garage.InsertSpecialAttributeToCurrentCarByIndex(i_Index, toInsert);
            }
            catch (ArgumentException error)
            {
                Console.WriteLine("You wrote invalid option, lets try again");
                insertSpecialAttributeByIndex(i_Index);
            }
            catch (ValueOutOfRangeException error)
            {
                Console.WriteLine(error.Message);
                insertSpecialAttributeByIndex(i_Index);
            }
        }

        private void insertAllSpecialAttribute()
        { 
            Console.WriteLine("Now we will check for further more information");
            for(int i = 0; i < r_Garage.GetAmountOfSpecialAttribute(); i++)
            {
                insertSpecialAttributeByIndex(i);
            }
        }

        private void displayLicenseNumbers()
        {
            StringBuilder licenseSentence = new StringBuilder();
            StringBuilder inValidChoiceSentence = new StringBuilder();
            StringBuilder optionsSentence = new StringBuilder();
            StringBuilder statusSentence = new StringBuilder();
            statusSentence.AppendFormat("Please pick requested status from the following:{0}", Environment.NewLine);
            foreach (VehicleStatus.eStatus status in (VehicleStatus.eStatus[])Enum.GetValues(typeof(VehicleStatus.eStatus)))
            {
                optionsSentence.AppendFormat("press {0}  for \"{1}\" status{2}", (int)status, status, Environment.NewLine);
            }

            optionsSentence.Append("Your choice: ");
            inValidChoiceSentence.AppendFormat("Input is not valid please choose only one of the following options:{0}", Environment.NewLine);
            statusSentence.Append(optionsSentence);
            inValidChoiceSentence.Append(optionsSentence);
            Console.WriteLine(statusSentence);
            int decisionNumber;
            while (!int.TryParse(Console.ReadLine(), out decisionNumber) || ((decisionNumber < k_ZeroFlag) || (decisionNumber > Enum.GetValues(typeof(VehicleStatus.eStatus)).Length - 1)))
            {
                Console.WriteLine(inValidChoiceSentence);
            }

            List<string> licenseList = r_Garage.LicenseNumbersByStatus((VehicleStatus.eStatus)decisionNumber);
            if(licenseList.Count > k_ZeroFlag)
            {
                licenseSentence.AppendFormat(
                    "The corresponding list to status {0} is:{1}",
                    (VehicleStatus.eStatus)decisionNumber,
                    Environment.NewLine);
                foreach(string license in licenseList)
                {
                    licenseSentence.AppendFormat("{0}{1}", license, Environment.NewLine);
                }
            }
            else
            {
                licenseSentence.AppendFormat(
                    "Sorry we don\'t have any vehicles with {0} status{1}",
                    (VehicleStatus.eStatus)decisionNumber,
                    Environment.NewLine);
            }

            Console.Write(licenseSentence);
            Console.WriteLine("Thank you for you patient, lets check if you need any more assistance");
            System.Threading.Thread.Sleep(5 * k_TimeOutPeriod);
        }

        private void changeVehicleStatus()
        {
            StringBuilder licenseSentence = new StringBuilder();
            StringBuilder inValidChoiceSentence = new StringBuilder();
            StringBuilder licenseNotExistSentence = new StringBuilder();
            StringBuilder optionsSentence = new StringBuilder();
            StringBuilder statusSentence = new StringBuilder();
            licenseSentence.Append("Please enter the license number of the vehicle you want to change the status");
            inValidChoiceSentence.AppendFormat("something went wrong, please reenter the requested license number:{0}", Environment.NewLine);
            licenseNotExistSentence.AppendFormat(
                "The corresponding license does not exist in the system, please reenter the requested license number:{0}", Environment.NewLine);
            bool check = outOrExit(out string licenseNumber, licenseSentence.ToString());
            if (check == false)
            {
                return;
            }

            inValidChoiceSentence = new StringBuilder();
            int i = 0;
            foreach (VehicleStatus.eStatus status in (VehicleStatus.eStatus[])Enum.GetValues(typeof(VehicleStatus.eStatus)))
            {
                optionsSentence.AppendFormat("Press {0}  for \"{1}\" status", (int)status, status);
                if (i < Enum.GetValues(typeof(VehicleStatus.eStatus)).Length - 1)
                {
                    optionsSentence.AppendLine();
                }

                i++;
            }

            inValidChoiceSentence.AppendFormat("Input is not valid please choose only one of the following options:{0}", Environment.NewLine);
            statusSentence.Append(optionsSentence);
            inValidChoiceSentence.Append(optionsSentence);
            Console.WriteLine(statusSentence);
            int decisionNumber;
            while (!int.TryParse(Console.ReadLine(), out decisionNumber) || ((decisionNumber < k_ZeroFlag) || (decisionNumber > Enum.GetValues(typeof(VehicleStatus.eStatus)).Length - 1)))
            {
                Console.WriteLine(inValidChoiceSentence);
            }

            r_Garage.ChangeStatus(licenseNumber, (VehicleStatus.eStatus)decisionNumber);
            Console.WriteLine("The status is now {0}. Thank you for you patience, lets check if you need any more assistance", (VehicleStatus.eStatus)decisionNumber);
            System.Threading.Thread.Sleep(3 * k_TimeOutPeriod);
        }
        
        private bool outOrExit(out string o_LicenseNumber, string i_WriteSentence)
        {
            bool toReturn = true;
            StringBuilder startSentence = new StringBuilder(i_WriteSentence);
            startSentence.AppendFormat(", Or Q for returning to main menu");
            StringBuilder inValidChoiceSentence = new StringBuilder();
            StringBuilder licenseNotExistSentence = new StringBuilder();
            licenseNotExistSentence.AppendFormat(
                "The corresponding license does not exist in the system, please reenter the requested license number Or Q for returning to main menu{0}", Environment.NewLine);
            inValidChoiceSentence.AppendFormat("something went wrong, please reenter the requested license number Or Q for returning to main menu{0}", Environment.NewLine);
            Console.WriteLine(startSentence);
            o_LicenseNumber = Console.ReadLine();
            while (o_LicenseNumber == null || o_LicenseNumber.Equals(k_QuitFlag) || !r_Garage.IsLicenseNumberExist(o_LicenseNumber))
            {
                if (o_LicenseNumber == null)
                {
                    Console.WriteLine(inValidChoiceSentence);
                }
                else
                {
                    if (o_LicenseNumber.Equals(k_QuitFlag))
                    {
                        toReturn = false;
                        break;
                    }

                    Console.Write(licenseNotExistSentence);
                }

                o_LicenseNumber = Console.ReadLine();
            }

            return toReturn;
        }

        private void inflateTiresToMaximum()
        {
            StringBuilder licenseSentence = new StringBuilder();
            StringBuilder inValidChoiceSentence = new StringBuilder();
            StringBuilder licenseNotExistSentence = new StringBuilder();
            licenseSentence.Append("Please enter the license number of the vehicle you want to change the status");
            inValidChoiceSentence.AppendFormat("something went wrong, please reenter the requested license number:{0}", Environment.NewLine);
            licenseNotExistSentence.AppendFormat(
                "The corresponding license does not exist in the system, please reenter the requested license number:{0}", Environment.NewLine);
            bool check = outOrExit(out string licenseNumber, licenseSentence.ToString());
            if (check == false)
            {
                return;
            }

            Console.WriteLine("Inflating tires to maximum...");
            System.Threading.Thread.Sleep(2 * k_TimeOutPeriod);
            Console.WriteLine("Tires are inflated! Thank you for you patient, lets check if you need any more assistance");
            System.Threading.Thread.Sleep(k_TimeOutPeriod);
            r_Garage.InflateWheelsToMaximum(licenseNumber);
        }
      
        private void refuelVehicle(string i_LicenseNumber, int i_DecisionNumber)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder licenseSentence = new StringBuilder();
            StringBuilder inValidChoiceSentence = new StringBuilder();
            StringBuilder fuelTypeSentence = new StringBuilder();
            StringBuilder amountSentence = new StringBuilder();
            StringBuilder optionsSentence = new StringBuilder();
            StringBuilder licenseNotExistSentence = new StringBuilder();
            licenseSentence.Append("Please enter the license number of the vehicle you want to refuel");
            inValidChoiceSentence.AppendFormat("something went wrong, please reenter the requested license number:{0}", Environment.NewLine);
            licenseNotExistSentence.AppendFormat(
                "The corresponding license does not exist in the system, please reenter the requested license number:{0}", Environment.NewLine);
            string licenseNumber = i_LicenseNumber;
            if (i_LicenseNumber == string.Empty)
            {
                bool check = outOrExit(out licenseNumber, licenseSentence.ToString());
                if(check == false)
                {
                    return;
                }
            }

            inValidChoiceSentence = new StringBuilder();
            fuelTypeSentence.AppendFormat("Please pick requested fuel type from the following, or press -1 to exit to main menu:{0}", Environment.NewLine);
            for (int i = 0; i < Enum.GetNames(typeof(Resources.eResourceType)).Length - 1; i++)
            {
                optionsSentence.AppendFormat("press {0} for {1}{2}", i, (Resources.eResourceType)i, Environment.NewLine);
            }

            optionsSentence.Append("your choice: ");
            inValidChoiceSentence.AppendFormat("Input is not valid please choose only one of the following options:{0}", Environment.NewLine);
            fuelTypeSentence.Append(optionsSentence);
            inValidChoiceSentence.Append(optionsSentence);
            int decisionNumber = i_DecisionNumber;
            if (i_DecisionNumber == k_MinusOneFlag)
            {
                Console.WriteLine(fuelTypeSentence);
                while(!int.TryParse(Console.ReadLine(), out decisionNumber)
                      || ((decisionNumber < k_MinusOneFlag) || (decisionNumber >= Enum.GetNames(typeof(Resources.eResourceType)).Length - 1)))
                {
                    Console.WriteLine(inValidChoiceSentence);
                }
            }

            if(decisionNumber == k_MinusOneFlag)
            {
                return;
            }

            inValidChoiceSentence = new StringBuilder();
            amountSentence.AppendFormat("Please enter the requested amount (in litters) to fuel or 0 to return to main menu:{0}", Environment.NewLine);
            inValidChoiceSentence.AppendFormat("Something went wrong, please reenter the amount of fuel or 0 to return to main menu:{0}", Environment.NewLine);
            Console.Write(amountSentence);
            float fuelAmount;
            while (!float.TryParse(Console.ReadLine(), out fuelAmount))
            {
                Console.Write(inValidChoiceSentence);
            }

            try
            {
                r_Garage.RefuelAFuelBasedVehicle(licenseNumber, (Resources.eResourceType)decisionNumber, fuelAmount);
                if (fuelAmount != k_ZeroFlag)
                {
                    result.Append("Refuel completed, lets check if you need any more assistance");
                }
                else
                {
                    result.Append("Lets return to main menu and check if you need any other assistance");
                }

                Console.Write(result);
                System.Threading.Thread.Sleep(3 * k_TimeOutPeriod);
            }
            catch(ValueOutOfRangeException error)
            {
                Console.WriteLine(error.Message);
                refuelVehicle(licenseNumber, decisionNumber);
            }
            catch (ArgumentException error)
            {
                Console.WriteLine(error.Message);
                if(error.InnerException == null)
                {
                    refuelVehicle(licenseNumber, k_MinusOneFlag);
                }
                else
                {
                    System.Threading.Thread.Sleep(2 * k_TimeOutPeriod);
                }
            }
        }

        private void chargeElectricVehicle(string i_LicenseNumber)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder licenseSentence = new StringBuilder();
            StringBuilder inValidChoiceSentence = new StringBuilder();
            StringBuilder amountSentence = new StringBuilder();
            licenseSentence.Append("Please enter the license number of the vehicle you want to charge");
            inValidChoiceSentence.Append("Something went wrong, please reenter the requested license number:");
            string licenseNumber = i_LicenseNumber;
            if (i_LicenseNumber == string.Empty)
            {
                bool check = outOrExit(out licenseNumber, licenseSentence.ToString());
                if(check == false)
                {
                    return;
                }

                if(!r_Garage.CheckIfCarIsElectricTypeByLicense(licenseNumber))
                {
                    Console.WriteLine("Sorry but the license number you entered is fuel type");
                    System.Threading.Thread.Sleep(3 * k_TimeOutPeriod);
                    return;
                }
            }

            amountSentence.Append("Please enter the requested amount (in hours) to charge or 0 to return to main menu:");
            inValidChoiceSentence = new StringBuilder();
            inValidChoiceSentence.Append("Something went wrong, please reenter the requested amount (in hours) to charge or 0 to return to main menu:");
            Console.WriteLine(amountSentence);
            float chargeAmount;
            while (!float.TryParse(Console.ReadLine(), out chargeAmount))
            {
                Console.WriteLine(inValidChoiceSentence);
            }

            try
            {
                r_Garage.ChargeAnElectricBasedVehicle(licenseNumber, chargeAmount);
                if(chargeAmount != k_ZeroFlag)
                {
                    result.Append("Charge completed, lets check if you need any more assistance");
                }
                else
                {
                    result.Append("Lets return to main menu and check if you need any other assistance");
                }

                Console.Write(result);
                System.Threading.Thread.Sleep(3 * k_TimeOutPeriod);
            }
            catch (ValueOutOfRangeException error)
            {
                Console.WriteLine(error.Message);
                chargeElectricVehicle(licenseNumber);
            }
        }
       
        private void displayInformation()
        {
            StringBuilder succeed = new StringBuilder();
            StringBuilder licenseSentence = new StringBuilder();
            StringBuilder inValidChoiceSentence = new StringBuilder();
            StringBuilder licenseNotExistSentence = new StringBuilder();
            licenseSentence.Append("Please enter the license number of the vehicle you want to change the status");
            inValidChoiceSentence.AppendFormat("Something went wrong, please reenter the requested license number:{0}", Environment.NewLine);
            licenseNotExistSentence.AppendFormat(
                "The corresponding license does not exist in the system, please reenter the requested license number:{0}", Environment.NewLine);
            bool check = outOrExit(out string licenseNumber, licenseSentence.ToString());
            if (check == false)
            {
                return;
            }

            succeed.Append(r_Garage.GetVehicleData(licenseNumber));
            succeed.Append("Lets check if you need any more assistance ");
            Console.Write(succeed);
            System.Threading.Thread.Sleep(10 * k_TimeOutPeriod);
        }
    }
}
