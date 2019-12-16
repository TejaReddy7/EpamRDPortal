﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EPAM.RDPreEducationPortal.Web.Site.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EpamRDPreEducationEntities : DbContext
    {
        public EpamRDPreEducationEntities()
            : base("name=EpamRDPreEducationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AttendeeDetail> AttendeeDetails { get; set; }
        public virtual DbSet<College> Colleges { get; set; }
        public virtual DbSet<EmailRecipient> EmailRecipients { get; set; }
        public virtual DbSet<Master_Gender> Master_Gender { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Sonar_Issues> Sonar_Issues { get; set; }
        public virtual DbSet<Student_GitRepositories> Student_GitRepositories { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User_Role> User_Role { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Webinar> Webinars { get; set; }
        public virtual DbSet<Webinar_Registrants> Webinar_Registrants { get; set; }
        public virtual DbSet<Webinar_Registration> Webinar_Registration { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Candidate_Details> Candidate_Details { get; set; }
        public virtual DbSet<HRAssessment> HRAssessments { get; set; }
        public virtual DbSet<TechnicalAssessment> TechnicalAssessments { get; set; }
        public virtual DbSet<Master_Locations> Master_Locations { get; set; }
        public virtual DbSet<Master_Roles> Master_Roles { get; set; }
        public virtual DbSet<Recruitment_Rounds> Recruitment_Rounds { get; set; }
        public virtual DbSet<Recruitment> Recruitments { get; set; }
        public virtual DbSet<TestsHosted> TestsHosteds { get; set; }
        public virtual DbSet<Candidate_Marks> Candidate_Marks { get; set; }
    
        public virtual int Delete_Student_GitRepositories(Nullable<int> gitRepositoryID)
        {
            var gitRepositoryIDParameter = gitRepositoryID.HasValue ?
                new ObjectParameter("GitRepositoryID", gitRepositoryID) :
                new ObjectParameter("GitRepositoryID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Student_GitRepositories", gitRepositoryIDParameter);
        }
    
        public virtual int Generate_GetScriptWithPK(string table_Name, string primary_Key)
        {
            var table_NameParameter = table_Name != null ?
                new ObjectParameter("Table_Name", table_Name) :
                new ObjectParameter("Table_Name", typeof(string));
    
            var primary_KeyParameter = primary_Key != null ?
                new ObjectParameter("Primary_Key", primary_Key) :
                new ObjectParameter("Primary_Key", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Generate_GetScriptWithPK", table_NameParameter, primary_KeyParameter);
        }
    
        public virtual int Generate_UpdateScriptWithPK(string table_Name, string primary_Key)
        {
            var table_NameParameter = table_Name != null ?
                new ObjectParameter("Table_Name", table_Name) :
                new ObjectParameter("Table_Name", typeof(string));
    
            var primary_KeyParameter = primary_Key != null ?
                new ObjectParameter("Primary_Key", primary_Key) :
                new ObjectParameter("Primary_Key", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Generate_UpdateScriptWithPK", table_NameParameter, primary_KeyParameter);
        }
    
        public virtual int Generate_WebinarReportCollegewise()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Generate_WebinarReportCollegewise");
        }
    
        public virtual ObjectResult<Get_Colleges_Result> Get_Colleges(Nullable<int> collegeID)
        {
            var collegeIDParameter = collegeID.HasValue ?
                new ObjectParameter("CollegeID", collegeID) :
                new ObjectParameter("CollegeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Colleges_Result>("Get_Colleges", collegeIDParameter);
        }
    
        public virtual ObjectResult<GET_Student_GitRepositories_Result> GET_Student_GitRepositories(Nullable<int> gitRepositoryID)
        {
            var gitRepositoryIDParameter = gitRepositoryID.HasValue ?
                new ObjectParameter("GitRepositoryID", gitRepositoryID) :
                new ObjectParameter("GitRepositoryID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GET_Student_GitRepositories_Result>("GET_Student_GitRepositories", gitRepositoryIDParameter);
        }
    
        public virtual ObjectResult<Get_UserDetails_SonarReport_Result> Get_UserDetails_SonarReport(Nullable<int> gitRepositoryID)
        {
            var gitRepositoryIDParameter = gitRepositoryID.HasValue ?
                new ObjectParameter("GitRepositoryID", gitRepositoryID) :
                new ObjectParameter("GitRepositoryID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_UserDetails_SonarReport_Result>("Get_UserDetails_SonarReport", gitRepositoryIDParameter);
        }
    
        public virtual ObjectResult<Get_UsersOnUserNameAndPassword_Result> Get_UsersOnUserNameAndPassword(string userName, string password)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_UsersOnUserNameAndPassword_Result>("Get_UsersOnUserNameAndPassword", userNameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<Get_WebinarAbsentees_Result> Get_WebinarAbsentees(string webinarDate)
        {
            var webinarDateParameter = webinarDate != null ?
                new ObjectParameter("WebinarDate", webinarDate) :
                new ObjectParameter("WebinarDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_WebinarAbsentees_Result>("Get_WebinarAbsentees", webinarDateParameter);
        }
    
        public virtual ObjectResult<Get_WebinarAbsenteesCollegewise_Result> Get_WebinarAbsenteesCollegewise(string webinarDate, Nullable<int> collegeId)
        {
            var webinarDateParameter = webinarDate != null ?
                new ObjectParameter("WebinarDate", webinarDate) :
                new ObjectParameter("WebinarDate", typeof(string));
    
            var collegeIdParameter = collegeId.HasValue ?
                new ObjectParameter("CollegeId", collegeId) :
                new ObjectParameter("CollegeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_WebinarAbsenteesCollegewise_Result>("Get_WebinarAbsenteesCollegewise", webinarDateParameter, collegeIdParameter);
        }
    
        public virtual int Get_webinarnonattendees(string webinarDate)
        {
            var webinarDateParameter = webinarDate != null ?
                new ObjectParameter("WebinarDate", webinarDate) :
                new ObjectParameter("WebinarDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Get_webinarnonattendees", webinarDateParameter);
        }
    
        public virtual int RandChars(Nullable<int> len, Nullable<byte> min, Nullable<byte> range, string exclude, ObjectParameter output)
        {
            var lenParameter = len.HasValue ?
                new ObjectParameter("len", len) :
                new ObjectParameter("len", typeof(int));
    
            var minParameter = min.HasValue ?
                new ObjectParameter("min", min) :
                new ObjectParameter("min", typeof(byte));
    
            var rangeParameter = range.HasValue ?
                new ObjectParameter("range", range) :
                new ObjectParameter("range", typeof(byte));
    
            var excludeParameter = exclude != null ?
                new ObjectParameter("exclude", exclude) :
                new ObjectParameter("exclude", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RandChars", lenParameter, minParameter, rangeParameter, excludeParameter, output);
        }
    
        public virtual int Report_WebinarAttendance_Collegewise()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Report_WebinarAttendance_Collegewise");
        }
    
        public virtual ObjectResult<Report_WebinarAttendance_Studentwise_Result> Report_WebinarAttendance_Studentwise()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Report_WebinarAttendance_Studentwise_Result>("Report_WebinarAttendance_Studentwise");
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        [DbFunction("EpamRDPreEducationEntities", "Split")]
        public virtual IQueryable<Split_Result> Split(string @string, string delimiter)
        {
            var stringParameter = @string != null ?
                new ObjectParameter("String", @string) :
                new ObjectParameter("String", typeof(string));
    
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("Delimiter", delimiter) :
                new ObjectParameter("Delimiter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Split_Result>("[EpamRDPreEducationEntities].[Split](@String, @Delimiter)", stringParameter, delimiterParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Update_AttendeeDetails(ObjectParameter attendeeID, Nullable<int> studentID, Nullable<int> webinarID, string attended, Nullable<int> interestRating, string lastName, string firstName, string emailAddress, string registrationDateTime, string joinTimeLeaveTimeTimeinSession, string timeinSession, string unsubscribed, string who)
        {
            var studentIDParameter = studentID.HasValue ?
                new ObjectParameter("StudentID", studentID) :
                new ObjectParameter("StudentID", typeof(int));
    
            var webinarIDParameter = webinarID.HasValue ?
                new ObjectParameter("WebinarID", webinarID) :
                new ObjectParameter("WebinarID", typeof(int));
    
            var attendedParameter = attended != null ?
                new ObjectParameter("Attended", attended) :
                new ObjectParameter("Attended", typeof(string));
    
            var interestRatingParameter = interestRating.HasValue ?
                new ObjectParameter("InterestRating", interestRating) :
                new ObjectParameter("InterestRating", typeof(int));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var emailAddressParameter = emailAddress != null ?
                new ObjectParameter("EmailAddress", emailAddress) :
                new ObjectParameter("EmailAddress", typeof(string));
    
            var registrationDateTimeParameter = registrationDateTime != null ?
                new ObjectParameter("RegistrationDateTime", registrationDateTime) :
                new ObjectParameter("RegistrationDateTime", typeof(string));
    
            var joinTimeLeaveTimeTimeinSessionParameter = joinTimeLeaveTimeTimeinSession != null ?
                new ObjectParameter("JoinTimeLeaveTimeTimeinSession", joinTimeLeaveTimeTimeinSession) :
                new ObjectParameter("JoinTimeLeaveTimeTimeinSession", typeof(string));
    
            var timeinSessionParameter = timeinSession != null ?
                new ObjectParameter("TimeinSession", timeinSession) :
                new ObjectParameter("TimeinSession", typeof(string));
    
            var unsubscribedParameter = unsubscribed != null ?
                new ObjectParameter("Unsubscribed", unsubscribed) :
                new ObjectParameter("Unsubscribed", typeof(string));
    
            var whoParameter = who != null ?
                new ObjectParameter("Who", who) :
                new ObjectParameter("Who", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Update_AttendeeDetails", attendeeID, studentIDParameter, webinarIDParameter, attendedParameter, interestRatingParameter, lastNameParameter, firstNameParameter, emailAddressParameter, registrationDateTimeParameter, joinTimeLeaveTimeTimeinSessionParameter, timeinSessionParameter, unsubscribedParameter, whoParameter);
        }
    
        public virtual int Update_GitRepositories_EmailStatus(ObjectParameter gitRepositoryID)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_GitRepositories_EmailStatus", gitRepositoryID);
        }
    
        public virtual ObjectResult<Nullable<int>> Update_Sonar_Issues(ObjectParameter sonarIssueID, Nullable<int> gitRepositoryID, string key, string rule, string severity, string component, string project, string line, string hash, string status, string message, string effort, string debt, string author, string creationDate, string updateDate, string organization, string who)
        {
            var gitRepositoryIDParameter = gitRepositoryID.HasValue ?
                new ObjectParameter("GitRepositoryID", gitRepositoryID) :
                new ObjectParameter("GitRepositoryID", typeof(int));
    
            var keyParameter = key != null ?
                new ObjectParameter("Key", key) :
                new ObjectParameter("Key", typeof(string));
    
            var ruleParameter = rule != null ?
                new ObjectParameter("Rule", rule) :
                new ObjectParameter("Rule", typeof(string));
    
            var severityParameter = severity != null ?
                new ObjectParameter("Severity", severity) :
                new ObjectParameter("Severity", typeof(string));
    
            var componentParameter = component != null ?
                new ObjectParameter("Component", component) :
                new ObjectParameter("Component", typeof(string));
    
            var projectParameter = project != null ?
                new ObjectParameter("Project", project) :
                new ObjectParameter("Project", typeof(string));
    
            var lineParameter = line != null ?
                new ObjectParameter("Line", line) :
                new ObjectParameter("Line", typeof(string));
    
            var hashParameter = hash != null ?
                new ObjectParameter("Hash", hash) :
                new ObjectParameter("Hash", typeof(string));
    
            var statusParameter = status != null ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(string));
    
            var messageParameter = message != null ?
                new ObjectParameter("Message", message) :
                new ObjectParameter("Message", typeof(string));
    
            var effortParameter = effort != null ?
                new ObjectParameter("Effort", effort) :
                new ObjectParameter("Effort", typeof(string));
    
            var debtParameter = debt != null ?
                new ObjectParameter("Debt", debt) :
                new ObjectParameter("Debt", typeof(string));
    
            var authorParameter = author != null ?
                new ObjectParameter("Author", author) :
                new ObjectParameter("Author", typeof(string));
    
            var creationDateParameter = creationDate != null ?
                new ObjectParameter("CreationDate", creationDate) :
                new ObjectParameter("CreationDate", typeof(string));
    
            var updateDateParameter = updateDate != null ?
                new ObjectParameter("UpdateDate", updateDate) :
                new ObjectParameter("UpdateDate", typeof(string));
    
            var organizationParameter = organization != null ?
                new ObjectParameter("Organization", organization) :
                new ObjectParameter("Organization", typeof(string));
    
            var whoParameter = who != null ?
                new ObjectParameter("Who", who) :
                new ObjectParameter("Who", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Update_Sonar_Issues", sonarIssueID, gitRepositoryIDParameter, keyParameter, ruleParameter, severityParameter, componentParameter, projectParameter, lineParameter, hashParameter, statusParameter, messageParameter, effortParameter, debtParameter, authorParameter, creationDateParameter, updateDateParameter, organizationParameter, whoParameter);
        }
    
        public virtual int Update_Student_GitRepositories(ObjectParameter gitRepositoryID, Nullable<int> studentID, string taskName, string taskDescription, string repositoryUrl, Nullable<bool> status, string who)
        {
            var studentIDParameter = studentID.HasValue ?
                new ObjectParameter("StudentID", studentID) :
                new ObjectParameter("StudentID", typeof(int));
    
            var taskNameParameter = taskName != null ?
                new ObjectParameter("TaskName", taskName) :
                new ObjectParameter("TaskName", typeof(string));
    
            var taskDescriptionParameter = taskDescription != null ?
                new ObjectParameter("TaskDescription", taskDescription) :
                new ObjectParameter("TaskDescription", typeof(string));
    
            var repositoryUrlParameter = repositoryUrl != null ?
                new ObjectParameter("RepositoryUrl", repositoryUrl) :
                new ObjectParameter("RepositoryUrl", typeof(string));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(bool));
    
            var whoParameter = who != null ?
                new ObjectParameter("Who", who) :
                new ObjectParameter("Who", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_Student_GitRepositories", gitRepositoryID, studentIDParameter, taskNameParameter, taskDescriptionParameter, repositoryUrlParameter, statusParameter, whoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Update_Users(ObjectParameter userId, string userName, string firstName, string gender, string email, string contactNumber, string address, string college, Nullable<int> marksScored, string graduationSpecialty, string who)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var genderParameter = gender != null ?
                new ObjectParameter("Gender", gender) :
                new ObjectParameter("Gender", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var contactNumberParameter = contactNumber != null ?
                new ObjectParameter("ContactNumber", contactNumber) :
                new ObjectParameter("ContactNumber", typeof(string));
    
            var addressParameter = address != null ?
                new ObjectParameter("Address", address) :
                new ObjectParameter("Address", typeof(string));
    
            var collegeParameter = college != null ?
                new ObjectParameter("College", college) :
                new ObjectParameter("College", typeof(string));
    
            var marksScoredParameter = marksScored.HasValue ?
                new ObjectParameter("MarksScored", marksScored) :
                new ObjectParameter("MarksScored", typeof(int));
    
            var graduationSpecialtyParameter = graduationSpecialty != null ?
                new ObjectParameter("GraduationSpecialty", graduationSpecialty) :
                new ObjectParameter("GraduationSpecialty", typeof(string));
    
            var whoParameter = who != null ?
                new ObjectParameter("Who", who) :
                new ObjectParameter("Who", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Update_Users", userId, userNameParameter, firstNameParameter, genderParameter, emailParameter, contactNumberParameter, addressParameter, collegeParameter, marksScoredParameter, graduationSpecialtyParameter, whoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Update_Webinar(ObjectParameter webinarID, string actualStartDate, string duration, Nullable<int> registered, Nullable<int> attended, Nullable<int> clickedRegistrationLink, Nullable<int> openedInvitation)
        {
            var actualStartDateParameter = actualStartDate != null ?
                new ObjectParameter("ActualStartDate", actualStartDate) :
                new ObjectParameter("ActualStartDate", typeof(string));
    
            var durationParameter = duration != null ?
                new ObjectParameter("Duration", duration) :
                new ObjectParameter("Duration", typeof(string));
    
            var registeredParameter = registered.HasValue ?
                new ObjectParameter("Registered", registered) :
                new ObjectParameter("Registered", typeof(int));
    
            var attendedParameter = attended.HasValue ?
                new ObjectParameter("Attended", attended) :
                new ObjectParameter("Attended", typeof(int));
    
            var clickedRegistrationLinkParameter = clickedRegistrationLink.HasValue ?
                new ObjectParameter("ClickedRegistrationLink", clickedRegistrationLink) :
                new ObjectParameter("ClickedRegistrationLink", typeof(int));
    
            var openedInvitationParameter = openedInvitation.HasValue ?
                new ObjectParameter("OpenedInvitation", openedInvitation) :
                new ObjectParameter("OpenedInvitation", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Update_Webinar", webinarID, actualStartDateParameter, durationParameter, registeredParameter, attendedParameter, clickedRegistrationLinkParameter, openedInvitationParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Update_Webinar_Registrants(ObjectParameter registrantId, Nullable<int> webinarID, string firstName, string lastName, string email, Nullable<System.DateTime> registrationDate, Nullable<bool> unsubscribed, string who)
        {
            var webinarIDParameter = webinarID.HasValue ?
                new ObjectParameter("WebinarID", webinarID) :
                new ObjectParameter("WebinarID", typeof(int));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var registrationDateParameter = registrationDate.HasValue ?
                new ObjectParameter("RegistrationDate", registrationDate) :
                new ObjectParameter("RegistrationDate", typeof(System.DateTime));
    
            var unsubscribedParameter = unsubscribed.HasValue ?
                new ObjectParameter("Unsubscribed", unsubscribed) :
                new ObjectParameter("Unsubscribed", typeof(bool));
    
            var whoParameter = who != null ?
                new ObjectParameter("Who", who) :
                new ObjectParameter("Who", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Update_Webinar_Registrants", registrantId, webinarIDParameter, firstNameParameter, lastNameParameter, emailParameter, registrationDateParameter, unsubscribedParameter, whoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Update_Webinar_Registration(ObjectParameter iD, string webinarID, string webinarName, Nullable<System.DateTime> scheduledStartDate, Nullable<int> registered, Nullable<System.TimeSpan> scheduledStartTime, Nullable<int> scheduledDurationMinutes, Nullable<int> clickedRegistrationLink, string who)
        {
            var webinarIDParameter = webinarID != null ?
                new ObjectParameter("WebinarID", webinarID) :
                new ObjectParameter("WebinarID", typeof(string));
    
            var webinarNameParameter = webinarName != null ?
                new ObjectParameter("WebinarName", webinarName) :
                new ObjectParameter("WebinarName", typeof(string));
    
            var scheduledStartDateParameter = scheduledStartDate.HasValue ?
                new ObjectParameter("ScheduledStartDate", scheduledStartDate) :
                new ObjectParameter("ScheduledStartDate", typeof(System.DateTime));
    
            var registeredParameter = registered.HasValue ?
                new ObjectParameter("Registered", registered) :
                new ObjectParameter("Registered", typeof(int));
    
            var scheduledStartTimeParameter = scheduledStartTime.HasValue ?
                new ObjectParameter("ScheduledStartTime", scheduledStartTime) :
                new ObjectParameter("ScheduledStartTime", typeof(System.TimeSpan));
    
            var scheduledDurationMinutesParameter = scheduledDurationMinutes.HasValue ?
                new ObjectParameter("ScheduledDurationMinutes", scheduledDurationMinutes) :
                new ObjectParameter("ScheduledDurationMinutes", typeof(int));
    
            var clickedRegistrationLinkParameter = clickedRegistrationLink.HasValue ?
                new ObjectParameter("ClickedRegistrationLink", clickedRegistrationLink) :
                new ObjectParameter("ClickedRegistrationLink", typeof(int));
    
            var whoParameter = who != null ?
                new ObjectParameter("Who", who) :
                new ObjectParameter("Who", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Update_Webinar_Registration", iD, webinarIDParameter, webinarNameParameter, scheduledStartDateParameter, registeredParameter, scheduledStartTimeParameter, scheduledDurationMinutesParameter, clickedRegistrationLinkParameter, whoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> ValidateUser(string userName, string password)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("ValidateUser", userNameParameter, passwordParameter);
        }
    
        public virtual int Get_RecruitmentReport(Nullable<int> recruitmentId, string roundType)
        {
            var recruitmentIdParameter = recruitmentId.HasValue ?
                new ObjectParameter("RecruitmentId", recruitmentId) :
                new ObjectParameter("RecruitmentId", typeof(int));
    
            var roundTypeParameter = roundType != null ?
                new ObjectParameter("RoundType", roundType) :
                new ObjectParameter("RoundType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Get_RecruitmentReport", recruitmentIdParameter, roundTypeParameter);
        }
    
        public virtual ObjectResult<Get_Candidate_Details_Result> Get_Candidate_Details(Nullable<int> testId, string role)
        {
            var testIdParameter = testId.HasValue ?
                new ObjectParameter("TestId", testId) :
                new ObjectParameter("TestId", typeof(int));
    
            var roleParameter = role != null ?
                new ObjectParameter("Role", role) :
                new ObjectParameter("Role", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Candidate_Details_Result>("Get_Candidate_Details", testIdParameter, roleParameter);
        }
    
        public virtual ObjectResult<Get_Candidate_Details_HREligible_Result> Get_Candidate_Details_HREligible(Nullable<int> testId)
        {
            var testIdParameter = testId.HasValue ?
                new ObjectParameter("TestId", testId) :
                new ObjectParameter("TestId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Candidate_Details_HREligible_Result>("Get_Candidate_Details_HREligible", testIdParameter);
        }
    
        public virtual ObjectResult<Report_RecruitmentSummary_Result> Report_RecruitmentSummary(Nullable<int> recruitmentid)
        {
            var recruitmentidParameter = recruitmentid.HasValue ?
                new ObjectParameter("Recruitmentid", recruitmentid) :
                new ObjectParameter("Recruitmentid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Report_RecruitmentSummary_Result>("Report_RecruitmentSummary", recruitmentidParameter);
        }
    
        public virtual ObjectResult<Report_RecruitmentSummary_Genderwise_Result> Report_RecruitmentSummary_Genderwise(Nullable<int> recruitmentid)
        {
            var recruitmentidParameter = recruitmentid.HasValue ?
                new ObjectParameter("Recruitmentid", recruitmentid) :
                new ObjectParameter("Recruitmentid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Report_RecruitmentSummary_Genderwise_Result>("Report_RecruitmentSummary_Genderwise", recruitmentidParameter);
        }
    }
}