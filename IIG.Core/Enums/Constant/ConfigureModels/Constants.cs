using IIG.Core.Common.Enums;

namespace IIG.Core.Common.ConfigureModels
{
    public static class Constants
    {
        public const string BaseUrlClassin = "https://www.eeo.cn/client/invoke/index.html";
        public const string DetailDateFormat = "ddMMyyyy hh:mm:ss";
        public const string DateFormatExcel = "_ddMMyyyy_hh:mm:ss";

        public static class FileType
        {
            public const string ExcelContentType = "application/vnd.ms-excel";
            public const string OctetStream = "application/octet-stream";
            public const string ExcelXlsx = "application/xlsx";
        }

        public static class ImportFileResponseHeader
        {
            public const string ImportFileFail = "ImportFileFail";
        }

        public static class FirebaseNotification
        {
            public const string Navigate = "Navigate";
            public const string NavigateType3 = "NavigateType3";
            public const string NavigateType6 = "NavigateType6";
            public const string NavigateType7 = "NavigateType7";
            public const string NavigateType8 = "NavigateType8";
            public const string NavigateType9 = "NavigateType9";
            public const string NavigateType10 = "NavigateType10";
            public const string NavigateType11 = "NavigateType11";
            public const string NavigateType12 = "NavigateType12";
            public const string NavigateType13 = "NavigateType13";
            public const string NavigateType14 = "NavigateType14";
            public const string NavigateType15 = "NavigateType15";
            public const string PurchaseHistory = "PurchaseHistory";
            public const string ChannelId = "channelId";
        }

        public static class PrefixCategory
        {
            public const string Character = "--- ";
        }

        public static class Pagination
        {
            public const int DefaultPage = 1;
            public const int ItemsPerPage = 20;
        }

        public static class LanguageTags
        {
            public const string TiengViet = "vi-VN";
            public const string TiengAnh = "en-US";
            public const string TiengHan = "ko-KR";
        }

        public static class RequestHeaderKey
        {
            public const string LanguageCode = "x-language-code";
            public const string KeyCodeCookieKey = "key_code_cookie_key";
        }

        public static class RedisKey
        {
            public const string RedisAllPermission = "RedisAllPermission";
            public const string RedisLanguageTags = "RedisLanguageTags";
            public const string RedisAllQuestionnaireGroupTreeView = "RedisAllQuestionnaireGroupTreeView";
            public const string RedisFirebaseRegistrationToken = "RedisFirebaseRegistrationToken";
            public const string RedisClassinLessonLinkUrl = "RedisClassinLessonLinkUrl";
            public const string RedisPermissionUserId = "RedisPermission{0}";
            public const string RedisFileInfo = "RedisFileInfo{0}";
            public const string RedisToeflSchool = "RedisToeflSchool";
            public const string RedisToeflProvince = "RedisToeflProvince";
            public const string RedisToeflDistrict = "RedisToeflDistrict";
            public const string RedisToeflChallengeTypes = "RedisToeflChallengeTypes";
            public const string RedisToeflChallengeMocktest = "RedisToeflChallengeMocktest";
            public const string RedisToeflChallengeContest = "RedisToeflChallengeContest";
            public const string RedisToeflChallengeCourseSuggestion = "RedisToeflChallengeCourseSuggestion";
            public const string RedisCourseScoringFilter = "RedisCourseScoringFilter:{0}";
            public const string RedisLiveClassScoringFilter = "RedisLiveClassScoringFilter:{0}";

            //category menu 
            public const string RedisCategoryMenuTop = "RedisCategoryMenuTop:{0}";
            public const string RedisCategoryMenu = "RedisCategoryMenu:{0}";

            public const string RedisMenuCategoryDetail = "RedisMenuCategoryDetail:{0}";
            public const string RedisMenuTTeacher = "RedisTTeacher";
            public const string RedisMenuTStudent = "RedisTStudent";
            public const string RedisMenuHomePageInfo = "RedisMenuHomePageInfo:{0}";

            public static string GetPrefixRedisKey(string key)
            {
                var array = key.Split(':');
                return array.Length > 1 ? array[0] : key;
            }

        }
        public static class AutoGenerateKeyCode
        {
            public const int KeyCodeLength = 10;
            public const int Quantity = 1;
        }

        public static class LanguageTagsMessage
        {
            public const string TranslationRequireVIKey = "translation_require_vi_key";
            public const string TranslationIsRequired = "translation_is_required";
        }

        public static class ClaimTypes
        {
            public const string UserId = "id";
            public const string RefreshToken = "refresh_token";
            public const string AccessToken = "access_token";
        }

        public static class Permission
        {
            public const string ParentQueryInsert = @"INSERT INTO [dbo].[permission]([id] ,[name] ,[description] ,[parent_id] ,[created] ,[modified] ,[is_permission]) VALUES ('{0}', '{1}', NULL, NULL, GETUTCDATE(), GETUTCDATE(), {2});";
            public const string QueryInsert = @"INSERT INTO [dbo].[permission]([id] ,[name] ,[description] ,[parent_id] ,[created] ,[modified] ,[is_permission]) VALUES ('{0}', '{1}', NULL, '{2}', GETUTCDATE(), GETUTCDATE(), {3});";
        }

        //public static class FileSettings
        //{
        //    public static readonly List<EFileTypeIdentifier> ListImageType = new()
        //    {
        //        EFileTypeIdentifier.ImageCourse,
        //        EFileTypeIdentifier.ImageUser,
        //        EFileTypeIdentifier.ImageCourseTeacher,
        //        EFileTypeIdentifier.ImageNews,
        //        EFileTypeIdentifier.PracticeImages,
        //        EFileTypeIdentifier.DefaultImageFiles,
        //        EFileTypeIdentifier.MockTestImage,
        //    };

        //    public const string KeyReplaceYear = "{year}";
        //    public const string KeyReplaceMonth = "{month}";
        //    public const string KeyReplaceId = "{id}";
        //    public const string KeyReplaceUserInput = "{userinput}";
        //    public const string KeyReplaceTimestamp = "{timestamp}";

        //    public const string FileIsEmptyKey = "file_is_empty_key";
        //    public const string FileTypeIsNotConfiguredKey = "file_type_is_not_configured_key";

        //    public const string RootPathFileStorage = "./data";
        //    public static string SubFolderPublic = "public"; //set in appSetting.json, config in ServiceCoreExtensions
        //    public static string SubFolderPrivate = "private";  //set in appSetting.json, config in ServiceCoreExtensions

        //    public const string FileExtensionNotValid = "{0}_must_be_{1}";
        //    public const string FileSizeNotValid = "{0}_must_be_less_than_{1}MB";
        //}

        public static class Common
        {
            public const int VerifyCodeLength = 6;
            public const string Comma = ",";
            public const string Dash = "-";
            public const string DateTimeHourFormat = "dd/MM/yyyy HH:mm:ss";
            public const string DateTimeHourNoSecondFormat = "dd/MM/yyyy HH:mm:ss";
            public const string DateTimeFormat = "dd/MM/yyyy";
            public const int ImageSizeMaximum = 5 * 1024 * 1024;
            public const int HourInVN = 7;
            public const long DefaultReceiptCode = 100000;
            public static EQuestionnaireType[] QuestionnaireTypeIsAllowedForMockTest = new EQuestionnaireType[]
            {
                EQuestionnaireType.MCQ,
                EQuestionnaireType.ImageDragDrop,
                EQuestionnaireType.Writing,
                EQuestionnaireType.Record,
                EQuestionnaireType.ReadTextALoud,
                EQuestionnaireType.Droplist,
                EQuestionnaireType.MCQImage,
                EQuestionnaireType.TrueFalse,
                EQuestionnaireType.FillInTheBlank,
                EQuestionnaireType.Matching,
                EQuestionnaireType.MatchingImage
            };

            public static class SWSign
            {
                public const string SpeakingSign = "S";
                public const string WritingSign = "W";
                public const string BreakLine = "\n";
            }
        }

        public static class CommonValidationMessages
        {
            public const string MinimumPageNumber = "page_number_must_be_greater_than_0";
            public const string MinimumPageSize = "page_size_must_be_greater_than_0";

            public const string ScoringDeadLineNotFound = "scoring_deadline_not_found";
        }

        public static class ValidationMessages
        {
            public static class WebUserMessage
            {
                public const string AccountDoesNotExist = "account_does_not_exist";
                public const string AccountInactive = "account_inactive";
                public const string CurrentPasswordInvalid = "current_password_invalid";
                public const string VerificationTokenInvalid = "verification_token_invalid";
                public const string VerificationTokenExpired = "verification_token_expired";
                public const string UserAlreadyExisted = "user_already_existed";
                public const string VerificationCodeIsUsedKey = "verification_code_is_used_key";
                public const string AvatarInvalidKey = "avatar_invalid_key";
                public const string UserIdDoesNotMatchKey = "user_id_does_not_match_key";
                public const string UserDeleteAvatarSuccessKey = "user_delete_avatar_success_key";
                public const string UserIdRequiredKey = "user_id_is_required_key";
                public const string UserInfoNotFound = "user_info_not_found";
                public const string UserNameRequiredKey = "user_name_is_required_key";
                public const string FullNameRequiredKey = "full_name_is_required_key";
                public const string PhoneNumberRequiredKey = "phone_number_is_required_key";
            }

            public static class RegisterUserMessage
            {
                public const string FullNameIsRequiredKey = "full_name_is_required_key";
                public const string FullNameMaximumLengthKey = "full_name_must_be_less_than_255_characters_key";
                public const string PhoneNumberIsRequiredKey = "phone_number_is_required_key";

                public const string PhoneNumberMaximumLengthKey = "phone_number_must_be_less_than_32_characters_key";
                public const string PhoneNumberInvalidFormatKey = "phone_number_invalid_format_key";

                public const string BirthdayIsRequiredKey = "birthday_is_required_key";

                public const string EmailIsRequiredKey = "email_is_required_key";
                public const string EmailMaximumLengthKey = "email_must_be_less_than_255_characters_key";
                public const string EmailInvalidFormatKey = "email_invalid_format_key";

                public const string PasswordIsRequiredKey = "password_is_required_key";
                public const string PasswordInvalidFormatKey = "password_invalid_format_key";
                public const string ConfirmPasswordIsRequiredKey = "confirm_password_is_required_key";
                public const string PasswordsDoNotMatchKey = "passwords_do_not_match_key";

                public const string UserEmailIsExistsKey = "user_email_is_exists_key";
                public const string UserPhoneIsExistsKey = "user_phone_is_exists_key";
                public const string UserNameIsExistsKey = "user_name_is_exists_key";

                public const string UserIsNotVerifiedByEmail = "user_is_not_verified_by_email";
                public const string CodeIsRequiredKey = "code_is_required_key";
                public const string CodeInvalidFormatKey = "code_invalid_format_key";
                public const string UserHasBeenVerifiedByEmailKey = "user_has_been_verified_by_email_key";
                public const string ResendAfter60SecondsKey = "resend_after_60_seconds_key";

                public const string UserIsInactivated = "user_is_inactivated";
                public const string GoogleIdIsRequiredKey = "google_id_is_required_key";
                public const string GoogleIdMaximumLengthKey = "google_id_must_be_less_than_255_characters_key";
                public const string FacebookIdMaximumLengthKey = "facebook_id_must_be_less_than_255_characters_key";
                public const string GenderMaximumLengthKey = "gender_must_be_less_than_1_character_key";
                public const string CurrentAddressMaximumLengthKey = "current_address_must_be_less_than_500_characters_key";
                public const string JobNameMaximumLengthKey = "job_name_must_be_less_than_255_characters_key";
            }

            public static class AuthMessage
            {
                public const string UserNameRequiredKey = "user_name_is_required_key";
                public const string PasswordRequiredKey = "password_is_required_key";

                public const string UserNotFoundKey = "user_not_found_key";
                public const string WrongPasswordKey = "wrong_password_key";
                public const string UserInActiveKey = "user_is_inactive_key";
                public const string UserIsLockedByAccessFailedKey = "user_is_locked_by_access_failed_key";

                public const string RefreshTokenRequiredKey = "refresh_token_is_required_key";
                public const string TokenRequiredKey = "token_is_required_key";

                public const string EmailRequiredKey = "email_is_required_key";
                public const string CodeRequiredKey = "code_is_required_key";
                public const string AccessTokenRequiredKey = "access_token_is_required_key";
                public const string FacebookIdRequiredKey = "facebook_id_is_required_key";
                public const string UserInfoIsRequired = "user_info_is_required_key";
                public const string EmailIsRequired = "email_is_required_key";
                public const string UserIsDeactivated = "user_is_deactivated";
                public const string UserForbidden = "user_forbidden";
                public const string EmailInvalidFormatKey = "email_invalid_format_key";
                public const string UserHasNotBeenVerifiedByEmailKey = "user_has_not_been_verified_by_email_key";
                public const string TokenResponseIsNull = "token_response_is_null";
            }

            public static class RefreshTokenMessage
            {
                public const string InvalidToken = "invalid_token";
                public const string TokenHasNotExpiredYet = "token_hasnt_expired_yet";
                public const string NotFoundRefreshToken = "not_found_refresh_token";
                public const string RefreshTokenHasExpired = "refresh_token_has_expired";
                public const string RefreshTokenIsUsed = "refresh_token_is_used";
                public const string RefreshTokenDoesNotMatchJwt = "refresh_token_does_not_match_jwt";
                public const string Success = "success";

                public const string InvalidKey = "invalid_key";
            }
            public static class AutoGenerateKeyCode
            {
                public const string UserInfoHasAlreadyBeenRegisted = "user_info_has_already_been_registed";

                public const string ParentNameIsRequiredKey = "parent_name_is_required_key";
                public const string ParentNameMaximumLengthKey = "parent_name_must_be_less_than_50_characters_key";

                public const string ParentPhoneNumberIsRequiredKey = "parent_phone_number_is_required_key";
                public const string ParentPhoneNumberMaximumLengthKey = "parent_phone_number_must_be_less_than_32_characters_key";
                public const string ParentPhoneNumberInvalidFormatKey = "parent_phone_number_invalid_format_key";

                public const string ParentEmailIsRequiredKey = "parent_email_is_required_key";
                public const string ParentEmailMaximumLengthKey = "parent_email_must_be_less_than_255_characters_key";
                public const string ParentEmailInvalidFormatKey = "parent_email_invalid_format_key";

                public const string NameIsRequiredKey = "name_is_required_key";
                public const string NameMaximumLengthKey = "name_must_be_less_than_255_characters_key";

                public const string BirthdayIsRequiredKey = "birthday_is_required_key";

                public const string GenderIsRequiredKey = "gender_is_required_key";

                public const string ProvinceIsRequiredKey = "province_is_required_key";
                public const string ProvinceMaximumLengthKey = "province_must_be_less_than_50_characters_key";

                public const string DistrictIsRequiredKey = "district_is_required_key";
                public const string DistrictMaximumLengthKey = "district_must_be_less_than_50_characters_key";

                public const string BlockIsRequiredKey = "block_is_required_key";
                public const string BlockMaximumLengthKey = "block_must_be_less_than_50_characters_key";

                public const string SchoolIsRequiredKey = "school_is_required_key";
                public const string SchoolMaximumLengthKey = "school_must_be_less_than_255_characters_key";

                public const string ContestIdIsRequiredKey = "contest_id_is_required_key";
            }

            public const string RequiredIds = "required_ids";
            public static class Writing
            {

                public static class MockTest
                {
                    public const string RequiredSetStrategyBeforeHandle = "required_set_strategy_before_handle";
                    public const string TypeIsNotDefined = "type_is_not_define";
                    public const string RequiredIdKey = "id_is_required_key";

                }
            }
            public static class Scoring
            {
                public const string NotFoundScoringByAI = "scoring_by_ai_not_found";
            }
        }

        public static class EmailTemplate
        {
            public const string FolderName = "./TemplateMails";
            public const string DiscountTemplateName = "discount.html";
            public const string LiveClassTeacherAccountName = "live-class-teacher-account.html";
            public const string LiveClassStudentAccountName = "live-class-student-account.html";
            public const string ResultExamToeflChallenge = "result-exam-toefl-challenge.html";
            public const string AccountBankAreAboutToExpire = "account_bank_are_about_to_expire.html";
            public const string AccountBankAreLowStock = "account_bank_are_low_stock.html";
            public const string SuperAdminGroupName = "SuperAdmin";
            public const string StudentAccountCreatedSuccessfully = "student-account_created_successfully.html";
            public const string ResultExamToeflChallengePrimaryUnder50 = "result-exam-toefl-challenge-primary-under-50.html";
            public const string ResultExamToeflChallengePrimaryOver50 = "result-exam-toefl-challenge-primary-over-50.html";
            public const string ResultExamToeflChallengeJuniorUnder50 = "result-exam-toefl-challenge-junior-under-50.html";
            public const string ResultExamToeflChallengeJuniorOver50 = "result-exam-toefl-challenge-junior-over-50.html";
        }

        public static class EmailSubjects
        {
            public const string EmailSubjectsToTeacher = "IIG Việt Nam - Thông báo tài khoản dạy trực tuyến Live-Class";
            public const string EmailSubjectsToStudent = "IIG Việt Nam - Thông báo tài khoản học trực tuyến Live-Class";
            public const string EmailSubjectsResultExamToeflChallenge = "TRẢ KẾT QUẢ THI {0}_Thí sinh {1}";
            public const string EmailSubjectsResultExamToeflChallengeJuniorOver50 = "Chúc mừng kết quả bài thi trải nghiệm trực tuyến TOEFL Junior Challenge 2023 – 2024 thí sinh {0} và thông tin vòng Vòng Tuyển chọn cấp thành phố Hà Nội";
            public const string EmailSubjectsResultExamToeflChallengeJuniorUnder50 = "Thông báo kết quả bài thi trải nghiệm thí sinh {0} & cơ hội cọ xát bản lĩnh, rèn luyện năng lực tại Vòng Tuyển chọn cấp thành phố Hà Nội.";
            public const string EmailSubjectsResultExamToeflChallengePrimaryOver50 = "Chúc mừng kết quả bài thi trải nghiệm trực tuyến TOEFL Primary Challenge 2023 – 2024 thí sinh {0} và thông tin vòng Vòng tuyển chọn cấp thành phố Hà Nội";
            public const string EmailSubjectsResultExamToeflChallengePrimaryUnder50 = "Thông báo kết quả bài thi trải nghiệm thí sinh {0} & cơ hội cọ xát bản lĩnh, rèn luyện năng lực tại Vòng tuyển chọn cấp thành phố Hà Nội.";
        }

        public static class ImportExportExcel
        {
            public const string FileIsNotCorrectFormat = "file_is_not_correct_format";
            public const string FileMustBeExcelFile = "file_must_be_excel_file";

            public static class MocktestExcel
            {
                public const int CheckedAI = 1;
                public const int UncheckedAI = 0;
                public const string HasCheckedAI = "Có";
            }
        }

        public static class VnPayResponseCode
        {
            public const string TransactionSuccessfully = "00";
            public const string OrderNotFound = "01";
            public const string OrderAlreadyConfirmed = "02";
            public const string InvalidAmount = "04";
            public const string CancelPayment = "24";
            public const string BankIsUnderMaintenance = "75";
            public const string InvalidSignature = "97";
            public const string OtherErrors = "99";
        }

        public static class AdminGroup
        {
            public const string SystemAdminGroupId = "3AB42B76-88C3-4C64-8122-8921C8A0CB13";
            public const string SystemAdminGroupName = "SystemAdmin";

            public const string TeacherGroupId = "5D9D0E5D-4220-44DD-B7DF-32CDECDBA8A1";
            public const string TeacherGroupName = "Giáo Viên";
        }

        public static class LiveClassAdminPermission
        {
            public const string GetListLiveClassPermission = ":LiveClassDetail:GetList";
            public const string GetLiveClassPermission = ":LiveClassDetail:GetById";
            public const string TeachLiveClassPermission = ":LiveLessonDetail:LiveClassGetLessonLinkUrl";
            public const string EditLiveClassPermission = ":LiveClassDetail:Update";
            public const string InsertLiveClassPermission = ":LiveClassDetail:Create";
            public const string DeleteLiveClassPermission = ":LiveClassDetail:DeleteLiveClassDetail";
        }

        public static class AdminUser
        {
            public const string SystemAdminUserName = "super_admin";
        }

        public static class KeyCode
        {
            public const int KeyCodeLength = 6;
        }

        public static class Course
        {
            public const int ScoreForEachCorrectAnswer = 10;
            public const int MinFinishPercentage = 60;
        }

        public static class RabbitMQ
        {
            public const string TopicPaymentOnline = "topic.payment.online";
            public const string TopicPaymentOffline = "topic.payment.offline";

            public const string QueuePaymentOnline = "queue.payment.online";
            public const string QueuePaymentOnlineRoutingkey = "*.payment.online";
            public const string PushMessagePaymentOnlineRoutingkey = "{0}.payment.online";

            public const string QueuePaymentOffline = "queue.payment.offline";
            public const string QueuePaymentOfflineRoutingkey = "*.payment.offline";
            public const string PushMessagePaymentOfflineRoutingkey = "{0}.payment.offline";

            //ScoringService
            public class ScoringService
            {
                public const string TopicGetLiveClassGroupScoring = "topic.scoring.get.live.class.group";
                public const string QueueGetLiveClassGroupScoring = "queue.scoring.get.live.class.group";
                public const string QueueGetLiveClassGroupScoringRoutingKey = "*.scoring.get.live.class.group";
                public const string PushMessageGetLiveClassGroupScoringRoutingKey = "{0}.scoring.get.live.class.group";

                public const string TopicGetGroupScoring = "topic.scoring.get.group.scoring";
                public const string QueueGetGroupScoring = "queue.scoring.get.group.scoring";
                public const string QueueGetGroupScoringRoutingKey = "*.scoring.get.group.scoring";
                public const string PushMessageGetGroupScoringRoutingKey = "{0}.scoring.get.group.scoring";
            }
           

          
        }

        public static class ClassinMessage
        {
            public const int SuccessNo = 1;
        }

        public static class NotificationSystems
        {
            public const string DiscountNotificationTitle = "Bạn đã nhận được mã giảm giá";
            public const string CartRouter = "/cart";
            public const string DiscountNotificationMessageVNI =
                "<p>Đừng bỏ lỡ! Bạn đã nhận được 1 voucher giảm giá <b>{{DiscountCode}}</b> nhân dịp {{DiscountName}}, mã sẽ có hiệu lực từ {{DiscountValidFromHour}} ngày {{DiscountValidFromDate}} đến {{DiscountValidToHour}} ngày {{DiscountValidToDate}}. Áp dụng ngay để không bỏ lỡ nhé.</p>";

            public const string MyCourseRouter = "/khoa-hoc-cua-toi";
            public const string ReviewedOrderNotificationTitle = "Kích hoạt khóa học";
            public const string ReviewedOrderNotificationMessageVNI =
                "<p>Đơn hàng {{OrderCode}} của bạn đã được kích hoạt. Truy cập <b>Khóa học của tôi</b> để kiểm tra khóa học ngay nhé.</p>";

            public const string MyCourseDetailRouter = "/khoa-hoc-cua-toi/{{CourseId}}";
            public const string CourseUnfinishedNotificationTitle = "Có khóa học chưa hoàn thành";
            public const string CourseUnfinishedNotificationMessageVNI =
                "<p>{{AccountName}} ơi, bạn có khóa học  <b>{{CourseName}}</b> chưa được hoàn thành. Đi đến <b>Chi tiết khóa học</b> để học ngay hôm nay nhé.</p>";
            public static class CourseScoringNotification
            {
                public const string StepName = "StepName";
                public const string UnitName = "UnitName";
                public const string UnitTestName = "UnitTestName";
                public const string CourseTestName = "CourseTestName";
                public const string CourseName = "CourseName";
                public const string Tool3rd = "Tool3rd";
                public const string KeyCode = "KeyCode";
                public const string MissionName = "MissionName";
                public const string LessonName = "LessonName";
                public const string FinalTestName = "FinalTestName";
                public const string ClassName = "ClassName";

                public const string TrailMessage = " đã được chấm. Xem kết quả ngay.";
                public const string TrailUpdateMessage = "mới được cập nhật. Xem kết quả ngay.";
                public const string HeadPracticeMessage = "Bài tập ";
                public const string HeadTestMessage = "Bài kiểm tra ";
                public const string HeadMissionMessage = "Nhiệm vụ ";
                public const string In = " trong ";
                public const string Of = " của ";
                public const string WithCode = " với mã code ";
                public const string Code = " Mã code ";

                public const string StepMessage = $"{HeadPracticeMessage}{StepName}{In}{UnitName}";
                public const string UnitTestMessage = $"{HeadTestMessage}{UnitTestName}{In}{UnitName}";
                public const string CourseTestMessage = $"{HeadTestMessage}{CourseTestName}{In}{CourseName}";
                public const string MockTestMessage = $"{Tool3rd}{WithCode}{KeyCode}";
                public const string MissionMessage = $"{HeadMissionMessage}{MissionName}{In}{LessonName}";
                public const string FinalTestMessage = $"{HeadTestMessage}{FinalTestName}{Of}{ClassName}";

                public const string ApproveTittle = "Bài tập của bạn đã được chấm";
                public const string ReApproveTitle = "Cập nhật kết quả chấm";
                public const string ViewResultPracticeRoute = "khoa-tu-hoc/study-course/CourseId?{0}";


                public const string CourseTestId = nameof(CourseTestId);
                public const string ViewResultCourseTestRoute = $"course-test/exam/{CourseTestId}/result";

                public const string CourseId = nameof(CourseId);
                public const string StepId = nameof(StepId);
                public const string UnitId = nameof(UnitId);
                public const string LessonId = nameof(LessonId);
                public const string QuestionnaireId = nameof(QuestionnaireId);
                public const string notiType = nameof(notiType);
                public const string ViewResultStepTestRoute = "course/CourseId/step/StepId/result";

                public const string UnitTestId = nameof(UnitTestId);
                //public const string ViewResultUnitTestRoute = "course/CourseId/unit-test/UnitTestId/result";
                public const string ViewResultUnitTestRoute = "khoa-tu-hoc/study-course/CourseId?UnitTestId";

                public const string MissionId = nameof(MissionId);
                public const string ViewResultLiveClassRoute = "live-class-mission/MissionId/result";

                //DetailTestId == final test
                public const string LiveClassDetailTestId = nameof(LiveClassDetailTestId);
                public const string FinalTestViewResultLink = "live-class-test/exam/LiveClassDetailTestId/view-result";

                public const string ViewResultMocktestRoute = "thi-thu-online/exam/KeyCode/result";
                public const string courseId = nameof(courseId);
                
            }
        }

        public static class LiveLessonDetailMessage
        {
            public const string LiveLessonDetailCannotGetLessonLinkUrl = "live_lesson_detail_cannot_get_lesson_link_url";
            public const int LiveLessonDetailTeacherMustHavePhoneNumberErrNo = 467;
            public const int LiveLessonDetailTeacherDoesNotHaveAccessErroNo = 150; //Expression no access to the phone number login key（This cell phone number does not belong to the student or teacher at the school）
            public const string UserDoesNotHaveAccessToClassinLesson = "user_does_not_have_access_to_classin_lesson";
            public const string LiveLessonDetailTeacherMustHavePhoneNumber = "live_lesson_detail_teacher_must_have_phone_number";
            public const string LiveLessonDetailMustHavePhoneNumber = "live_lesson_detail_student_must_have_phone_number";
            public const int LiveLessonDetailSuccessNo = 1;
        }
        public static class ToeflChallengeContest
        {
            public const string Upcoming = "Sắp diễn ra";
            public const string Happenning = "Đang diễn ra";
            public const string Finished = "Đã diễn ra";


        }
        public static class ToeflChallengeScoreComment
        {
            public const string ScoreCommentDoesNotExist = "score_comment_does_not_exist";

        }

        public static class MockTestType
        {
            public const string ToeflPrimaryChallenge = "TOEFL Primary Challenge";
            public const string ToeflJuniorChallenge = "TOEFL Junior Challenge";
        }

        public static class ScoreToeflChallengerToSendEmail
        {
            public const int PassingScore = 50;
        }

        public static class LiveClassTypeName
        {
            public const string PreMission = "Nhiệm vụ trước buổi học";
            public const string AfterMission = "Nhiệm vụ sau buổi học";

        }

        public static class Permissions
        {
            public static class Scoring
            {
                public const string CourseScoring = ":CourseScoring"; // Chấm điểm lớp tự học
                public const string CourseScoringAssign = ":CourseScoring:Assign"; // Được giao việc
                public const string CourseScoringEvaluate = ":CourseScoring:Evaluate"; // Chấm điểm
                public const string CourseScoringRead = ":CourseScoring:Read"; // Xem
                public const string CourseScoringUpdate = ":CourseScoring:Update"; // Sửa

                public const string LiveScoring = "::LiveScoring"; // Chấm điểm lớp tự học
                public const string LiveScoringScoringAssign = ":LiveScoring:Assign"; // Được giao việc
                public const string LiveScoringScoringEvaluate = ":LiveScoring:Evaluate"; // Chấm điểm
                public const string LiveScoringScoringRead = ":LiveScoring:Read"; // Xem
                public const string LiveScoringScoringUpdate = ":LiveScoring:Update"; // Sửa
            }
        }

        public static class MongoCollectionName
        {
            public const string Questionnaires = "questionnaires";
            public const string LeftSections = "leftSections";
            public const string Questions = "questions";
            public const string UserSubmitLesson = "userSubmitLessons";
            public const string MocktestKeyCode = "mockTestKeyCodes";
            public const string KeyCodeAnswers = "keyCodeAnswers";
            public const string Mocktests = "mocktests";
            public const string CourseTestAnswers = "courseTestAnswers";
            public const string UnitTestAnswers = "unitTestAnswers";
            public const string ScoringAI = "scoringByAI";
        }

        public static class LastUpdatedType
        {
            public const string Student = "Học viên";
            public const string System =  "Hệ Thống";

        }

        
    }
}