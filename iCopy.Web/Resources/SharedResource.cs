using Microsoft.Extensions.Localization;
using System;
using System.Reflection;

namespace iCopy.Web.Resources
{
    public class SharedResource
    {
        private readonly IStringLocalizer localizer;

        public SharedResource(IStringLocalizerFactory localizer)
        {
            this.localizer = localizer.Create(nameof(SharedResource), new AssemblyName(typeof(SharedResource).Assembly.FullName).Name);
        }

        public string LocalizedString(string key)
        {
            return localizer[key];

        }

        public string SaveChanges => localizer[nameof(SaveChanges)];
        public string Close => localizer[nameof(Close)];
        public string Name => localizer[nameof(Name)];
        public string ShortName => localizer[nameof(ShortName)];
        public string PhoneCode => localizer[nameof(PhoneCode)];
        public string Active => localizer[nameof(Active)];
        public string Actions => localizer[nameof(Actions)];
        public string Search => localizer[nameof(Search)];
        public string Reset => localizer[nameof(Reset)];
        public string Inactive => localizer[nameof(Inactive)];
        public string All => localizer[nameof(All)];
        public string LocationSettings => localizer[nameof(LocationSettings)];
        public string Countries => localizer[nameof(Countries)];
        public string Yes => localizer[nameof(Yes)];
        public string No => localizer[nameof(No)];
        public string AreYouSure => localizer[nameof(AreYouSure)];
        public string Cancel => localizer[nameof(Cancel)];
        public string PostalCode => localizer[nameof(PostalCode)];
        public string Bosnian => localizer[nameof(Bosnian)];
        public string EnglishUS => localizer[nameof(EnglishUS)];
        public string Privacy => localizer[nameof(Privacy)];
        public string Contact => localizer[nameof(Contact)];
        public string Team => localizer[nameof(Team)];
        public string About => localizer[nameof(About)];
        public string Legal => localizer[nameof(Legal)];
        public string WelcomeToCopierManagementSystem => localizer[nameof(WelcomeToCopierManagementSystem)];
        public string CopierManagementSystem => localizer[nameof(CopierManagementSystem)];
        public string LanguageImagePath => localizer[nameof(LanguageImagePath)];
        public string OR => localizer[nameof(OR)];
        public string Address => localizer[nameof(Address)];
        public string PhoneNumber => localizer[nameof(PhoneNumber)];
        public string Email => localizer[nameof(Email)];
        public string ContactAgent => localizer[nameof(ContactAgent)];
        public string CopierSettings => localizer[nameof(CopierSettings)];
        public string EmployeeSettings => localizer[nameof(EmployeeSettings)];
        public string SystemSettings => localizer[nameof(SystemSettings)];
        public string PhoneNumberRegex => localizer[nameof(PhoneNumberRegex)];
        public string AccountSettings => localizer[nameof(AccountSettings)];
        public string Completed => localizer[nameof(Completed)];
        public string Previous => localizer[nameof(Previous)];
        public string Submit => localizer[nameof(Submit)];
        public string NextStep => localizer[nameof(NextStep)];
        public string Username => localizer[nameof(Username)];
        public string ProfileImage => localizer[nameof(ProfileImage)];
        public string DragFilesOrClickToUpload => localizer[nameof(DragFilesOrClickToUpload)];
        public string Confirm => localizer[nameof(Confirm)];
        public string PasswordConfirm => localizer[nameof(PasswordConfirm)];
        public string CorrectErrorBeforeGoToNextStep => localizer[nameof(CorrectErrorBeforeGoToNextStep)];
        public string ContactEmail => localizer[nameof(ContactEmail)];
        public string Details => localizer[nameof(Details)];
        public string CompanyDetails => localizer[nameof(CompanyDetails)];
        public string CopierDetails => localizer[nameof(CopierDetails)];
        public string EmployeeDetails => localizer[nameof(EmployeeDetails)];
        public string AccountDetails => localizer[nameof(AccountDetails)];
        public string EmailConfirmed => localizer[nameof(EmailConfirmed)];
        public string EmailNotConfirmed => localizer[nameof(EmailNotConfirmed)];
        public string LockoutEnd => localizer[nameof(LockoutEnd)];
        public string Update => localizer[nameof(Update)];
        public string ChangePassword => localizer[nameof(ChangePassword)];
        public string CurrentPassword => localizer[nameof(CurrentPassword)];
        public string NewPassword => localizer[nameof(NewPassword)];
        public string ChangeYourPassword => localizer[nameof(ChangeYourPassword)];
        public string SuccPasswordUpdated => localizer[nameof(SuccPasswordUpdated)];
        public string Settings => localizer[nameof(Settings)];
        public string TwoFactorEnabled => localizer[nameof(TwoFactorEnabled)];
        public string PhoneNumberConfirmed => localizer[nameof(PhoneNumberConfirmed)];
        public string PhoneNumberNotConfirmed => localizer[nameof(PhoneNumberNotConfirmed)];
        public string ChangeProfileImage => localizer[nameof(ChangeProfileImage)];
        public string SuccUserUpdate => localizer[nameof(SuccUserUpdate)];
        public string ErrUpdatePassword => localizer[nameof(ErrUpdatePassword)];
        public string ErrUserUpdate => localizer[nameof(ErrUserUpdate)];
        public string ErrUpdate => localizer[nameof(ErrUpdate)];
        public string ErrAdd => localizer[nameof(ErrAdd)];
        public string Back => localizer[nameof(Back)];
        public string CorrectTheErrors => localizer[nameof(CorrectTheErrors)];
        public string SuccRegister => localizer[nameof(SuccRegister)];
        public string Logout => localizer[nameof(Logout)];
        public string Notifications => localizer[nameof(Notifications)];
        public string OOPSSomethingWentWronghere => localizer[nameof(OOPSSomethingWentWronghere)];
        public string MyProfile => localizer[nameof(MyProfile)];
        public string PrintSettings => localizer[nameof(PrintSettings)];
        public string UploadFile => localizer[nameof(UploadFile)];


        #region SuccMessage
        public string SuccUpdate => localizer[nameof(SuccUpdate)];
        public string SuccAdd => localizer[nameof(SuccAdd)];
        public string SuccDelete => localizer[nameof(SuccDelete)];
        public string SuccChangeStatus => localizer[nameof(SuccChangeStatus)];
        #endregion

        #region ErrorMessage
        public string ErrDelete => localizer[nameof(ErrDelete)];
        public string ErrChangeStatus => localizer[nameof(ErrChangeStatus)];
        #endregion

        #region Country
        public string Country => localizer[nameof(Country)];
        public string AddCountry => localizer[nameof(AddCountry)];
        public string CountrySettings => localizer[nameof(CountrySettings)];
        public string EditCountry => localizer[nameof(EditCountry)];
        public string ChooseCountry => localizer[nameof(ChooseCountry)];
        #endregion

        #region City
        public string City => localizer[nameof(City)];
        public string Cities => localizer[nameof(Cities)];
        public string CitySettings => localizer[nameof(CitySettings)];
        public string AddCity => localizer[nameof(AddCity)];
        public string EditCity => localizer[nameof(EditCity)];
        public string ChooseCity => localizer[nameof(ChooseCity)];
        #endregion

        #region Company
        public string Company => localizer[nameof(Company)];
        public string CompanySettings => localizer[nameof(CompanySettings)];
        public string AddCompany => localizer[nameof(AddCompany)];
        public string JIB => localizer[nameof(JIB)];
        public string ContactAgentPhoneNumber => localizer[nameof(ContactAgentPhoneNumber)];
        public string UpdateCompanyDetails => localizer[nameof(UpdateCompanyDetails)];
        public string ChooseCompany => localizer[nameof(ChooseCompany)];

        #endregion

        #region Copier
        public string Copier => localizer[nameof(Copier)];
        public string Description => localizer[nameof(Description)];
        public string StartWorkingTime => localizer[nameof(StartWorkingTime)];
        public string EndWorkingTime => localizer[nameof(EndWorkingTime)];
        public string Url => localizer[nameof(Url)];
        public string AddCopier => localizer[nameof(AddCopier)];
        public string UpdateCopierDetails => localizer[nameof(UpdateCopierDetails)];
        public string ChooseCopier => localizer[nameof(ChooseCopier)];
        #endregion

        #region Login
        public string Login => localizer[nameof(Login)];
        public string ForgotPassword => localizer[nameof(ForgotPassword)];
        public string UsernameOrEmail => localizer[nameof(UsernameOrEmail)];
        public string Password => localizer[nameof(Password)];
        public string SignUp => localizer[nameof(SignUp)];
        public string Registration => localizer[nameof(Registration)];

        #endregion

        #region Person
        public string FirstName => localizer[nameof(FirstName)];
        public string LastName => localizer[nameof(LastName)];
        public string MiddleName => localizer[nameof(MiddleName)];
        public string Gender => localizer[nameof(Gender)];
        public string BirthDate => localizer[nameof(BirthDate)];
        public string ChooseGender => localizer[nameof(ChooseGender)];

        #endregion

        #region Employee
        public string AddEmployee => localizer[nameof(AddEmployee)];
        public string UpdateEmployeeDetails => localizer[nameof(UpdateEmployeeDetails)];
        #endregion

        #region User

        public string UserSettings => localizer[nameof(UserSettings)];
        public string Enabled => localizer[nameof(Enabled)];
        public string Disabled => localizer[nameof(Disabled)];
        public string UserAccountActivated => localizer[nameof(UserAccountActivated)];
        public string UserAccountNotActivated => localizer[nameof(UserAccountNotActivated)];

        #endregion

        #region Dashboard
        public string Dashboard => localizer[nameof(Dashboard)];
        #endregion

        #region PrintRequest
        public string PrintRequest => localizer[nameof(PrintRequest)];
        public string PrintRequestSettings => localizer[nameof(PrintRequestSettings)];
        public string AddPrintRequest => localizer[nameof(AddPrintRequest)];
        public string FilePath => localizer[nameof(FilePath)];
        public string File => localizer[nameof(File)];
        #endregion

        #region PrintPagesOptions
        public string PrintPagesOptions => localizer[nameof(PrintPagesOptions)];
        public string ChoosePrintOptions => localizer[nameof(ChoosePrintOptions)];

        #endregion

        #region SidePrintOption
        public string SidePrintOption => localizer[nameof(SidePrintOption)];
        public string ChooseSidePrintOptions => localizer[nameof(ChooseSidePrintOptions)];
        #endregion

        #region Orientation
        public string Orientation => localizer[nameof(Orientation)];
        public string ChooseOrientation => localizer[nameof(ChooseOrientation)];
        #endregion

        #region Letter
        public string Letter => localizer[nameof(Letter)];
        public string ChooseLetter => localizer[nameof(ChooseLetter)];
        #endregion

        #region PagePerSheet
        public string PagePerSheet => localizer[nameof(PagePerSheet)];
        public string ChoosePagePerSheet => localizer[nameof(ChoosePagePerSheet)];
        #endregion

        #region CollatedPrintOptions
        public string CollatedPrintOptions => localizer[nameof(CollatedPrintOptions)];
        public string ChooseCollatedPrintOptions => localizer[nameof(ChooseCollatedPrintOptions)];
        #endregion

        #region Status
        public string Status => localizer[nameof(Status)];
        public string ChooseStatus => localizer[nameof(ChooseStatus)];
        #endregion
    }
}
