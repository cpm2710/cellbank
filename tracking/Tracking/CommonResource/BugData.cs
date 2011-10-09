using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CommonResource
{
    /// <summary>
    /// Bug Fields
    /// </summary>
    public enum BugFields
    {
        /// <summary>
        /// The Team Id where the bug data is fetched (Product Studio etc.)
        /// </summary>
        TeamId,

        /// <summary>
        /// BUG Id
        /// </summary>
        BugId,

        /// <summary>
        /// Description
        /// </summary>
        Description,

        /// <summary>
        /// Dev Resolution Notes
        /// </summary>
        DevResolutionNotes,

        /// <summary>
        /// KB number
        /// </summary>
        KbNumber,

        /// <summary>
        /// QFE triage status: Approved/Need Review/Investigate etc
        /// </summary>
        QfeTriageStatus,

        /// <summary>
        /// PSS
        /// </summary>
        PSS,

        /// <summary>
        /// Product the bug belongs to
        /// </summary>
        Product,

        /// <summary>
        /// The date when the BUG is accepted
        /// </summary>
        QfeAcceptDate,

        /// <summary>
        /// Check-in submission date
        /// </summary>
        CheckinSubmitDate,

        /// <summary>
        /// The date when builder finishes build and assign the bug back to Dev
        /// </summary>
        BuildFinishDate,

        /// <summary>
        /// Private tester
        /// </summary>
        PrivateTester,

        /// <summary>
        /// BUG Release Date
        /// </summary>
        ReleaseDate,

        /// <summary>
        /// Affected Binaries
        /// </summary>
        Binaries,

        /// <summary>
        /// Customer Verification Finish Date
        /// </summary>
        CustomerVerifyFinishDate,

        /// <summary>
        /// QFE Status: Awaiting PSS/BVT Pending/Closed/Core Review etc
        /// </summary>
        QfeStatus,

        /// <summary>
        /// Who found the BUG
        /// </summary>
        Customer,

        /// <summary>
        /// PM owner
        /// </summary>
        PMOwner,

        /// <summary>
        /// Developer
        /// </summary>
        Developer,

        /// <summary>
        /// BUG title
        /// </summary>
        Title,

        /// <summary>
        /// BUG status: Active, Resolved, Closed
        /// </summary>
        Status,

        /// <summary>
        /// Changed By
        /// </summary>
        ChangedBy,

        /// <summary>
        /// Resolved date
        /// </summary>
        ResolvedDate,

        /// <summary>
        /// Resolved by
        /// </summary>
        Resolver,

        /// <summary>
        /// Resolution: Fixed, External, Won't Fix, By Design
        /// </summary>
        Resolution,

        /// <summary>
        /// Bug Owner (Assign to)
        /// </summary>
        BugOwner,

        /// <summary>
        /// Issue Type (RFC/RFH/CDCR)
        /// </summary>
        IssueType,

        /// <summary>
        /// BUG severity
        /// </summary>
        Severity,

        /// <summary>
        /// The guy who closed the bug
        /// </summary>
        ClosedBy,

        /// <summary>
        /// BUG priority
        /// </summary>
        Priority,

        /// <summary>
        /// The time when the bug is opened
        /// </summary>
        BugOpenedDate,

        /// <summary>
        /// BUG Filer
        /// </summary>
        BugFiler,

        /// <summary>
        /// Cause
        /// </summary>
        Cause,

        /// <summary>
        /// RFx Close Date
        /// </summary>
        CloseDate,

        /// <summary>
        /// bug's last modify time 
        /// </summary>
        LastModifiedTime,

        /// <summary>
        /// Feature
        /// </summary>
        Feature,

        /// <summary>
        /// Fix ETA
        /// </summary>
        FixETA,

        /// <summary>
        /// Must Fix By Date
        /// </summary>
        MustFixBy,

        /// <summary>
        /// The date when the official bundle is created
        /// </summary>
        BundleCreateDate,

        /// <summary>
        /// Test Reviewer
        /// </summary>
        TestReviewer,

        /// <summary>
        /// SLA date
        /// </summary>
        SLADate,

        /// <summary>
        /// Work Item State
        /// </summary>
        WorkItemState,

        /// <summary>
        /// QFE Assigned Date
        /// </summary>
        QfeAssignedDate,

        /// <summary>
        /// The Path bug is located at
        /// </summary>
        Path,

        /// <summary>
        /// The product build in which a bug was found
        /// </summary>
        OpenedBuild,

        /// <summary>
        /// The product build in which a bug was fixed
        /// </summary>
        ResolvedBuild,

        /// <summary>
        /// Dev Owner
        /// </summary>
        DevOwner,

        /// <summary>
        /// Test Owner
        /// </summary>
        TestOwner,

        /// <summary>
        /// Build Owner
        /// </summary>
        BuildOwner,

        /// <summary>
        /// Official builder
        /// </summary>
        Builder,

        /// <summary>
        /// Release PM
        /// </summary>
        ReleasePM,

        /// <summary>
        /// The date when the BUG is rejected
        /// </summary>
        QfeRejectDate,

        /// <summary>
        /// The date when the BUG is approved to investigate
        /// </summary>
        InvestigationAssignDate,

        /// <summary>
        /// Investigation SLA
        /// </summary>
        InvestigationDue,

        /// <summary>
        /// The date when the investigation is finished
        /// </summary>
        InvestigationFinishDate,

        /// <summary>
        /// Code Review request date
        /// </summary>
        CodeReviewRequestDate,

        /// <summary>
        /// Code Review Status
        /// </summary>
        CodeReviewStatus,

        /// <summary>
        /// Code Reviewer
        /// </summary>
        CodeReviewer,

        /// <summary>
        /// Code Review File
        /// </summary>
        CodeReviewFile,

        /// <summary>
        /// Code Review Finish Date
        /// </summary>
        CodeReviewFinishDate,

        /// <summary>
        /// Test Case Info
        /// </summary>
        TestCase,

        /// <summary>
        /// The date when private test is assigned to test
        /// </summary>
        PrivateTestRequestDate,

        /// <summary>
        /// Private test Status: Available/Passed/Failed
        /// </summary>
        PrivateTestStatus,

        /// <summary>
        /// The date the private test is finished 
        /// </summary>
        PrivateTestFinishDate,

        /// <summary>
        /// Check-in request date
        /// </summary>
        CheckinRequestDate,

        /// <summary>
        /// Check-in status: Requested/Approved/Rejected/Submitted
        /// </summary>
        CheckinStatus,

        /// <summary>
        /// Check-in approved date
        /// </summary>
        CheckinApproveDate,

        /// <summary>
        /// The date when the bug is assigned to builder
        /// </summary>
        BuildRequestDate,

        /// <summary>
        /// The date the official bundle is assigned to BVT 
        /// </summary>
        BVTAssignDate,

        /// <summary>
        /// BVT status: Assigned/Passed/Failed
        /// </summary>
        BVTStatus,

        /// <summary>
        /// BVT tester
        /// </summary>
        BVTTester,

        /// <summary>
        /// The date when BVT is finished
        /// </summary>
        BVTFinishDate,

        /// <summary>
        /// The date when the bug is assigned to regression test
        /// </summary>
        RegressionAssignDate,

        /// <summary>
        /// Regression Test status: Assigned/Passed/Failed
        /// </summary>
        RegressionStatus,

        /// <summary>
        /// Regerssion Tester
        /// </summary>
        RegressionTester,

        /// <summary>
        /// Regression Test Finish Date
        /// </summary>
        RegressionFinishDate,

        /// <summary>
        /// Code Sign Request Date
        /// </summary>
        CodeSignRequestDate,

        /// <summary>
        /// Code Sign Finish Date
        /// </summary>
        CodeSignFinishDate,

        /// <summary>
        /// Code Sign Request Number
        /// </summary>
        CodeSignRequestNumber,

        /// <summary>
        /// Rfx Submitted
        /// </summary>
        RfxSubmitted,

        /// <summary>
        /// Pending Changelist of this bug
        /// </summary>
        PendingChangelist,

        /// <summary>
        /// Security Rank of the bug
        /// </summary>
        SecurityRank,

        /// <summary>
        /// Security Effect of the bug
        /// </summary>
        SecurityEffect,

        /// <summary>
        /// Who approved triaged the bug 
        /// </summary>
        ApprovedBy,

        /// <summary>
        /// Milestone for the bug
        /// </summary>
        Milestone,

        /// <summary>
        /// Triage date for bug
        /// </summary>
        RFxTriagedDate,

        /// <summary>
        /// Investigation complete date for bug
        /// </summary>
        InvestigationCompleteDate
    }

    /// <summary>
    /// String Field
    /// </summary>
    [DataContract]
    public class StringField
    {
        #region Properties

        private BugFields fieldName;

        /// <summary>
        /// Filed Name
        /// </summary>
        [DataMember]
        public BugFields FieldName
        {
            get { return this.fieldName; }
            set { this.fieldName = value; }
        }

        private String fieldValue;

        /// <summary>
        /// Field Value
        /// </summary>
        [DataMember]
        public String FieldValue
        {
            get { return this.fieldValue; }
            set { this.fieldValue = value; }
        }

        #endregion Properties
    }

    /// <summary>
    /// Integer Field
    /// </summary>
    [DataContract]
    public class IntegerField
    {
        #region Properties

        /// <summary>
        /// Filed Name
        /// </summary>
        [DataMember]
        public BugFields FieldName { get; set; }

        /// <summary>
        /// Field Value
        /// </summary>
        [DataMember]
        public Int32 FieldValue { get; set; }

        #endregion Properties
    }

    /// <summary>
    /// DateTime Field
    /// </summary>
    [DataContract]
    public class DatetimeField
    {
        #region Properties

        /// <summary>
        /// Filed Name
        /// </summary>
        [DataMember]
        public BugFields FieldName { get; set; }

        /// <summary>
        /// Field Value
        /// </summary>
        [DataMember]
        public DateTime? FieldValue { get; set; }

        #endregion Properties

    }

    /// <summary>
    /// PendingChangelist Field
    /// </summary>
    [DataContract]
    public class PendingChangelistField
    {
        #region Properties

        /// <summary>
        /// Filed Name
        /// </summary>
        [DataMember]
        public BugFields FieldName { get; set; }

        /// <summary>
        /// Field Value
        /// </summary>
        [DataMember]
        public ChangelistInfo FieldValue { get; set; }

        #endregion Properties

    }

    /// <summary>
    /// DataContract
    /// </summary>
    [DataContract]
    public class BugData
    {
        #region Properties

        /// <summary>
        /// The Team Id where the bug data is fetched (Product Studio etc.)
        /// </summary>
        [DataMember]
        public IntegerField TeamId { get; set; }

        /// <summary>
        /// BUG Id
        /// </summary>
        [DataMember]
        public IntegerField BugId { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [DataMember]
        public StringField Description { get; set; }

        /// <summary>
        /// Dev Resolution Notes
        /// </summary>
        [DataMember]
        public StringField DevResolutionNotes { get; set; }

        /// <summary>
        /// KB number
        /// </summary>
        [DataMember]
        public StringField KbNumber { get; set; }

        /// <summary>
        /// QFE triage status: Approved/Need Review/Investigate etc
        /// </summary>
        [DataMember]
        public StringField QfeTriageStatus { get; set; }

        /// <summary>
        /// PSS
        /// </summary>
        [DataMember]
        public StringField PSS { get; set; }

        /// <summary>
        /// Proudtc the bug belongs to
        /// </summary>
        [DataMember]
        public StringField Product { get; set; }

        /// <summary>
        /// The date when the BUG is accepted
        /// </summary>
        [DataMember]
        public DatetimeField QfeAcceptDate { get; set; }

        /// <summary>
        /// Check-in submission date
        /// </summary>
        [DataMember]
        public DatetimeField CheckinSubmitDate { get; set; }

        /// <summary>
        /// The date when builder finishes build and assign the bug back to Dev
        /// </summary>
        [DataMember]
        public DatetimeField BuildFinishDate { get; set; }

        /// <summary>
        /// Private tester
        /// </summary>
        [DataMember]
        public StringField PrivateTester { get; set; }

        /// <summary>
        /// BUG Release Date
        /// </summary>
        [DataMember]
        public DatetimeField ReleaseDate { get; set; }

        /// <summary>
        /// Affected Binaries
        /// </summary>
        [DataMember]
        public StringField Binaries { get; set; }

        /// <summary>
        /// Customer Verification Finish Date
        /// </summary>
        [DataMember]
        public DatetimeField CustomerVerifyFinishDate { get; set; }

        /// <summary>
        /// QFE Status: Awaiting PSS/BVT Pending/Closed/Core Review etc
        /// </summary>
        [DataMember]
        public StringField QfeStatus { get; set; }

        /// <summary>
        /// Who found the BUG
        /// </summary>
        [DataMember]
        public StringField Customer { get; set; }

        /// <summary>
        /// PM owner
        /// </summary>
        [DataMember]
        public StringField PMOwner { get; set; }

        /// <summary>
        /// Developer
        /// </summary>
        [DataMember]
        public StringField Developer { get; set; }

        /// <summary>
        /// BUG title
        /// </summary>
        [DataMember]
        public StringField Title { get; set; }

        /// <summary>
        /// BUG status: Active, Resolved, Closed
        /// </summary>
        [DataMember]
        public StringField Status { get; set; }

        /// <summary>
        /// Changed By
        /// </summary>
        [DataMember]
        public StringField ChangedBy { get; set; }

        /// <summary>
        /// Resolved date
        /// </summary>
        [DataMember]
        public DatetimeField ResolvedDate { get; set; }

        /// <summary>
        /// Resolved by
        /// </summary>
        [DataMember]
        public StringField Resolver { get; set; }

        /// <summary>
        /// Resolution: Fixed, External, Won't Fix, By Design
        /// </summary>
        [DataMember]
        public StringField Resolution { get; set; }

        /// <summary>
        /// Bug Owner (Assign to)
        /// </summary>
        [DataMember]
        public StringField BugOwner { get; set; }

        /// <summary>
        /// Issue Type (RFC/RFH/CDCR)
        /// </summary>
        [DataMember]
        public StringField IssueType { get; set; }

        /// <summary>
        /// BUG severity
        /// </summary>
        [DataMember]
        public StringField Severity { get; set; }

        /// <summary>
        /// The guy who closed the bug
        /// </summary>
        [DataMember]
        public StringField ClosedBy { get; set; }

        /// <summary>
        /// BUG priority
        /// </summary>
        [DataMember]
        public StringField Priority { get; set; }

        /// <summary>
        /// The time when the bug is opened
        /// </summary>
        [DataMember]
        public DatetimeField BugOpenedDate { get; set; }

        /// <summary>
        /// BUG Filer
        /// </summary>
        [DataMember]
        public StringField BugFiler { get; set; }

        /// <summary>
        /// Cause
        /// </summary>
        [DataMember]
        public StringField Cause { get; set; }

        /// <summary>
        /// RFx Close Date
        /// </summary>
        [DataMember]
        public DatetimeField CloseDate { get; set; }

        /// <summary>
        /// bug's last modify time 
        /// </summary>
        [DataMember]
        public DatetimeField LastModifiedTime { get; set; }

        /// <summary>
        /// Feature
        /// </summary>
        [DataMember]
        public StringField Feature { get; set; }

        /// <summary>
        /// Fix ETA
        /// </summary>
        [DataMember]
        public DatetimeField FixETA { get; set; }

        /// <summary>
        /// Must Fix By Date
        /// </summary>
        [DataMember]
        public DatetimeField MustFixBy { get; set; }

        /// <summary>
        /// The date when the official bundle is created
        /// </summary>
        [DataMember]
        public DatetimeField BundleCreateDate { get; set; }

        /// <summary>
        /// Test Reviewer
        /// </summary>
        [DataMember]
        public StringField TestReviewer { get; set; }

        /// <summary>
        /// SLA date
        /// </summary>
        [DataMember]
        public DatetimeField SLADate { get; set; }

        /// <summary>
        /// Work Item State
        /// </summary>
        [DataMember]
        public StringField WorkItemState { get; set; }

        /// <summary>
        /// QFE Assigned Date
        /// </summary>
        [DataMember]
        public DatetimeField QfeAssignedDate { get; set; }

        /// <summary>
        /// The Path bug is located at
        /// </summary>
        [DataMember]
        public StringField Path { get; set; }

        /// <summary>
        /// The product build in which a bug was found
        /// </summary>
        [DataMember]
        public StringField OpenedBuild { get; set; }

        /// <summary>
        /// The product build in which a bug was fixed
        /// </summary>
        [DataMember]
        public StringField ResolvedBuild { get; set; }

        /// <summary>
        /// Dev Owner
        /// </summary>
        [DataMember]
        public StringField DevOwner { get; set; }

        /// <summary>
        /// Test Owner
        /// </summary>
        [DataMember]
        public StringField TestOwner { get; set; }

        /// <summary>
        /// Build Owner
        /// </summary>
        [DataMember]
        public StringField BuildOwner { get; set; }

        /// <summary>
        /// Official builder
        /// </summary>
        [DataMember]
        public StringField Builder { get; set; }

        /// <summary>
        /// Release PM
        /// </summary>
        [DataMember]
        public StringField ReleasePM { get; set; }

        /// <summary>
        /// The date when the BUG is rejected
        /// </summary>
        [DataMember]
        public DatetimeField QfeRejectDate { get; set; }

        /// <summary>
        /// The date when the BUG is approved to investigate
        /// </summary>
        [DataMember]
        public DatetimeField InvestigationAssignDate { get; set; }

        /// <summary>
        /// Investigation SLA
        /// </summary>
        [DataMember]
        public DatetimeField InvestigationDue { get; set; }

        /// <summary>
        /// The date when the investigation is finished
        /// </summary>
        [DataMember]
        public DatetimeField InvestigationFinishDate { get; set; }

        /// <summary>
        /// Code Review request date
        /// </summary>
        [DataMember]
        public DatetimeField CodeReviewRequestDate { get; set; }

        /// <summary>
        /// Code Review Status
        /// </summary>
        [DataMember]
        public StringField CodeReviewStatus { get; set; }

        /// <summary>
        /// Code Reviewer
        /// </summary>
        [DataMember]
        public StringField CodeReviewer { get; set; }

        /// <summary>
        /// Code Review File
        /// </summary>
        [DataMember]
        public StringField CodeReviewFile { get; set; }

        /// <summary>
        /// Code Review Finish Date
        /// </summary>
        [DataMember]
        public DatetimeField CodeReviewFinishDate { get; set; }

        /// <summary>
        /// Test Case Info
        /// </summary>
        [DataMember]
        public StringField TestCase { get; set; }

        /// <summary>
        /// The date when private test is requested
        /// </summary>
        [DataMember]
        public DatetimeField PrivateTestRequestDate { get; set; }

        /// <summary>
        /// Private test Status: Available/Passed/Failed
        /// </summary>
        [DataMember]
        public StringField PrivateTestStatus { get; set; }

        /// <summary>
        /// The date the private test is finished 
        /// </summary>
        [DataMember]
        public DatetimeField PrivateTestFinishDate { get; set; }

        /// <summary>
        /// Check-in request date
        /// </summary>
        [DataMember]
        public DatetimeField CheckinRequestDate { get; set; }

        /// <summary>
        /// Check-in status: Requested/Approved/Rejected/Submitted
        /// </summary>
        [DataMember]
        public StringField CheckinStatus { get; set; }

        /// <summary>
        /// Check-in approved date
        /// </summary>
        [DataMember]
        public DatetimeField CheckinApproveDate { get; set; }

        /// <summary>
        /// The date when the bug is assigned to builder
        /// </summary>
        [DataMember]
        public DatetimeField BuildRequestDate { get; set; }

        /// <summary>
        /// The date the official bundle is assigned to BVT 
        /// </summary>
        [DataMember]
        public DatetimeField BVTAssignDate { get; set; }

        /// <summary>
        /// BVT status: Assigned/Passed/Failed
        /// </summary>
        [DataMember]
        public StringField BVTStatus { get; set; }

        /// <summary>
        /// BVT tester
        /// </summary>
        [DataMember]
        public StringField BVTTester { get; set; }

        /// <summary>
        /// The date when BVT is finished
        /// </summary>
        [DataMember]
        public DatetimeField BVTFinishDate { get; set; }

        /// <summary>
        /// The date when the bug is assigned to regression test
        /// </summary>
        [DataMember]
        public DatetimeField RegressionAssignDate { get; set; }

        /// <summary>
        /// Regression Test status: Assigned/Passed/Failed
        /// </summary>
        [DataMember]
        public StringField RegressionStatus { get; set; }

        /// <summary>
        /// Regerssion Tester
        /// </summary>
        [DataMember]
        public StringField RegressionTester { get; set; }

        /// <summary>
        /// Regression Test Finish Date
        /// </summary>
        [DataMember]
        public DatetimeField RegressionFinishDate { get; set; }

        /// <summary>
        /// Code Sign Request Date
        /// </summary>
        [DataMember]
        public DatetimeField CodeSignRequestDate { get; set; }

        /// <summary>
        /// Code Sign Finish Date
        /// </summary>
        [DataMember]
        public DatetimeField CodeSignFinishDate { get; set; }

        /// <summary>
        /// Code Sign Request Number
        /// </summary>
        [DataMember]
        public StringField CodeSignRequestNumber { get; set; }

        /// <summary>
        /// Rfx Submitted
        /// </summary>
        [DataMember]
        public DatetimeField RfxSubmitted { get; set; }

        /// <summary>
        /// Bug changelist information
        /// </summary>
        [DataMember]
        public PendingChangelistField PendingChangelist { get; set; }

        /// <summary>
        /// Security rank
        /// </summary>
        [DataMember]
        public StringField SecurityRank { get; set; }

        /// <summary>
        /// Security effect
        /// </summary>
        [DataMember]
        public StringField SecurityEffect { get; set; }

        /// <summary>
        /// Approved by
        /// </summary>
        [DataMember]
        public StringField ApprovedBy { get; set; }

        /// <summary>
        /// Milestone
        /// </summary>
        [DataMember]
        public StringField Milestone { get; set; }

        /// <summary>
        /// Rfx Triaged
        /// </summary>
        [DataMember]
        public DatetimeField RFxTriagedDate { get; set; }

        /// <summary>
        /// Investigation Complete
        /// </summary>
        [DataMember]
        public DatetimeField InvestigationCompleteDate { get; set; }

        #endregion Properties

        #region Public functions

        /// <summary>
        /// Constructor
        /// </summary>
        public BugData()
        {
            this.TeamId = new IntegerField();
            this.TeamId.FieldName = BugFields.TeamId;

            this.BugId = new IntegerField();
            this.BugId.FieldName = BugFields.BugId;

            this.Description = new StringField();
            this.Description.FieldName = BugFields.Description;

            this.DevResolutionNotes = new StringField();
            this.DevResolutionNotes.FieldName = BugFields.DevResolutionNotes;

            this.KbNumber = new StringField();
            this.KbNumber.FieldName = BugFields.KbNumber;

            this.QfeTriageStatus = new StringField();
            this.QfeTriageStatus.FieldName = BugFields.QfeTriageStatus;

            this.PSS = new StringField();
            this.PSS.FieldName = BugFields.PSS;

            this.Product = new StringField();
            this.Product.FieldName = BugFields.Product;

            this.QfeAcceptDate = new DatetimeField();
            this.QfeAcceptDate.FieldName = BugFields.QfeAcceptDate;

            this.CheckinSubmitDate = new DatetimeField();
            this.CheckinSubmitDate.FieldName = BugFields.CheckinSubmitDate;

            this.BuildFinishDate = new DatetimeField();
            this.BuildFinishDate.FieldName = BugFields.BuildFinishDate;

            this.PrivateTester = new StringField();
            this.PrivateTester.FieldName = BugFields.PrivateTester;

            this.ReleaseDate = new DatetimeField();
            this.ReleaseDate.FieldName = BugFields.ReleaseDate;

            this.Binaries = new StringField();
            this.Binaries.FieldName = BugFields.Binaries;

            this.CustomerVerifyFinishDate = new DatetimeField();
            this.CustomerVerifyFinishDate.FieldName = BugFields.CustomerVerifyFinishDate;

            this.QfeStatus = new StringField();
            this.QfeStatus.FieldName = BugFields.QfeStatus;

            this.Customer = new StringField();
            this.Customer.FieldName = BugFields.Customer;

            this.PMOwner = new StringField();
            this.PMOwner.FieldName = BugFields.PMOwner;

            this.Developer = new StringField();
            this.Developer.FieldName = BugFields.Developer;

            this.Title = new StringField();
            this.Title.FieldName = BugFields.Title;

            this.Status = new StringField();
            this.Status.FieldName = BugFields.Status;

            this.ChangedBy = new StringField();
            this.ChangedBy.FieldName = BugFields.ChangedBy;

            this.ResolvedDate = new DatetimeField();
            this.ResolvedDate.FieldName = BugFields.ResolvedDate;

            this.Resolver = new StringField();
            this.Resolver.FieldName = BugFields.Resolver;

            this.Resolution = new StringField();
            this.Resolution.FieldName = BugFields.Resolution;

            this.BugOwner = new StringField();
            this.BugOwner.FieldName = BugFields.BugOwner;

            this.IssueType = new StringField();
            this.IssueType.FieldName = BugFields.IssueType;

            this.Severity = new StringField();
            this.Severity.FieldName = BugFields.Severity;

            this.ClosedBy = new StringField();
            this.ClosedBy.FieldName = BugFields.ClosedBy;

            this.Priority = new StringField();
            this.Priority.FieldName = BugFields.Priority;

            this.BugOpenedDate = new DatetimeField();
            this.BugOpenedDate.FieldName = BugFields.BugOpenedDate;

            this.BugFiler = new StringField();
            this.BugFiler.FieldName = BugFields.BugFiler;

            this.Cause = new StringField();
            this.Cause.FieldName = BugFields.Cause;

            this.CloseDate = new DatetimeField();
            this.CloseDate.FieldName = BugFields.CloseDate;

            this.LastModifiedTime = new DatetimeField();
            this.LastModifiedTime.FieldName = BugFields.LastModifiedTime;

            this.Feature = new StringField();
            this.Feature.FieldName = BugFields.Feature;

            this.FixETA = new DatetimeField();
            this.FixETA.FieldName = BugFields.FixETA;

            this.MustFixBy = new DatetimeField();
            this.MustFixBy.FieldName = BugFields.MustFixBy;

            this.BundleCreateDate = new DatetimeField();
            this.BundleCreateDate.FieldName = BugFields.BundleCreateDate;

            this.TestReviewer = new StringField();
            this.TestReviewer.FieldName = BugFields.TestReviewer;

            this.SLADate = new DatetimeField();
            this.SLADate.FieldName = BugFields.SLADate;

            this.WorkItemState = new StringField();
            this.WorkItemState.FieldName = BugFields.WorkItemState;

            this.QfeAssignedDate = new DatetimeField();
            this.QfeAssignedDate.FieldName = BugFields.QfeAssignedDate;

            this.Path = new StringField();
            this.Path.FieldName = BugFields.Path;

            this.OpenedBuild = new StringField();
            this.OpenedBuild.FieldName = BugFields.OpenedBuild;

            this.ResolvedBuild = new StringField();
            this.ResolvedBuild.FieldName = BugFields.ResolvedBuild;

            this.DevOwner = new StringField();
            this.DevOwner.FieldName = BugFields.DevOwner;

            this.TestOwner = new StringField();
            this.TestOwner.FieldName = BugFields.TestOwner;

            this.BuildOwner = new StringField();
            this.BuildOwner.FieldName = BugFields.BuildOwner;

            this.Builder = new StringField();
            this.Builder.FieldName = BugFields.Builder;

            this.ReleasePM = new StringField();
            this.ReleasePM.FieldName = BugFields.ReleasePM;

            this.QfeRejectDate = new DatetimeField();
            this.QfeRejectDate.FieldName = BugFields.QfeRejectDate;

            this.InvestigationAssignDate = new DatetimeField();
            this.InvestigationAssignDate.FieldName = BugFields.InvestigationAssignDate;

            this.InvestigationDue = new DatetimeField();
            this.InvestigationDue.FieldName = BugFields.InvestigationDue;

            this.InvestigationFinishDate = new DatetimeField();
            this.InvestigationFinishDate.FieldName = BugFields.InvestigationFinishDate;

            this.CodeReviewRequestDate = new DatetimeField();
            this.CodeReviewRequestDate.FieldName = BugFields.CodeReviewRequestDate;

            this.CodeReviewStatus = new StringField();
            this.CodeReviewStatus.FieldName = BugFields.CodeReviewStatus;

            this.CodeReviewer = new StringField();
            this.CodeReviewer.FieldName = BugFields.CodeReviewer;

            this.CodeReviewFile = new StringField();
            this.CodeReviewFile.FieldName = BugFields.CodeReviewFile;

            this.CodeReviewFinishDate = new DatetimeField();
            this.CodeReviewFinishDate.FieldName = BugFields.CodeReviewFinishDate;

            this.TestCase = new StringField();
            this.TestCase.FieldName = BugFields.TestCase;

            this.PrivateTestRequestDate = new DatetimeField();
            this.PrivateTestRequestDate.FieldName = BugFields.PrivateTestRequestDate;

            this.PrivateTestStatus = new StringField();
            this.PrivateTestStatus.FieldName = BugFields.PrivateTestStatus;

            this.PrivateTestFinishDate = new DatetimeField();
            this.PrivateTestFinishDate.FieldName = BugFields.PrivateTestFinishDate;

            this.CheckinRequestDate = new DatetimeField();
            this.CheckinRequestDate.FieldName = BugFields.CheckinRequestDate;

            this.CheckinStatus = new StringField();
            this.CheckinStatus.FieldName = BugFields.CheckinStatus;

            this.CheckinApproveDate = new DatetimeField();
            this.CheckinApproveDate.FieldName = BugFields.CheckinApproveDate;

            this.BuildRequestDate = new DatetimeField();
            this.BuildRequestDate.FieldName = BugFields.BuildRequestDate;

            this.BVTAssignDate = new DatetimeField();
            this.BVTAssignDate.FieldName = BugFields.BVTAssignDate;

            this.BVTStatus = new StringField();
            this.BVTStatus.FieldName = BugFields.BVTStatus;

            this.BVTTester = new StringField();
            this.BVTTester.FieldName = BugFields.BVTTester;

            this.BVTFinishDate = new DatetimeField();
            this.BVTFinishDate.FieldName = BugFields.BVTFinishDate;

            this.RegressionAssignDate = new DatetimeField();
            this.RegressionAssignDate.FieldName = BugFields.RegressionAssignDate;

            this.RegressionStatus = new StringField();
            this.RegressionStatus.FieldName = BugFields.RegressionStatus;

            this.RegressionTester = new StringField();
            this.RegressionTester.FieldName = BugFields.RegressionTester;

            this.CodeSignRequestDate = new DatetimeField();
            this.CodeSignRequestDate.FieldName = BugFields.CodeSignRequestDate;

            this.CodeSignFinishDate = new DatetimeField();
            this.CodeSignFinishDate.FieldName = BugFields.CodeSignFinishDate;

            this.CodeSignRequestNumber = new StringField();
            this.CodeSignRequestNumber.FieldName = BugFields.CodeSignRequestNumber;

            this.RfxSubmitted = new DatetimeField();
            this.RfxSubmitted.FieldName = BugFields.RfxSubmitted;

            this.PendingChangelist = new PendingChangelistField();
            this.PendingChangelist.FieldName = BugFields.PendingChangelist;

            this.SecurityRank = new StringField();
            this.SecurityRank.FieldName = BugFields.SecurityRank;

            this.SecurityEffect = new StringField();
            this.SecurityEffect.FieldName = BugFields.SecurityEffect;

            this.ApprovedBy = new StringField();
            this.ApprovedBy.FieldName = BugFields.ApprovedBy;

            this.Milestone = new StringField();
            this.Milestone.FieldName = BugFields.Milestone;

            this.RFxTriagedDate = new DatetimeField();
            this.RFxTriagedDate.FieldName = BugFields.RFxTriagedDate;

            this.InvestigationCompleteDate = new DatetimeField();
            this.InvestigationCompleteDate.FieldName = BugFields.InvestigationCompleteDate;
        }


        public BugData(Int32 teamId,
            Int32 bugId,
            String portalStatus,
            String description,
            String devResolutionNotes,
            String kbNumber,
            String qfeTriageStatus,
            String pss,
            String product,
            DateTime? qfeAcceptDate,
            DateTime? checkinSubmissionDate,
            DateTime? buildFinishDate,
            DateTime? releaseDate,
            String binaries,
            DateTime? customerVerifyFinishDate,
            String qfeStatus,
            String customer,
            String pmOwner,
            String developer,
            String title,
            String status,
            String changedBy,
            DateTime? resolvedDate,
            String resolver,
            String resolution,
            String bugOwner,
            String issueType,
            Int32 severity,
            String closedBy,
            Int32 priority,
            DateTime? bugOpenedDate,
            String bugFiler,
            String cause,
            DateTime? closeDate,
            DateTime? lastModifiedTime,
            String feature,
            DateTime? fixETA,
            DateTime? mustFixBy,
            DateTime? bundleCreationDate,
            String testReviewer,
            DateTime? slaDate,
            String workItemState,
            DateTime? qfeAssignedDate,
            String path,
            String openbuild,
            String resolvedBuild,
            String devOwner,
            String testOwner,
            String buildOwner,
            String builder,
            String releasePM,
            DateTime? qfeRejectDate,
            DateTime? investigationAssignDate,
            DateTime? investigationDue,
            DateTime? investigationFinishDate,
            DateTime? codeReviewRequestDate,
            String codeReviewStatus,
            String codeReviewer,
            String codeReviewFile,
            DateTime? codeReviewFinishDate,
            String testCase,
            DateTime? privateTestRequestDate,
            String privateTestStatus,
            String privateTester,
            DateTime? privateTestFinishDate,
            DateTime? checkinRequestDate,
            String checkinStatus,
            DateTime? checkinApproveDate,
            DateTime? buildRequestDate,
            DateTime? bvtAssignDate,
            String bvtStatus,
            String bvtTester,
            DateTime? bvtFinishDate,
            DateTime? regressionAssignDate,
            String regressionStatus,
            String regressionTester,
            DateTime? regressionFinishDate,
            DateTime? codeSignRequestDate,
            DateTime? codeSignFinishDate,
            String codeSignRequestNumber,
            DateTime? rfxSubmitted,
            ChangelistInfo changelist,
            String securityRank,
            String securityEffect,
            String approvedBy,
            String milestone,
            DateTime? rfxTriagedDate,
            DateTime? investigationCompleteDate
            )
        {

            this.TeamId = new IntegerField();
            this.TeamId.FieldName = BugFields.TeamId;
            this.TeamId.FieldValue = teamId;

            this.BugId = new IntegerField();
            this.BugId.FieldName = BugFields.BugId;
            this.BugId.FieldValue = bugId;

            this.Description = new StringField();
            this.Description.FieldName = BugFields.Description;
            this.Description.FieldValue = description;

            this.DevResolutionNotes = new StringField();
            this.DevResolutionNotes.FieldName = BugFields.DevResolutionNotes;
            this.DevResolutionNotes.FieldValue = devResolutionNotes;

            this.KbNumber = new StringField();
            this.KbNumber.FieldName = BugFields.KbNumber;
            this.KbNumber.FieldValue = kbNumber;

            this.QfeTriageStatus = new StringField();
            this.QfeTriageStatus.FieldName = BugFields.QfeTriageStatus;
            this.QfeTriageStatus.FieldValue = qfeTriageStatus;

            this.PSS = new StringField();
            this.PSS.FieldName = BugFields.PSS;
            this.PSS.FieldValue = pss;

            this.Product = new StringField();
            this.Product.FieldName = BugFields.Product;
            this.Product.FieldValue = product;

            this.QfeAcceptDate = new DatetimeField();
            this.QfeAcceptDate.FieldName = BugFields.QfeAcceptDate;
            this.QfeAcceptDate.FieldValue = qfeAcceptDate;

            this.CheckinSubmitDate = new DatetimeField();
            this.CheckinSubmitDate.FieldName = BugFields.CheckinSubmitDate;
            this.CheckinSubmitDate.FieldValue = checkinSubmissionDate;

            this.BuildFinishDate = new DatetimeField();
            this.BuildFinishDate.FieldName = BugFields.BuildFinishDate;
            this.BuildFinishDate.FieldValue = buildFinishDate;

            this.ReleaseDate = new DatetimeField();
            this.ReleaseDate.FieldName = BugFields.ReleaseDate;
            this.ReleaseDate.FieldValue = releaseDate;

            this.Binaries = new StringField();
            this.Binaries.FieldName = BugFields.Binaries;
            this.Binaries.FieldValue = binaries;

            this.CustomerVerifyFinishDate = new DatetimeField();
            this.CustomerVerifyFinishDate.FieldName = BugFields.CustomerVerifyFinishDate;
            this.CustomerVerifyFinishDate.FieldValue = customerVerifyFinishDate;

            this.QfeStatus = new StringField();
            this.QfeStatus.FieldName = BugFields.QfeStatus;
            this.QfeStatus.FieldValue = qfeStatus;

            this.Customer = new StringField();
            this.Customer.FieldName = BugFields.Customer;
            this.Customer.FieldValue = customer;

            this.PMOwner = new StringField();
            this.PMOwner.FieldName = BugFields.PMOwner;
            this.PMOwner.FieldValue = pmOwner;

            this.Developer = new StringField();
            this.Developer.FieldName = BugFields.Developer;
            this.Developer.FieldValue = developer;

            this.Title = new StringField();
            this.Title.FieldName = BugFields.Title;
            this.Title.FieldValue = title;

            this.Status = new StringField();
            this.Status.FieldName = BugFields.Status;
            this.Status.FieldValue = status;

            this.ChangedBy = new StringField();
            this.ChangedBy.FieldName = BugFields.ChangedBy;
            this.ChangedBy.FieldValue = changedBy;

            this.ResolvedDate = new DatetimeField();
            this.ResolvedDate.FieldName = BugFields.ResolvedDate;
            this.ResolvedDate.FieldValue = resolvedDate;

            this.Resolver = new StringField();
            this.Resolver.FieldName = BugFields.Resolver;
            this.Resolver.FieldValue = resolver;

            this.Resolution = new StringField();
            this.Resolution.FieldName = BugFields.Resolution;
            this.Resolution.FieldValue = resolution;

            this.BugOwner = new StringField();
            this.BugOwner.FieldName = BugFields.BugOwner;
            this.BugOwner.FieldValue = bugOwner;

            this.IssueType = new StringField();
            this.IssueType.FieldName = BugFields.IssueType;
            this.IssueType.FieldValue = issueType;

            this.Severity = new StringField();
            this.Severity.FieldName = BugFields.Severity;
            this.Severity.FieldValue = severity.ToString();

            this.ClosedBy = new StringField();
            this.ClosedBy.FieldName = BugFields.ClosedBy;
            this.ClosedBy.FieldValue = closedBy;

            this.Priority = new StringField();
            this.Priority.FieldName = BugFields.Priority;
            this.Priority.FieldValue = priority.ToString();

            this.BugOpenedDate = new DatetimeField();
            this.BugOpenedDate.FieldName = BugFields.BugOpenedDate;
            this.BugOpenedDate.FieldValue = bugOpenedDate;

            this.BugFiler = new StringField();
            this.BugFiler.FieldName = BugFields.BugFiler;
            this.BugFiler.FieldValue = bugFiler;

            this.Cause = new StringField();
            this.Cause.FieldName = BugFields.Cause;
            this.Cause.FieldValue = cause;

            this.CloseDate = new DatetimeField();
            this.CloseDate.FieldName = BugFields.CloseDate;
            this.CloseDate.FieldValue = closeDate;

            this.LastModifiedTime = new DatetimeField();
            this.LastModifiedTime.FieldName = BugFields.LastModifiedTime;
            this.LastModifiedTime.FieldValue = lastModifiedTime;

            this.Feature = new StringField();
            this.Feature.FieldName = BugFields.Feature;
            this.Feature.FieldValue = feature;

            this.FixETA = new DatetimeField();
            this.FixETA.FieldName = BugFields.FixETA;
            this.FixETA.FieldValue = fixETA;

            this.MustFixBy = new DatetimeField();
            this.MustFixBy.FieldName = BugFields.MustFixBy;
            this.MustFixBy.FieldValue = mustFixBy;

            this.BundleCreateDate = new DatetimeField();
            this.BundleCreateDate.FieldName = BugFields.BundleCreateDate;
            this.BundleCreateDate.FieldValue = bundleCreationDate;

            this.TestReviewer = new StringField();
            this.TestReviewer.FieldName = BugFields.TestReviewer;
            this.TestReviewer.FieldValue = testReviewer;

            this.SLADate = new DatetimeField();
            this.SLADate.FieldName = BugFields.SLADate;
            this.SLADate.FieldValue = slaDate;

            this.WorkItemState = new StringField();
            this.WorkItemState.FieldName = BugFields.WorkItemState;
            this.WorkItemState.FieldValue = workItemState;

            this.QfeAssignedDate = new DatetimeField();
            this.QfeAssignedDate.FieldName = BugFields.QfeAssignedDate;
            this.QfeAssignedDate.FieldValue = qfeAssignedDate;

            this.Path = new StringField();
            this.Path.FieldName = BugFields.Path;
            this.Path.FieldValue = path;

            this.OpenedBuild = new StringField();
            this.OpenedBuild.FieldName = BugFields.OpenedBuild;
            this.OpenedBuild.FieldValue = openbuild;

            this.ResolvedBuild = new StringField();
            this.ResolvedBuild.FieldName = BugFields.ResolvedBuild;
            this.ResolvedBuild.FieldValue = resolvedBuild;

            this.DevOwner = new StringField();
            this.DevOwner.FieldName = BugFields.DevOwner;
            this.DevOwner.FieldValue = devOwner;

            this.TestOwner = new StringField();
            this.TestOwner.FieldName = BugFields.TestOwner;
            this.TestOwner.FieldValue = testOwner;

            this.BuildOwner = new StringField();
            this.BuildOwner.FieldName = BugFields.BuildOwner;
            this.BuildOwner.FieldValue = buildOwner;

            this.Builder = new StringField();
            this.Builder.FieldName = BugFields.Builder;
            this.Builder.FieldValue = builder;

            this.ReleasePM = new StringField();
            this.ReleasePM.FieldName = BugFields.ReleasePM;
            this.ReleasePM.FieldValue = releasePM;

            this.QfeRejectDate = new DatetimeField();
            this.QfeRejectDate.FieldName = BugFields.QfeRejectDate;
            this.QfeRejectDate.FieldValue = qfeRejectDate;

            this.InvestigationAssignDate = new DatetimeField();
            this.InvestigationAssignDate.FieldName = BugFields.InvestigationAssignDate;
            this.InvestigationAssignDate.FieldValue = investigationAssignDate;

            this.InvestigationDue = new DatetimeField();
            this.InvestigationDue.FieldName = BugFields.InvestigationDue;
            this.InvestigationDue.FieldValue = investigationDue;

            this.InvestigationFinishDate = new DatetimeField();
            this.InvestigationFinishDate.FieldName = BugFields.InvestigationFinishDate;
            this.InvestigationFinishDate.FieldValue = investigationFinishDate;

            this.CodeReviewRequestDate = new DatetimeField();
            this.CodeReviewRequestDate.FieldName = BugFields.CodeReviewRequestDate;
            this.CodeReviewRequestDate.FieldValue = codeReviewRequestDate;

            this.CodeReviewStatus = new StringField();
            this.CodeReviewStatus.FieldName = BugFields.CodeReviewStatus;
            this.CodeReviewStatus.FieldValue = codeReviewStatus;

            this.CodeReviewer = new StringField();
            this.CodeReviewer.FieldName = BugFields.CodeReviewer;
            this.CodeReviewer.FieldValue = codeReviewer;

            this.CodeReviewFile = new StringField();
            this.CodeReviewFile.FieldName = BugFields.CodeReviewFile;
            this.CodeReviewFile.FieldValue = codeReviewFile;

            this.CodeReviewFinishDate = new DatetimeField();
            this.CodeReviewFinishDate.FieldName = BugFields.CodeReviewFinishDate;
            this.CodeReviewFinishDate.FieldValue = codeReviewFinishDate;

            this.TestCase = new StringField();
            this.TestCase.FieldName = BugFields.TestCase;
            this.TestCase.FieldValue = testCase;

            this.PrivateTestRequestDate = new DatetimeField();
            this.PrivateTestRequestDate.FieldName = BugFields.PrivateTestRequestDate;
            this.PrivateTestRequestDate.FieldValue = privateTestRequestDate;

            this.PrivateTestStatus = new StringField();
            this.PrivateTestStatus.FieldName = BugFields.PrivateTestStatus;
            this.PrivateTestStatus.FieldValue = privateTestStatus;

            this.PrivateTester = new StringField();
            this.PrivateTester.FieldName = BugFields.PrivateTester;
            this.PrivateTester.FieldValue = privateTester;

            this.PrivateTestFinishDate = new DatetimeField();
            this.PrivateTestFinishDate.FieldName = BugFields.PrivateTestFinishDate;
            this.PrivateTestFinishDate.FieldValue = privateTestFinishDate;

            this.CheckinRequestDate = new DatetimeField();
            this.CheckinRequestDate.FieldName = BugFields.CheckinRequestDate;
            this.CheckinRequestDate.FieldValue = checkinRequestDate;

            this.CheckinStatus = new StringField();
            this.CheckinStatus.FieldName = BugFields.CheckinStatus;
            this.CheckinStatus.FieldValue = checkinStatus;

            this.CheckinApproveDate = new DatetimeField();
            this.CheckinApproveDate.FieldName = BugFields.CheckinApproveDate;
            this.CheckinApproveDate.FieldValue = checkinApproveDate;

            this.BuildRequestDate = new DatetimeField();
            this.BuildRequestDate.FieldName = BugFields.BuildRequestDate;
            this.BuildRequestDate.FieldValue = buildRequestDate;

            this.BVTAssignDate = new DatetimeField();
            this.BVTAssignDate.FieldName = BugFields.BVTAssignDate;
            this.BVTAssignDate.FieldValue = bvtAssignDate;

            this.BVTStatus = new StringField();
            this.BVTStatus.FieldName = BugFields.BVTStatus;
            this.BVTStatus.FieldValue = bvtStatus;

            this.BVTTester = new StringField();
            this.BVTTester.FieldName = BugFields.BVTTester;
            this.BVTTester.FieldValue = bvtTester;

            this.BVTFinishDate = new DatetimeField();
            this.BVTFinishDate.FieldName = BugFields.BVTFinishDate;
            this.BVTFinishDate.FieldValue = bvtFinishDate;

            this.RegressionStatus = new StringField();
            this.RegressionStatus.FieldName = BugFields.RegressionStatus;
            this.RegressionStatus.FieldValue = regressionStatus;

            this.RegressionTester = new StringField();
            this.RegressionTester.FieldName = BugFields.RegressionTester;
            this.RegressionTester.FieldValue = regressionTester;

            this.RegressionFinishDate = new DatetimeField();
            this.RegressionFinishDate.FieldName = BugFields.RegressionFinishDate;
            this.RegressionFinishDate.FieldValue = regressionFinishDate;

            this.CodeSignRequestDate = new DatetimeField();
            this.CodeSignRequestDate.FieldName = BugFields.CodeSignRequestDate;
            this.CodeSignRequestDate.FieldValue = codeSignRequestDate;

            this.CodeSignFinishDate = new DatetimeField();
            this.CodeSignFinishDate.FieldName = BugFields.CodeSignFinishDate;
            this.CodeSignFinishDate.FieldValue = codeSignFinishDate;

            this.CodeSignRequestNumber = new StringField();
            this.CodeSignRequestNumber.FieldName = BugFields.CodeSignRequestNumber;
            this.CodeSignRequestNumber.FieldValue = codeSignRequestNumber;

            this.RfxSubmitted = new DatetimeField();
            this.RfxSubmitted.FieldName = BugFields.RfxSubmitted;
            this.RfxSubmitted.FieldValue = rfxSubmitted;

            this.PendingChangelist = new PendingChangelistField();
            this.PendingChangelist.FieldName = BugFields.PendingChangelist;
            this.PendingChangelist.FieldValue = changelist;

            this.SecurityRank = new StringField();
            this.SecurityRank.FieldName = BugFields.SecurityRank;
            this.SecurityRank.FieldValue = securityRank;

            this.SecurityEffect = new StringField();
            this.SecurityEffect.FieldName = BugFields.SecurityEffect;
            this.SecurityEffect.FieldValue = securityEffect;

            this.ApprovedBy = new StringField();
            this.ApprovedBy.FieldName = BugFields.ApprovedBy;
            this.ApprovedBy.FieldValue = approvedBy;

            this.Milestone = new StringField();
            this.Milestone.FieldName = BugFields.Milestone;
            this.Milestone.FieldValue = milestone;

            this.RFxTriagedDate = new DatetimeField();
            this.RFxTriagedDate.FieldName = BugFields.RFxTriagedDate;
            this.RFxTriagedDate.FieldValue = rfxTriagedDate;

            this.InvestigationCompleteDate = new DatetimeField();
            this.InvestigationCompleteDate.FieldName = BugFields.InvestigationCompleteDate;
            this.InvestigationCompleteDate.FieldValue = investigationCompleteDate;
        }

        #endregion Public functions
    }


    /// <summary>
    /// Integer item list
    /// </summary>
    [DataContract]
    public class IntegerItemCollection
    {
        #region Properties

        /// <summary>
        /// Integer Item List
        /// </summary>
        [DataMember]
        public Dictionary<int, string> IntegerItemList { get; set; }

        #endregion Properties

        public IntegerItemCollection()
        {
            IntegerItemList = new Dictionary<int, string>();
        }
    }

    /// <summary>
    /// String item list
    /// </summary>
    [DataContract]
    public class StringItemCollection
    {
        #region Properties

        /// <summary>
        /// String Item List
        /// </summary>
        [DataMember]
        public Dictionary<string, string> StringItemList { get; set; }

        #endregion Properties

        public StringItemCollection()
        {
            StringItemList = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }
    }
}
