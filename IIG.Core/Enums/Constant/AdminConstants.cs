
namespace IIG.Core.Enums;

public static partial class AdminConstants
{
    public static class AdminCronExpression
    {
        public const string EveryMinute = "* * * * *";
        public const string EveryHour = "0 * * * *";
        /// <summary>
        /// Every day at 18:00 UTC - 2:00 AM (UTC+7)
        /// </summary>
        public const string EveryDay = "0 18 * * *";
        public const string EveryWeek = "0 0 * * 0";
        public const string EveryMonth = "0 0 1 * *";
    }
    public static class FileType
    {
        public const string ExcelContentType = "application/vnd.ms-excel";
    }

    public static class ExcelCourse
    {
        public const int MaxSortOrderNumberCourseDetail = 7;
        public const int MinSortOrderNumberCourseDetail = 1;

        public const string FileExportName = "Report";
        public const int MaxNumberTeacherInCourse = 10;

    }

    public static class ExcelAdminUser
    {
        public const int MaxLengthAdminUserFullName = 255;
        public const int MaxLengthAdminUserUserName = 255;
        public const int MaxLengthAdminUserEmail = 255;
        public const int MaxLengthPhoneNumber = 32;
        public const int TimeDelayBetweenSendEmailPasswordInSeconds = 20;
        public const string FileExportName = "Report";
    }
    public static class ExcelExamToolAccountLevelManagement
    {
        public const string FileExportName = "Quản lý cấp tài khoản công cụ";
    }
    public static class ExcelManageCourseStudent
    {
        public const string FileExportNameList = "Danh sách học viên";
        public const string FileExportHistoryActionList = "Lịch sử thao tác";
    }

    public static class ExcelAdminWebUser
    {
        public const int MaxLengthAdminWebUserFullName = 255;
        public const int MaxLengthAdminWebUserUserName = 255;
        public const int MaxLengthAdminWebUserEmail = 255;
        public const int MaxLengthAdminWebUserPhoneNumber = 32;
        public const int MaxLengthAdminWebUserAddress = 500;
        public const int MaxLengthAdminWebUserJobName = 255;
        public const string FileExportName = "Report";
        public static int MinLengthAdminWebUserPassword = 8;
        public static int MaxLengthAdminWebUserPassword = 15;
    }

    public static class ExcelQuestionnaire
    {
        public const string FileExportName = "Report";
        public const string QuestionnaireActive = "Đang hoạt động";
        public const string QuestionnaireNotActive = "Không hoạt động";
    }

    public static class RankingScoreImportExport
    {
        public static class GroupScore
        {
            public const string STTKey = "STT";
            public const string GroupNameKey = "Tên nhóm";
            public const string FromQuestionNumberKey = "Từ câu hỏi";
            public const string ToQuestionNumberKey = "Đến câu hỏi";
            public const string MinScore = "Điểm tối thiểu";
            public const string MaxScore = "Điểm tối đa";
            public const string ErrorMessage = "Message lỗi";
        }

        public static class ScoreRange
        {
            public const string STTKey = "STT";
            public const string ExtractScore = "Điểm chính xác";
            public const string FromScore = "Từ điểm";
            public const string ToScore = "Đến điểm";
            public const string ErrorMessage = "Ghi chú";
        }
    }

    public static class ExcelOrder
    {
        public const string FileExportName = "Report";
        public const int MaxLengthDiscountCode = 6;
    }

    public static class ExcelMockTestKeyCode
    {
        public const string FileExportName = "Report";
    }

    public static class ExcelReceiptInvoice
    {
        public const string FileExportName = "Export Danh sách xuất hóa đơn";
    }


    public static class ExcelMockTest
    {
        public const string FileExportName = "Report";
    }


    public static class CourseTeacher
    {
        public const int MaxCourseTeacherInCourse = 10;
    }

    public static class CourseDetail
    {
        public const int MaxCourseDetailInCourse = 7;
    }

    public static class QuestionnaireGroup
    {
        public const int MaxLevel = 4;
    }

    public static class CourseValidateMaxLength
    {
        public const int MaxLengthCourseName = 255;
        public const int MaxLengthCourseCode = 128;
        public const int MaxLengthCourseCategory = 128;
        public const int MaxLengthCourseDescription = 500;
        public const int MaxLengthCourseNumberOfAssignment = 128;
        public const int MaxLengthCourseNumberOfStudent = 128;
        public const int MaxLengthCourseNumberOfVideo = 128;
        public const int MaxLengthCourseUserGuideLink = 255;
        public const int MaxLengthCourseMetaKeywords = 500;
        public const int MaxLengthCourseTags = 1000;

        public const int MaxLengthCoursePriceName = 128;

        public const int MaxLengthCourseDetailTabName = 128;
        public const int MaxLengthCourseDetailTitle = 255;

        public const int MaxLengthCourseTeacherName = 128;
        public const int MaxLengthCourseTeacherUniversity = 255;
        public const int MaxLengthCourseTeacherDescription = 500;
    }

    public static class QuestionnaireGroupValidateMaxLength
    {
        public const int MaxLengthName = 255;
        public const int MaxLengthIdentifier = 10;
    }

    public static class QuestionnaireValidateMaxLength
    {
        public const int MaxLengthName = 255;
        public const int MaxLengthIntroContent = 128;
    }


    public static class MockTestValidateMaxLength
    {
        public const int MaxLengthName = 255;
        public const int MaxLengthTypeName = 255;
        public const int MaxLengthObjectName = 255;
        public const int MaxLengthTextViewMore = 225;
        public const int MaxLengthLinkUrlViewMore = 500;
        public const int MaxLengthTag = 1000;
        public const int MaxItemTags = 10;
    }

    public static class MockTestSectionValidateMaxLength
    {
        public const int MaxLengthName = 255;
        public const int MaxLengthRankingScoreName = 500;
    }

    public static class MockTestPartValidateMaxLength
    {
        public const int MaxLengthName = 255;
    }

    public static class LeftSectionValidateMaxLength
    {
        public const int MaxLengthTitle = 255;
    }

    public static class QuestionValidateMaxLength
    {
        public const int MaxLengthName = 500;
        public const int MaxLengthFakeValues = 255;
        public const int MaxLengthDisplayTimestamp = 30;
        public const int MaxLengthWordEnglish = 500;
        public const int MaxLengthWordPhonetic = 50;
        public const int MaxLengthTextEnglish = 500;

        public const int MaxLengthNameInFlashCardQuestion = 128;
    }
    public static class ToeflChallengeValidateMaxLength
    {
        public const int MaxLengthName = 50;
        public const int MaxLengthToeflChallengeContest = 128;
    }

    public static class QuestionTranslationValidateLength
    {
        public const int MaxLengthWordTranslation = 500;
    }

    public static class QuestionValidateConstant
    {
        public const int MinQuestionInImageDragDropQuestion = 1;
        public const int MinQuestionInDropListQuestion = 1;
        public const int MinQuestionInMCQQuestion = 1;

        public const int MinQuestionInFlashCardQuestion = 1;

        public const int MinAnswerInImageDragDropQuestion = 1;
        public const int MinAnswerInVideoQuestion = 2;
        public const int MinAnswerCorrectInVideoQuestion = 1;
        public const int MinAnswerInMCQQuestion = 2;
        public const int MinAnswerCorrectInMCQQuestion = 1;
        public const int MinAnswerDropListQuestion = 2;
        public const int MinAnswerInMCQImageQuestion = 2;
        public const int MinAnswerCorrectInMCQImageQuestion = 1;

        public const int MinQuestionInTrueFalseQuestion = 1;
        public const int NumberAnswerInTrueFalseQuestion = 2;
        public const int NumberAnswerCorrectInTrueFalseQuestion = 1;
        public const int MinQuestionInFillInTheBlankQuestion = 1;
        public const int MinAnswerInFillInTheBlankQuestion = 1;
        public const int MinQuestionInMatchingQuestion = 1;
        public const int MinMatchingQuestionInMatchingQuestion = 1;
    }

    public static class AnswerValidateMaxLength
    {
        public const int MaxLengthName = 300;
        public const int MaxLengthMatchingKey = 50;
        public const int MaxLengthCorrectMatchingValues = 255;
        public const int MaxLengthFakeSelectValues = 255;
        public const int MaxLengthNameMatchingQuestion = 500;
        public const int MaxLengthNameMatchingAnswer = 500;
        public const int MaxLengthAnswer = 255;
        public const int MaxLengthCorrectAnswer = 128;
    }

    public static class AccountBankValidateMaxLength
    {
        public const int MaxLengthKeycode = 255;
        public const int MaxLengthUsername = 255;
        public const int MaxLengthPassword = 128;
    }

    public static class MockTestKeyCodeValidateMaxLength
    {
        public const int MaxLengthIdentificationNumber = 128;
        public const int MaxLengthFullName = 128;
        public const int MaxlengthChallengeName = 255;
    }

    public static class LiveClassDetailValidateMaxLength
    {
        public const int MaxLengthName = 40;
        public const int MaxAdminUserInLiveClassDetail = 6;
    }

    public static class LiveLessonDetailValidateMaxLength
    {
        public const int MaxLengthName = 50;
    }

    public static class LiveClassTypeValidateMaxLength
    {
        public const int MaxLengthName = 255;
    }

    public static class MockTestValidateConstant
    {
        // in seconds
        public const int MaxDifferentTimeAllow = 600;
    }

    public static class MockTestJobConstant
    {
        public const string MockTestJobIdTemplate = "publish_mocktest_{0}";
    }

    public static class BannerValidationConstant
    {
        public const int MaxBannerHomeNumber = 10;
        public const int MaxBannerIIG = 1;
    }

    public static class FeedbackValidationConstant
    {
        public const int MaxNumber = 10;
    }
    public static class ToeflChallengeUser
    {
        public const string Listening = "Listening";
        public const string Reading = "Reading";
        public const string Stucture = "Structure and Written Expression";
        public const string ExcelDefaultName = "Sheet";
    }
    public static class ToeflChallengeContest
    {
        public const string Upcoming = "Sắp diễn ra";
        public const string Happenning = "Đang diễn ra";
        public const string Finished = "Đã diễn ra";
    }

    public static partial class ValidationMessages
    {
        public static class AdminGroupMessage
        {
            public const string AdminGroupInUse = "admin_group_in_use";
            public const string AdminGroupNotFound = "admin_group_not_found";
            public const string AdminGroupNameExistsKey = "admin_group_name_exists_key";
            public const string AdminGroupNameRequiredKey = "admin_group_name_required_key";
            public const string AdminGroupIsActive = "admin_group_is_active";
            public const string AdminGroupTeacherCanNotDeleteOrChange = "admin_group_teacher_can_not_delete_or_change";
            public const string AdminGroupSystemAdminCanNotDeleteOrChange = "admin_group_system_admin_can_not_delete_or_change";
        }

        public static class AdminUserMessage
        {
            public const string AdminUserNotFound = "admin_user_not_found";
        }

        public static class CategoryMessage
        {
            public const string CategoryNotFoundKey = "category_not_found_key";
            public const string CategoryIsExists = "category_is_exists";
            public const string CategoryNameIsExists = "category_name_is_exists";
            public const string CategoryIsUsed = "category_is_used";
            public const string CategoryCannotAssignParent = "category_cannot_assign_parent";
            public const string NameIsRequired = "name_is_required";
            public const string NameInvalidFormat = "name_must_be_less_than_128_characters";
            public const string LinkInvalidFormat = "link_url_must_be_less_than_255_characters";
            public const string DescriptionInvalidFormat = "description_must_be_less_than_500_characters";
            public const string ActiveStatusIsRequired = "active_status_is_required";
            public const string SortOrderIsRequired = "category_sort_order_is_required";
            public const string CategoryTypeIsRequired = "category_type_is_required";
            public const string LevelIsRequired = "level_is_required";
            public const string IdentifierIsRequired = "identifier_is_required";
            public const string CategoryNotFound = "category_not_found";
            public const string TranslationIsRequired = "translation_is_required";

            // image
            public const string CategoryImageFileIdNotCorrectKey = "category_image_file_id_is_not_correct_key";
        }

        public static class CategoryAreaMessage
        {
            public const string AreaCodeIsRequired = "area_code_is_required";
            public const string AreaCodeInvalidFormat = "area_code_must_be_less_than_50_characters";
            public const string SortOrderIsRequired = "area_sort_order_is_required";
        }

        public static class UserMessage
        {
            public const string UserNameIsRequiredKey = "user_name_is_required_key";
            public const string EmailIsRequired = "email_is_required";
            public const string BirthdayIsRequired = "birthday_is_required";
            public const string FirstNameIsReuquired = "first_name_is_required";
            public const string FullNameIsRequired = "full_name_is_required";
            public const string LastNameIsRequired = "last_name_is_required";
            public const string PhoneNumberIsRequired = "phone_number_is_required";

            public const string PasswordRequiredKey = "password_is_required_key";
            public const string PasswordMustBeAtLeast8Characters = "password_must_be_at_least_8_characters";
            public const string ConfirmPasswordIsRequiredKey = "confirm_password_is_required_key";
            public const string PasswordsDoNotMatchKey = "passwords_do_not_match_key";

            public const string CodeIsRequiredKey = "code_is_required_key";
            public const string CodeInvalidFormatKey = "code_invalid_format_key";

            public const string VerificationTokenInvalid = "verification_token_invalid";
            public const string VerificationTokenExpired = "verification_token_expired";
            public const string VerificationCodeIsUsedKey = "verification_code_is_used_key";
            public const string NewPasswordsMustBeDifferentFromCurrentPassword = "new_passwords_must_be_different_from_current_password";

            public const string EmailMaximumLengthKey = "email_must_be_less_than_50_characters";
            public const string PhoneNumberMaximumLengthKey = "phone_number_must_be_less_than_15_characters_key";
            public const string FirstnameMaximumLengthKey = "first_name_must_be_less_than_50_characters_key";
            public const string FullNameMaximumLengthKey = "full_name_must_be_less_than_255_characters_key";
            public const string LastnameMaximumLengthKey = "last_name_must_be_less_than_50_characters_key";

            public const string EmailInvalidFormatKey = "email_invalid_format_key";
            public const string PhoneInvalidFormatKey = "phone_invalid_format_key";
        }

        public static class ImportExportExcelMessage
        {
            public const string CourseCodeIsExisted = "course_code_is_existed";
            public const string CourseNameAlreadyExistedKey = "course_name_already_existed_key";
            public const string NumberOfTeacherInCourseMustBeLessThan10 = "number_of_teacher_in_course_must_be_less_than_10";
            public const string PriceMustBeNumber = "price_must_be_number";
            public const string SalePriceMustBeNumber = "sale_price_must_be_number";
            public const string SaleValidFrom = "sale_valid_from_invalid_format";
            public const string SaleValidTo = "sale_valid_to_invalid_format";
            public const string SortOrderMustBeNumber = "sort_order_must_be_number";
            public const string SortOrderInvalid = "sort_order_invalid";
            public const string CourseCodeAndSortOrderMustBeUnique = "course_code_and_sort_order_must_be_unique";
            public const string CategoryIdentifierIsNotExisted = "category_identifier_is_not_existed";
            public const string AdminGroupNameIsNotExisted = "admin_group_name_is_not_existed";
            public const string EmailIsExisted = "email_is_existed";
            public const string UserNameIsExisted = "user_name_is_existed";
            public const string AdminUserFullNameMustBeLessThan255 = "admin_user_full_name_must_be_less_than_255_characters";
            public const string AdminUserUserNameMustBeLessThan255 = "admin_user_user_name_must_be_less_than_255_characters";
            public const string AdminUserEmailMustBeLessThan255 = "admin_user_email_must_be_less_than_255_characters";
            public const string AdminUserPhoneNumberMustBeLessThan32 = "admin_user_phone_number_must_be_less_than_32_characters";
            public const string BirthdayInvalidFormat = "birthday_invalid_format";
            public const string PriceIsRequiredKey = "price_is_required_key";
            public const string NumberOfMonthMustBeNumber = "number_of_month_must_be_number";
            public const string SaleValidFromIsRequiredKey = "sale_valid_from_is_required_key";
            public const string SaleValidToIsRequiredKey = "sale_valid_to_is_required_key";
            public const string CourseCodeAndCoursePriceNameMustBeUnique = "course_code_and_course_price_name_must_be_unique";
            public const string QuestionnaireGroupIdentifierIsNotExisted = "questionnaire_group_identifier_is_not_existed";
            public const string SalePriceMustBeGreaterThanZero = "sale_price_must_be_greater_than_0";

            // import web user
            public const string ImportWebUserRequiredKeyMessage = "information_is_required"; // Đây là thông tin bắt buộc
            public const string ImportWebUserInvalidFormatMessage = "information_invalid_format"; // Vượt quá số kí tự cho phép
            public const string ImportWebUserBirthdayInvalidFormatMessage = "import_web_user_birthday_invalid_format_01"; // Ngày sinh không đúng định dạng. Định dạng Ngày sinh phải là DD/MM/YYYY
            public const string ImportWebUserBirthdayInvalidFormat2Message = "import_web_user_birthday_invalid_format_02"; // Ngày sinh phải nhỏ hơn ngày hiện tại
            public const string ImportWebUserPhoneNumberAlreadyExistedMessage = "import_web_user_phone_number_already_existed"; // Số điện thoại đã tồn tại
            public const string ImportWebUserEmailAlreadyExistedMessage = "import_web_user_email_already_existed"; // Email đã tồn tại
            public const string ImportWebUserPhoneNumberIsDuplicatedInFileMessage = "import_web_user_phone_number_already_existed_in_file"; // Số điện thoại trùng trong file
            public const string ImportWebUserEmailIsDuplicatedInFileMessage = "import_web_user_email_already_existed_in_file"; // Email trùng trong file
            public const string ImportWebUserPhoneNumberInvalidFormatMessage = "import_web_user_phone_number_invalid_format"; // Số điện thoại không đúng định dạng. Số điện thoại phải có ít hơn 15 ký tự bao gồm số
            public const string ImportWebUserPasswordInvalidFormatMessage = "import_web_user_password_invalid_format"; // Mật khẩu không đúng định dạng. Mật khẩu phải có từ 8-15 ký tự, bao gồm chữ, số và ký tự đặc biệt.
            public const string ImportWebUserEmailInvalidFormatMessage = "import_web_user_email_invalid_format"; // Email không đúng định dạng
            public const string ImportWebUserGenderInvalidFormatMessage = "import_web_user_gender_invalid_format"; // Giới tính chỉ bao gồm các giá trị sau: Nam, Nữ, Khác

            public const string FullNameIsRequiredKey = "full_name_is_required_key";
            public const string PhoneNumberIsRequiredKey = "phone_number_is_required_key";
            public const string EmailIsRequiredKey = "email_is_required_key";
            public const string UserNameIsRequiredKey = "user_name_is_required_key";
            public const string PhoneNumberInvalidFormat = "phone_number_invalid_format";
            public const string EmailInvalidFormat = "email_invalid_format";
            public const string GenderIsRequiredKey = "gender_is_required_key";
            public const string AdminWebUserFullNameMustBeLessThan255 = "admin_web_user_full_name_must_be_less_than_255_characters";
            public const string AdminWebUserEmailMustBeLessThan255 = "admin_web_user_email_must_be_less_than_255_characters";
            public const string AdminWebUserJobNameMustBeLessThan255 = "admin_web_user_job_name_must_be_less_than_255_characters";
            public const string AdminWebUserAddressMustBeLessThan500 = "admin_web_user_address_must_be_less_than_500_characters";
            public const string AdminWebUserPhoneNumberMustBeLessThan32 = "admin_web_user_phone_number_must_be_less_than_32_characters";
            public const string PhoneNumberIsExisted = "phone_number_is_existed";
            public const string PhoneNumberIsDuplicated = "phone_number_is_duplicated";
            public const string EmailIsDuplicated = "email_is_duplicated";
            public const string UserNameIsDuplicated = "user_name_is_duplicated";
            public const string PasswordInvalidFormatKey = "password_invalid_format_key";
            public const string EmailNotExisted = "email_not_existed";
            public const string DiscountCodeNotExisted = "discount_code_not_existed";
            public const string CourseCodeNotExisted = "course_code_not_existed";
            public const string CoursePriceIdNotExisted = "course_price_id_not_existed";
        }

        public static class CourseMessage
        {
            public const string CourseIdAlreadyExistedKey = "course_id_already_existed_key";
            public const string CourseNotFoundKey = "course_not_found_key";
            public const string CourseIdIsRequiredKey = "course_id_is_required_key";
            public const string CourseIdInvalidFormatKey = "course_id_invalid_format_required_key";
            public const string CourseCodeIsRequiredKey = "course_code_is_required_key";
            public const string CourseCodeMaximumLengthKey = "course_code_must_be_less_than_128_characters_key";
            public const string CourseCategoryMaximumLengthKey = "course_category_must_be_less_than_128_characters_key";
            public const string CourseCategoryIsRequiredKey = "course_category_is_required_key";
            public const string CourseCodeAlreadyExistedKey = "course_code_already_existed_key";
            public const string CourseNameAlreadyExistedKey = "course_name_already_existed_key";
            public const string CourseNameIsRequiredKey = "course_name_is_required_key";
            public const string CourseNameMaximumLengthKey = "course_name_must_be_less_than_255_characters_key";
            public const string CourseDescriptionIsRequiredKey = "course_description_name_is_required_key";
            public const string CourseDescriptionMaximumLengthKey = "course_description_must_be_less_than_500_characters_key";
            public const string CourseCategoryIdInvalidKey = "course_category_id_invalid_key";
            public const string CourseNumberOfAssignmentMaximumLengthKey = "course_number_of_assignment_must_be_less_than_128_characters_key";
            public const string CourseNumberOfStudentMaximumLengthKey = "course_number_of_student_must_be_less_than_128_characters_key";
            public const string CourseNumberOfVideoMaximumLengthKey = "course_number_of_video_must_be_less_than_128_characters_key";
            public const string CourseUserGuideLinkMaximumLengthKey = "course_user_guide_link_must_be_less_than_255_characters_key";
            public const string CourseMetaKeywordsMaximumLengthKey = "course_meta_keywords_must_be_less_than_500_characters_key";
            public const string CourseTagsMaximumLengthKey = "course_meta_keywords_must_be_less_than_1000_characters_key";
            public const string CourseStatusIsRequiredKey = "course_status_is_required_key";
            public const string CourseStatusInvalidFormatKey = "course_status_invalid_format_key";
            public const string CourseDetailTabNameExistedKey = "course_detail_tab_name_is_required_key";
            public const string CourseDetailTabNameMaximumLengthKey = "course_detail_tab_name_must_be_less_than_128_characters_key";
            public const string CourseDetailTitleMaximumLengthKey = "course_detail_title_must_be_less_than_128_characters_key";
            public const string CoursePriceNameIsRequiredKey = "course_price_name_is_required_key";
            public const string CoursePriceNameMaximumLengthKey = "course_price_name_must_be_less_than_128_characters_key";
            public const string CourseTeacherNameIsRequiredKey = "course_teacher_name_is_required_key";
            public const string CourseTeacherNameMaximumLengthKey = "course_teacher_name_must_be_less_than_128_characters_key";
            public const string CourseTeacherUniversityMaximumLengthKey = "course_teacher_university_must_be_less_than_255_characters_key";
            public const string CourseTeacherDescriptionMaximumLengthKey = "course_teacher_description_must_be_less_than_500_characters_key";
            public const string IsFreeLearningUnit1Lesson1Invalid = "is_free_learning_unit_1_lesson_1_invalid";
            public const string IsHotInvalid = "is_hot_invalid";
            public const string CourseCannotDelete = "course_cannot_delete";

            //course image
            public const string CourseImageFileNameInvalidKey = "course_image_file_name_invalid_key";

            public const string CourseImageExtensionInvalidKey = "course_image_extension_invalid_key";
            public const string CourseImageStorageLocationInvalidKey = "course_image_storage_location_invalid_key";
            public const string CourseImageFileTypeInvalidKey = "course_image_file_type_id_invalid_key";

            public const string CourseImageFileNotFoundKey = "course_image_file_not_found_key";
            public const string CourseImageFileIdIsNotCorrectKey = "course_image_file_id_is_not_correct_key";

            //status
            public const string CourseStatusCannotChangeToActive = "course_status_cannot_change_to_active";
            public const string CourseStatusCannotChangeToInActive = "course_status_cannot_change_to_inactive";
            public const string CourseStatusCannotChangeToStopWorking = "course_status_cannot_change_to_stop_working";
            public const string CourseStatusNotValid = "course_status_is_not_valid";

            // translation
            public const string TranslationIsRequired = "course_translation_is_required";

            public const string NameIsRequired = "course_name_is_required";

            public const string CourseIsNotActiveKey = "course_is_not_active_key";
        }

        public static class CoursePriceMessage
        {
            // course price
            public const string CoursePriceNotFoundKey = "course_price_not_found_key";

            public const string CoursePriceInUse = "course_price_in_use";
            public const string CoursePriceNameIsRequiredKey = "course_price_name_is_required_key";
            public const string CoursePriceNameMaximumLengthKey = "course_price_name_must_be_less_than_128_characters_key";
            public const string CoursePriceCourseIdIsRequiredKey = "course_price_course_id_is_required_key";
            public const string CoursePricePriceIsRequiredKey = "course_price_price_is_required_key";
            public const string CoursePricePriceShouldNotBeLessThanZeroKey = "course_price_price_should_not_be_less_than_zezo_key";

            public const string CoursePricePriceShouldNotBeLessThanOneKey = "course_price_should_not_be_less_than_1_key"; // Giá trị phải lớn hơn hoặc bằng 1
            public const string CoursePriceSaleValidFromNotValid = "course_price_sale_valid_from_not_valid_key";
            public const string CoursePriceSaleValidToNotValid = "course_price_sale_valid_to_not_valid_key";
            public const string CoursePriceSalePriceMuseBeLessThanPrice = "course_price_sale_price_must_be_less_than_price";
            public const string CoursePriceNameIsExisted = "course_price_name_is_existed";
            public const string CoursePriceName = "course_price_name";
            public const string CoursePriceSaleValidFromMustBeLessThanSaleValidTo = "course_price_sale_valid_from_must_be_less_than_sale_valid_to";

            public const string CoursePriceCountInCourseInvalidFormat = "course_must_have_at_least_1_price_and_content";

            // translation
            public const string TranslationIsRequired = "course_price_translation_is_required";

            // exam tool
            public const string ExamToolCategoryIdIsRequiredKey = "course_price_exam_tool_category_id_is_required_key";
            public const string ExamToolCategoryIdNotFoundKey = "course_price_exam_tool_category_id_not_found";

            // mock test
            public const string NoMockTestSelected = "no_mocktest_selected"; // required mocktest

            // learn with teacher
            public const string ClassTypeIdIsRequiredKey = "course_price_class_type_id_is_required_key";
            public const string ClassTypeIdNotFoundKey = "course_price_class_type_id_is_not_found";

            // image
            public const string CoursePriceImageFileIdNotCorrectKey = "course_price_image_file_id_is_not_correct_key";
        }

        public static class CourseDetailMessage
        {
            public const string CourseDetailNotFound = "course_detail_id_not_found";
            public const string CourseIdIsRequired = "course_id_is_required";
            public const string TabNameIsRequired = "tab_name_is_required";
            public const string TabNameInvalidFormat = "tab_name_must_be_less_than_128";
            public const string TitleInvalidFormat = "title_must_be_less_than_255";
            public const string SortOrderIsRequired = "sort_order_is_required";
            public const string CourseDetailExceed7Records = "course_detail_exceed_7_records";

            public const string CourseDetailCountInCourseInvalidFormat = "course_must_have_at_least_1_price_and_content";

            // translation
            public const string TranslationIsRequired = "course_detail_translation_is_required";
        }

        public static class CourseTeacherMessage
        {
            public const string CourseTeacherIdIsRequired = "course_teacher_id_is_required";
            public const string CourseTeacherNotFound = "course_teacher_id_not_found";
            public const string CourseIdIsRequired = "course_id_is_required";
            public const string NameIsRequired = "teacher_name_is_required";
            public const string NameInvalidFormat = "teacher_name_must_be_less_than_128";
            public const string UniversityInvalidFormat = "university_must_be_less_than_255";
            public const string DescriptionInvalidFormat = "description_must_be_less_than_500";
            public const string AvatarUrlInvalidFormat = "avatar_url_must_be_less_than_255";
            public const string CourseTeacherImageInvalidKey = "course_teacher_image_invalid_key";

            //teacher image
            public const string CourseTeacherImageFileNameInvalidKey = "course_teacher_image_file_name_invalid_key";

            public const string CourseTeacherImageExtensionInvalidKey = "course_teacher_image_extension_invalid_key";
            public const string CourseTeacherImageStorageLocationInvalidKey = "course_teacher_image_storage_location_invalid_key";
            public const string CourseTeacherImageFileTypeInvalidKey = "course_teacher_image_file_type_id_invalid_key";
            public const string CourseTeacherExceed10Records = "course_teacher_exceed_10_records";
            public const string CourseTeacherImageFileNotFoundKey = "course_teacher_image_file_not_found_key";
            public const string CourseTeacherImageFileIdIsNotCorrectKey = "course_teacher_image_file_id_is_not_correct_key";
        }

        public static class DiscountMessage
        {
            public static string DiscountCodeAlreadyExistedKey = "discount_code_already_existed_key";
            public static string DiscountCodeNotFound = "discount_code_not_found";
            public static string DiscountNameIsRequiredKey = "discount_name_is_required_key";
            public static string DiscountNameMaximumLengthKey = "discount_name_must_be_less_than_128_characters_key";
            public static string DiscountCodeIsRequiredKey = "discount_code_is_required_key";
            public static string DiscountCodeMaximumLengthKey = "discount_code_must_be_less_than_6_characters_key";
            public static string DiscountValidFromIsRequiredKey = "discount_valid_from_is_required_key";
            public static string DiscountValidToIsRequiredKey = "discount_valid_to_is_required_key";
            public static string DiscountValidToMustBeGreaterThanValidFrom = "discount_valid_to_must_be_greater_than_valid_from_key";
            public static string DiscountScopeApplyIsRequiredKey = "discount_scope_apply_is_required_key";
            public static string DiscountTypeIsRequiredKey = "discount_type_is_required_key";
            public static string DiscountPriceIsRequiredKey = "discount_price_is_required_key";
            public static string DiscountLimitedUseIsRequiredKey = "discount_limited_use_is_required_key";
            public static string DiscountTypeUseIsRequiredKey = "discount_type_use_is_required_key";
            public static string DiscountPeriodNoIsRequiredKey = "discount_period_no_is_required_key";
            public static string DiscountPeriodNoMustBeGreaterThanRequiredKey = "discount_period_no_must_be_greater_than_0";

            public static string DiscountStatusCannotChangeStopWorkingNotActive = "discount_status_cannot_change_StopWorking_to_NotActive";
            public static string DiscountStatusCannotChangeActiveNotActive = "discount_status_cannot_change_Active_to_NotActive";
            public static string DiscountStatusNotValid = "discount_status_is_not_valid";
            public static string DiscountStatusDateChangeNotValid = "current_date_not_valid_to_change_discount_status";
            public static string UserIdsIsRequiredKey = "user_ids_is_required_key";
            public static string DiscountStatusNotActive = "discount_status_is_not_active";
            public static string DiscountCannotDeleteActiveDiscount = "discount_cannot_delete_active_discount";
            public static string SendDiscountDateNotValid = "current_date_not_valid_to_send_discount";
            public static string DiscountLimitedUseGreaterThanZero = "discount_limited_use_must_be_greater_than_0";
            public static string DiscountTypeUseNotValid = "discount_type_use_not_valid";
            public static string DiscountTypeNotValid = "discount_type_not_valid";
            public static string DiscountPercentValueValidateKey = "discount_percent_must_be_0_to_100_key";
            public static string DiscountValueGreaterThanZero = "discount_value_must_be_greater_than_0";
            public static string DiscountNameAlreadyExistedKey = "discount_name_already_existed_key";

            public const string TranslationIsRequired = "translation_is_required";
            public const string DiscountScopeApplyIsNotValid = "discount_scope_apply_is_not_valid";
            public const string DiscountCodeNotAvailable = "discount_code_not_available";
            public const string DiscountNotValid = "discount_not_valid";
            public const string DiscountTypeUseIsNotValid = "discount_type_use_is_not_valid";
            public const string OrderIdMustBeRequired = "order_id_must_be_required";
            public const string WebUserIdMustBeRequired = "web_user_id_must_be_required";
            public const string OrderCodeMustBeRequired = "order_code_must_be_required";
        }

        public static class NewsMessage
        {
            public const string NewsNotFoundKey = "news_not_found_key";
            public const string NewsCategoryIdInvalidKey = "news_category_id_invalid_key";
            public static string NewsIdIsRequiredKey = "news_id_is_required_key";
            public static string NewsNameIsRequiredKey = "news_name_is_required_key";
            public static string NewsNameMaximumLengthKey = "news_name_must_be_less_than_255_characters_key";
            public static string NewsDescriptionIsRequiredKey = "news_description_is_required_key";
            public static string NewsDescriptionMaximumLengthKey = "news_description_must_be_less_than_500_characters_key";
            public static string NewsContentIsRequiredKey = "news_content_is_required_key";
            public static string NewsMetaKeywordsMaximumLengthKey = "news_meta_keywords_must_be_less_than_200_characters_key";
            public static string NewsAuthorMaximumLengthKey = "news_author_must_be_less_than_128_characters_key";
            public static string NewsUrlNotValidKey = "news_url_not_valid_key";
            public static string NewsUrlIsExistedKey = "news_url_existed_key";

            //news image
            public const string NewsImageFileNameInvalidKey = "news_image_file_name_invalid_key";

            public const string NewsImageExtensionInvalidKey = "news_image_extension_invalid_key";
            public const string NewsImageStorageLocationInvalidKey = "news_image_storage_location_invalid_key";
            public const string NewsImageFileTypeInvalidKey = "news_image_file_type_id_invalid_key";
        }

        public static class OrderMessage
        {
            public const string OrderStatusCannotChange = "order_status_cannot_change";
            public const string OrderCannotDeleteOrder = "order_cannot_delete_order";
            public const string OrderNotFound = "order_not_found";
            public const string OrderDateMustBeRequired = "order_date_must_be_required";
            public const string OrderWebUserIdMustBeRequired = "order_web_user_id_must_be_required";
            public const string OrderStatusNotValid = "order_status_not_valid";

            public const string OrderKeyIsRequiredKey = "order_key_is_required_key";
            public const string PaymentMethodIsRequiredKey = "payment_method_is_required_key";
            public const string OrderDateInvalidFormat = "order_date_invalid_format";
            public const string UserEmailMaximumLengthKey = "user_email_must_be_less_than_255_characters_key";
            public const string DiscountCodeMaximumLengthKey = "discount_code_maximum_length_key";
            public const string IsFreeOrderInvalid = "is_free_order_invalid";

            public const string WebUserNotActive = "student_inactive";
            public const string ProductNotActive = "product_inactive";
            public const string DiscountStatusNotActive = "discount_code_inactive";
            public const string DiscountCodeNotAssignedToStudent = "discount_code_not_assigned_to_student";
            public const string EnoughKeyCodeOrAccountBank = "enough_key_code_or_account_bank";
            public const string NotEnoughKeyCodeOrAccountBank = "not_enough_key_code_or_account_bank";
        }

        public static class OrderDetailMessage
        {
            public const string STTMustBeNumber = "stt_must_be_number";
            public const string OrderIdMustBeRequired = "order_id_must_be_required";
            public const string WebUserIdMustBeRequired = "web_user_id_must_be_required";
            public const string OrderKeyIsRequiredKey = "order_key_is_required_key";
            public const string CourseCodeIsRequiredKey = "course_code_is_required_key";
            public const string CoursePriceIdIsRequiredKey = "course_price_id_is_required_key";
            public const string QuantityMustBeNumber = "quantity_must_be_number";
        }

        public static class OrderImportExportMessage
        {
            public const int MaxLengthEmail = 255;
            public const int MaxlengthOrderKey = 128;
            public const string OrderImportError3 = "order_import_error_3"; //Đây là thông tin bắt buộc
            public const string OrderImportError4 = "order_import_error_4"; //Vượt quá số ký tự quy định
            public const string OrderImportError5 = "order_import_error_5"; //Key đơn hàng bị trùng lặp
            public const string OrderImportError6 = "order_import_error_6"; //Phương thức thanh toán chỉ bao gồm: Chuyển khoản hoặc Tiền mặt
            public const string OrderImportError7 = "order_import_error_7"; //Định dạng ngày phải là DD/MM/YYYY và phải tồn tại ngày tháng đó
            public const string OrderImportError8 = "order_import_error_8"; //Ngày thanh toán không thể là ngày trong tương lai
            public const string OrderImportError9 = "order_import_error_9";  //Email học viên không tồn tại
            public const string OrderImportError10 = "order_import_error_10"; // Mã giảm giá không tồn tại
            public const string OrderImportError11 = "order_import_error_11"; // Đơn hàng không đồng chỉ bao gồm: Có hoặc Không
            public const string OrderImportError12 = "order_import_error_12"; // Key đơn hàng không tồn tại trong sheet "Danh sách đơn hàng"
            public const string OrderImportError13 = "order_import_error_13"; // Giá sản phẩm không tồn tại
            public const string OrderImportError14 = "order_import_error_14"; // Giá trị phải lớn hơn hoặc bằng 1
            public const string OrderImportError15 = "order_import_error_15"; // Giá sản phẩm bị trùng lặp
            public const string OrderImportError16 = "order_import_error_16"; // Số lượng phải là kiểu số
            public const string OrderImportError17 = "order_import_error_17"; // Email có tối đa 255 ký tự bao gồm chữ từ a->z và số từ 0->9, kí tự @
            public const string OrderImportError18 = "order_import_error_18"; // Email của học viên đã dừng hoạt động
            public const string OrderImportDiscountNotValid = "order_import_discount_not_valid"; // Mã giảm giá không hợp lệ (có thể là chưa gửi mã giảm giá cho người dùng, hoặc hết số lần sử dụng)
            public const string OrderImportDiscountCodeNotAvailable = "order_import_discount_code_not_available"; // Thời gian hiệu lực mã giảm giá không hợp lệ
            public const string OrderImportDiscountStatusIsNotActive = "order_import_discount_status_is_not_active"; // Trạng thái của mã giảm giá phải là Active
            public const string OrderImportDiscountNotFound = "order_import_discount_code_not_found"; // Mã giảm giá không tồn tại
            public const string OrderImportFileIsNotCorrectFormat = "order_impport_file_is_not_correct_format"; //Mẫu nhập dữ liệu không đúng với quy định",
        }
        public static class QuestionnaireGroupMessage
        {
            public const string QuestionnareGroupIsExists = "questionnaire_group_is_exists";
            public const string QuestionnareGroupNotFound = "questionnaire_group_not_found";
            public const string ParentQuestionnareGroupNotFound = "parent_questionnaire_group_not_found";
            public const string QuestionnareGroupIsUsed = "questionnaire_group_is_used";
            public const string QuestionnaireGroupNameIsRequiredKey = "questionnaire_group_name_is_required_key";
            public const string QuestionnaireGroupNameMaximumLengthKey = "questionnaire_group_name_must_be_less_than_255_characters_key";
            public const string QuestionnaireGroupDescriptionMaximumLengthKey = "questionnaire_group_description_must_be_less_than_500_characters_key";
            public const string QuestionnaireGroupIdentifierMaximumLengthKey = "questionnaire_group_identifier_must_be_less_than_10_characters_key";
            public const string QuestionnaireGroupMaximumLevel = "questionnaire_group_level_must_be_less_than_4";
        }

        public static class QuestionnaireLeftSectionMessage
        {
            public const string LeftSectionNotFound = "left_section_not_found";
            public const string LeftSectionTitleRequiredKey = "left_section_title_required_key";
            public const string LeftSectionTitleMaximumLengthKey = "left_section_title_must_be_less_than_255_characters_key";
            public const string LeftSectionFileIdIsNotCorrectKey = "left_section_file_id_is_not_correct_key";
            public const string LeftSectionSortOrderMustIsANumber = "left_section_sort_order_must_is_a_number";
            public const string LeftSectionAudioFileNotFound = "left_section_audio_file_not_found";
        }

        public static class QuestionnaireMessage
        {
            public const string QuestionnaireTypeIsNotValid = "questionnaire_type_is_not_valid";
            public const string QuestionnaireIsNotFound = "questionnaire_is_not_found";
            public const string QuestionnaireNameIsRequiredKey = "questionnaire_name_is_required_key";
            public const string QuestionnaireNameMaximumLengthKey = "questionnaire_name_must_be_less_than_255_characters_key";
            public const string QuestionnaireMustBeAtLeast1LeftSection = "questionnaire_must_be_at_least_1_leftsection";
            public const string QuestionnaireTitleLeftSectionMaximumLengthKey = "questionnaire_title_left_section_must_be_less_than_255_characters_key";
            public const string QuestionnaireFileIdIsNotCorrectKey = "questionnaire_file_id_is_not_correct_key";
            public const string QuestionnaireIntroContentMaximumLengthKey = "questionnaire_intro_content_must_be_less_than_128_characters_key";
            public const string QuestionnaireSlideFileIdIsNotNull = "questionnaire_slide_file_id_is_not_null";
            public const string QuestionnaireDumpErrorWithId = "questionnaire_dump_error_with_id_{0}";
            public const string ActiveQuestionnaireCannotBeDelete = "active_questionnaire_cannot_be_deleted";
            public const string QuestionnaireAtLeastOneInfoIsRequired = "questionnaire_at_least_one_info_is_required";
            public const string QuestionnaireCannotChangeStatusToInActiveWhenQuestionnaireInMockTestOrInCourse = "change_status_practice_error";
            public const string QuestionnaireNameIsAlreadyExisted = "questionnaire_name_is_already_existed";
            public const string QuestionnaireCannotUpdateStructureChange = "questionnaire_cannot_update_structure_change_{0}";

            public const string InvalidateSpaceTimeFormat = "questionnaire_invalid_space_time_format";
        }

        public static class QuestionMessage
        {
            public const string QuestionNameIsRequiredKey = "question_name_is_required_key";
            public const string QuestionNameMaximumLengthKey = "question_name_must_be_less_than_500_characters_key";
            public const string QuestionExplanationMaximumLengthKey = "question_explanation_must_be_less_than_500_characters_key";
            public const string QuestionNoteMaximumLengthKey = "question_note_must_be_less_than_500_characters_key";
            public const string QuestionFakeValuesMaximumLengthKey = "question_fake_values_must_be_less_than_255_characters_key";
            public const string QuestionDisplayTimestampMaximumLengthKey = "question_display_timestamp_must_be_less_than_30_characters_key";
            public const string QuestionWordEnglishMaximumLengthKey = "question_word_english_must_be_less_than_500_characters_key";
            public const string QuestionWordPhoneticMaximumLengthKey = "question_word_phonetic_must_be_less_than_50_characters_key";
            public const string QuestionTextEnglishMaximumLengthKey = "question_text_english_must_be_less_than_500_characters_key";
            public const string AnswerIsRequiredKey = "answer_is_required_key";
            public const string AnswerTemplateIsRequiredKey = "answer_template_is_required_key";
            public const string EvaluateTemplateRequiredKey = "evaluate_template_required_key";
            public const string AnswerTemplateMaximumLengthKey = "answer_template_must_be_less_than_500_characters_key";
            public const string EvaluateTemplateMaximumLengthKey = "evaluate_template_must_be_less_than_500_characters_key";
            public const string InvalidSampleTemplateObjectFormat = "invalid_sample_template_object_format";
            public const string RecordingTimeRequired = "recording_time_required_key";

            public const string ImageDragDropMustBeAtLeast1AnswerIn1Question = "image_drag_drop_must_be_at_least_1_answer_in_1_question";
            public const string ListQuestionMustNotBeEmpty = "list_question_must_not_be_empty";
            public const string VideoQuestionMustBeAtLeast2AnswerIn1Question = "video_question_must_be_at_least_2_answer_in_1_question";
            public const string VideoQuestionMustBeAtLeast1AnswerCorrectIn1Question = "video_question_must_be_at_least_1_answer_correct_in_1_question";
            public const string MCQQuestionMustBeAtLeast2AnswerIn1Question = "MCQ_question_must_be_at_least_2_answer_in_1_question";
            public const string MCQQuestionMustBeAtLeast1AnswerCorrectIn1Question = "MCQ_question_must_be_at_least_1_answer_correct_in_1_question";
            public const string DropListMustBeAtLeast2AnswerIn1Question = "drop_list_must_be_at_least_2_answer_in_1_question";
            public const string DropListAnswerMustBeHaveCorrectAnswer = "drop_list_answer_must_be_have_correct_answer";
            public const string MCQImageQuestionMustBeAtLeast2AnswerIn1Question = "MCQ_image_question_must_be_at_least_2_answer_in_1_question";
            public const string MCQImageQuestionMustBeAtLeast1AnswerCorrectIn1Question = "MCQ_image_question_must_be_at_least_1_answer_correct_in_1_question";

            public const string FlashCardQuestionTranslationsIsRequiredKey = "translation_is_required";
            public const string FlashCardQuestionNameMaximumLengthKey = "flash_card_question_name_must_be_less_than_128_characters_key";
            public const string FlashCardQuestionWordEnglishIsRequiredKey = "flash_card_question_word_english_is_required_key";
            public const string FlashCardQuestionWordPhoneticIsRequiredKey = "flash_card_question_word_phonetic_is_required_key";
            public const string FlashCardQuestionTypeOfWordIsRequiredKey = "flash_card_question_type_of_word_is_required_key";
            public const string FlashCardQuestionTextEnglishIsRequiredKey = "flash_card_question_text_english_is_required_key";
            public const string FlashCardQuestionWordPhonetic = "flash_card_question_word_phonetic";

            public const string FlashCardQuestionWordFileIsRequiredKey = "flash_card_question_word_file_is_required_key";
            public const string FlashCardQuestionTextFileIsRequiredKey = "flash_card_question_text_file_is_required_key";

            public const string QuestionSortOrderMustIsANumber = "question_sort_order_must_is_a_number";
            public const string AnswerSortOrderMustIsANumber = "answer_sort_order_must_is_a_number";
            public const string QuestionTimestampInvalidFormat = "question_timestamp_invalid_format";

            public const string QuestionInvalidSampleTemplateObject = "question_invalid_sample_template_object";

            public const string TrueFalseQuestionMustBeExact2AnswerIn1Question = "true_false_question_must_exact_2_answer_in_1_question";
            public const string TrueFalseQuestionMustBeHave1AnswerCorrectIn1Question = "true_false_question_must_have_1_answer_correct_in_1_question";

            public const string FillInTheBlankMustBeAtLeast1AnswerIn1Question = "fill_in_the_blank_must_be_at_least_1_answer_in_1_question";

            public const string MatchingMustBeAtLeast1AnswerAnd1QuestionBeMatched = "matching_must_be_at_least_1_answer_and_1_question_be_matched";
            public const string MatchingQuestionNameMaximumLengthKey = "matching_question_name_must_be_less_than_500_characters_key";
            public const string MatchingAnswerNameMaximumLengthKey = "matching_answer_name_must_be_less_than_500_characters_key";

            public const string PronunciationRecognitionQuestionWordPhoneticIsRequiredKey = "pronunciation_recognition_question_word_phonetic_is_required_key";
        }

        public static class QuestionTranslationMessage
        {
            public const string QuestionTranslationWordTranslationIsRequiredKey = "question_translation_word_translation_is_required_key";
        }

        public static class AnswerMessage
        {
            public const string AnswerNameIsRequiredKey = "answer_name_is_required_key";
            public const string AnswerNameMaximumLengthKey = "answer_name_must_be_less_than_300_characters_key";
            public const string AnswerMatchingKeyMaximumLengthKey = "answer_matching_key_must_be_less_than_50_characters_key";
            public const string AnswerCorrectMatchingValuesMaximumLengthKey = "answer_conrrect_matching_values_must_be_less_than_255_characters_key";
            public const string AnswerFakeSelectValuesMaximumLengthKey = "answer_fake_select_values_must_be_less_than_255_characters_key";

            public const string AnswerCannotBeDeleted = "answer_cannot_be_deleted";
            public const string AnswerSortOrderMustIsANumber = "answer_sort_order_must_is_a_number";
            public const string AnswerCorrectMatchingValuesIsRequiredKey = "answer_conrrect_matching_values_is_required_key";
            public const string AnswerFakeSelectValuesIsRequiredKey = "answer_conrrect_fake_select_values_is_required_key";
            public const string AnswerIsNotValid = "answer_is_not_valid";

            public const string TextAnswerIsRequiredKey = "text_answer_is_required_key";
            public const string CorrespondingAnswerIsRequiredKey = "corresponding_answer_is_required_key";
        }

        public static class StepMessage
        {
            public const string StepIdRequiredKey = "step_id_required_key";
            public const string StepIdNotFound = "step_id_not_found";
            public const string LessonIdIsRequiredKey = "step_lesson_id_is_required_key";
            public const string LessonIdNotFound = "step_lesson_id_not_found";
            public const string TitleMaximumLengthKey = "step_title_must_be_less_than_128_characters_key";
            public const string DescriptionMaximumLengthKey = "step_description_must_be_less_than_500_characters_key";
            public const string StepQuestionnaireIdRequiredKey = "step_questionnaire_id_required_key";
            public const string QuestionnaireIdRequiredKey = "questionnaire_id_required_key";
        }

        public static class UnitMessage
        {
            public const string UnitNotFoundKey = "unit_not_found_key";
            public const string UnitCourseIdIsRequiredKey = "unit_course_id_is_required_key";
            public const string UnitTitleMaximumLengthKey = "unit_title_must_be_less_than_128_characters_key";
            public const string UnitDescriptionMaximumLengthKey = "unit_description_must_be_less_than_500_characters_key";
            public const string UnitTagNameMaximumLengthKey = "unit_tag_name_must_be_less_than_128_characters_key";
            public const string UnitLearnInOrderIsRequiredKey = "unit_learn_in_order_is_required_key";
        }

        public static class UnitTestMessage
        {
            public const string UnitTestNotFoundKey = "unit_test_not_found_key";
            public const string UnitTestUnitIdIsRequiredKey = "unit_test_unit_id_is_required_key";
            public const string UnitTestTitleIsRequiredKey = "unit_test_title_is_required_key";
            public const string UnitTestTestTimeIsRequiredKey = "unit_test_test_time_is_required_key";
            public const string UnitTestTitleMaximumLengthKey = "unit_test_title_must_be_less_than_128_characters_key";
            public const string UnitTestDescriptionMaximumLengthKey = "unit_test_description_must_be_less_than_500_characters_key";

            // unit test questionnaire
            public const string UnitTestQuestionnaireUnitTestIdIsRequiredKey = "unit_test_questionnaire_unit_test_id_is_required_key";
            public const string UnitTestQuestionnaireQuestionnaireIdIsRequiredKey = "unit_test_questionnaire_questionnaire_id_is_required_key";
        }

        public static class LessonMessage
        {
            public const string LessonNotFoundKey = "lesson_not_found_key";
            public const string LessonUnitIdIsRequiredKey = "unit_course_id_is_required_key";
            public const string LessonTitleMaximumLengthKey = "lesson_title_must_be_less_than_128_characters_key";
            public const string LessonDescriptionMaximumLengthKey = "lesson_description_must_be_less_than_500_characters_key";
            public const string LessonLearnInOrderIsRequiredKey = "lesson_learn_in_order_is_required_key";
        }

        public static class RankingScoreMessage
        {
            public const int ScoreMaxValueKey = 9999;
            public const string RankingScoreNameAlreadyExisted = "add_new_score_01";
            public const string RankingScoreNameIsRequiredKey = "ranking_score_name_is_required_key";
            public const string RankingScoreNameMaximumLengthKey = "ranking_score_name_must_be_less_than_or_equal_to_100_characters_key";
            public const string RankingScoreNumOfQuestionsIsRequiredKey = "ranking_score_num_of_questions_is_required_key";
            public const string RankingScoreNumOfQuestionsMustNotBeNegativeKey = "ranking_score_num_of_questions_must_not_be_negative_key";
            public const string RankingScoreNumOfQuestionsMaximumLengthKey = "ranking_score_num_of_questions_must_not_be_greater_than_3_characters_key";
            public const string RankingScoreMinScoreIsRequiredKey = "ranking_score_min_score_is_required_key";
            public const string RankingScoreMinScoreMustNotBeNegativeKey = "ranking_score_min_score_must_not_be_negative_key";
            public const string RankingScoreMinScoreMaximumLengthKey = "ranking_score_min_score_must_not_be_greater_than_3_characters_key";
            public const string RankingScoreMaxScoreIsRequiredKey = "ranking_score_max_score_is_required_key";
            public const string RankingScoreMaxScoreMustBePositiveIntegerKey = "ranking_score_max_score_must_be_a_positive_integer_key";
            public const string RankingScoreMaxScoreMaximumLengthKey = "ranking_score_max_score_must_not_be_greater_than_3_characters_key";
            public const string RankingScoreNumScoreRangeIsRequiredKey = "ranking_score_num_score_range_is_required_key";
            public const string RankingScoreNumScoreRangeMustBePositiveKey = "ranking_score_num_score_range_must_be_positive_key";
            public const string RankingScoreNumScoreRangeMaximumLengthKey = "ranking_score_num_score_range_must_not_be_greater_than_3_characters_key";
            public const string RankingScoreMaxMustBeGreaterThanMinScoreKey = "ranking_score_max_score_must_be_greater_than_min_score_key";
            public const string RankingScoreNumScoreRangeMustBeLessThanOrEqualMaxScoreSubtractionMinScoreKey = "ranking_score_num_score_range_must_be_less_than_or_equal_max_score_subtraction_min_score_key";
            public const string RankingScoreNumScoreRangeMustBeLessThanMaxScoreKey = "ranking_score_num_score_range_must_be_less_than_max_score_key";

            public const string RankingScoreSaveSuccessfullyKey = "add_new_score_02";
            public const string RankingScoreIsNotFoundKey = "ranking_score_is_not_found_key";
            public const string RankingScoreDataCanNotBeChangedDataWhenTheStatusIsActiveKey = "ranking_score_data_cannot_be_changed_when_status_is_active_key";
            public const string RankingScoreCannotBeDeleted = "ranking_score_cannot_be_deleted";
            public const string RankingScoreIsUsedInMockTest = "ranking_score_is_used_in_mock_test";
            public const string RankingScoreNeedAtLeast1ScoreDetailKey = "ranking_score_need_at_least_1_score_detail_key";
            public const string OtherRankingScoreAlreadyHasThisMocktestType = "other_ranking_score_already_has_this_mocktest_type";
        }

        public static class GroupScoreMessage
        {
            public const string NameIsRequired = "Vui lòng nhập tên";
            public const string MaximumNameLength = "group_score_maximum_name_length";
            public const string MinimumNameLength = "group_score_minimum_name_length";
            public const string GroupNameAlreadyInFile = "Nhóm điểm đã tồn tại trong file";

            public const string FromQuestionNumberRequired = "Vui lòng nhập từ câu hỏi";
            public const string FromQuestionNumberMustPositive = "Giá trị phải là số nguyên dương";
            public const string FromQuestionNumberTooBigInt = "Giá trị không hợp lệ";
            public const string ToQuestionNumberTooBigInt = "Giá trị không hợp lệ";
            public const string ToQuestionNumberMustPositive = "Đến câu hỏi phải là số nguyên dương";
            public const string FromQuestionNotMatchToQuestion = "Đến câu hỏi phải lớn hơn Từ câu hỏi";
            public const string DuplicateQuestionRange = "Trùng từ câu hỏi đến câu hỏi";
            public const string MinScoreMustPositive = "Điểm tối thiểu phải là số nguyên dương";
            public const string MinScoreTooBigInt = "Giá trị điểm tối thiểu quá lớn";
            public const string MaxScoreIsRequired = "Vui lòng nhập điểm tối đa";
            public const string MaxScoreMustPositive = "Điểm tối đa phải là số nguyên dương";
            public const string MaxScoreTooBigInt = "Điểm tối đa quá lớn";
            public const string QuestionNumberNotInRange = "group_score_question_number_not_in_range";

            public const string GroupFromQuestionNotInRangeExcelMessage = "Giá trị từ câu vượt quá tổng số câu trong thang điểm";
            public const string GroupToQuestionNotInRangeExcelMessage = "Đến câu vượt quá tổng số câu trong thang điểm";
            public const string GroupScore_NameAlreadyInfile = "Nhóm điểm đã tồn tại trong file";

            public const string RequiredGroupScore = "group_score_required_group_score";
            public const string GroupNameNotMatchCurrInSystem = "Không tìm thấy tên nhóm trong hệ thống";
            public const string DuplicateGroupNameInFile = "Trùng tên nhóm trong file";
            public const string NotAnyGroupNameColumnInFile = "Không có tên nhóm nào trong file";
            public const string ScoreRangeNotMathInSystem = "Không đúng cấu trúc trong hệ thống";

            public const string ScoreValueMustPositive = "score_range_score_value_must_positive";
            public const string FromScoreValueMustPositive = "score_range_from_score_value_must_positive";
            public const string ToScoreValueMustPositive = "score_range_to_score_value_must_positive";

            public const string ExtractScoreTooBigInt = "score_range_extract_score_too_big_int";
            public const string FromScoreTooBigInt = "score_range_from_score_too_big_int";
            public const string ToScoreTooBigInt = "score_range_to_score_too_big_int";

            public const string NumberQuestionNotMatch = "group_score_number_questions_not_match";
            public const string NumberQuestionNotMatchExcelMessage = "Số câu hỏi ở các nhóm phải bằng Số câu của thang điểm";

            public const string QuestionsIsNotInSequence = "group_score_questions_not_in_sequence";
            public const string QuestionsIsNotInSequenceExcelMessage = "Thứ tự các câu hỏi trong các nhóm không đúng thứ tự";

            public const string NotFoundGroupScoreName = "Không tìm thấy nhóm điểm trên hệ thống";
            public static string NotFoundGroupScoreNameInSystem(string groupName) => $"Không tìm thấy nhóm: {groupName} trên hệ thống";
            public const string ExtractScoreMustGreaterthanMinScore = "Điểm chính xác không được nhỏ hơn điểm tối thiểu của thang điểm";
            public const string ExtractScoreMustLessthanMaxScore = $"Điểm chính xác không được lớn hơn điểm tối đa của thang điểm";
            public const string MinScoreMustLessThanMaxScore = $"Điểm tối thiểu phải nhỏ hơn điểm tối đa";


        }


        public static class ScoreDetailMessage
        {
            public const string ScoreDetailAllFieldsIsRequiredKey = "add_new_score_03";
            public const string ScoreDetailScoreCannotBeLessThanMinScoreKey = "add_new_score_04";
            public const string ScoreDetailScoreCannotBeGreaterThanMaxScoreKey = "add_new_score_05";
            public const string ScoreDetailScoresMustBeSortedInAscendingOrderKey = "add_new_score_06";
            public const string ScoreDetailScoresNotEqualMaxScore = "add_new_score_07";

            public const string ScoreDetailRankingScoreIdIsNotCorrectKey = "score_detail_ranking_score_id_is_not_correct_key";
            public const string ScoreDetailNumberOfCorrectQuestionsIsNotCorrectKey = "score_detail_number_of_correct_questions_is_not_correct_key";
            public const string ScoreDetailExactScoreMustNotBeLessThanMinScoreKey = "score_detail_exact_score_must_not_be_less_than_min_score_key";
            public const string ScoreDetailExactScoreCanNotBeGreaterThanMaxScoreKey = "score_detail_exact_score_can_not_be_greater_than_max_score_key";
            public const string ScoreDetailSortOrderIsNotCorrectKey = "score_detail_sort_order_is_not_correct_key";
            public const string ScoreDetailEachDataBoxCanOnlyEnterUpTo3CharactersKey = "score_detail_each_data_box_can_only_enter_up_to_3_characters_key";
            public const string ScoreDetailCannotBeDeleted = "score_detail_cannot_be_deleted";
            public const string ScoreDetailExactScoreAndFromScoreMustBeEqualKey = "score_detail_exact_score_and_from_score_must_be_equal_key";
            public const string ScoreDetailToScoreMustBeEqualExactScorePlusNumScoreRangeKey = "score_detail_to_score_must_be_equal_exact_score_plus_num_score_range_key";
            public const string ScoreDetailScoresNotEqualMinScore = "score_detail_scores_not_equal_min_score";

            public const int FileImportMaxSize = 50 * 1024; //Byte
            public const string ImportScore01 = "import_score_01"; //Kích thước file quá lớn. Kích thước tối đa là 50kb
            public const string ImportScore02 = "import_score_02"; //Cấu trúc file không đúng. Vui lòng sử dụng đúng mẫu file nhập dữ liệu
            public const string ImportScore03 = "import_score_03"; //Chỉ được nhập dữ liệu là số nguyên dương. Vui lòng kiểm tra lại file
            public const string ImportScore04 = "import_score_04"; //Mỗi ô dữ liệu chỉ được nhập tối đa 4 ký tự. Vui lòng kiểm tra lại file
            public const string ImportScore05 = "import_score_05"; //Nhập dữ liệu thang điểm thành công
            public const string ImportScore06 = "import_score_06"; //Các mục trong thang điểm không được để trống
            public const string ImportScore07 = "import_score_07"; //Điểm không được nhỏ hơn điểm tối thiểu của thang điểm
            public const string ImportScore08 = "import_score_08"; //Điểm không được lớn hơn điểm tối đa của thang điểm
            public const string ImportScore09 = "import_score_09"; //Thứ tự điểm phải được sắp xếp tăng dần
            public const string ImportScore10 = "import_score_10"; //Số dòng lớn hơn số dòng thực tế của bảng điểm
            public const string ImportScore11 = "import_score_11"; //Số dòng nhỏ hơn số dòng thực tế của bảng điểm
        }

        public static class MockTestTypeMessage
        {
            public const string MockTestTypeNotFound = "mock_test_type_not_found_key";
            public const string MockTestTypeNameIsRequiredKey = "mock_test_type_name_is_required";
            public const string MockTestTypeNameIsExists = "mock_test_type_name_is_exists";
            public const string MockTestTypeNameInvalidFormat = "mock_test_type_name_must_be_less_than_255_characters";
            public const string MockTestTypeSortOrderIsRequiredKey = "mock_test_type_sort_order_is_required";
            public const string MockTestTypeCannotBeDeleted = "mock_test_type_cannot_be_deleted";
        }

        public static class MockTestObjectMessage
        {
            public const string MockTestObjectNotFound = "mock_test_object_not_found_key";
            public const string MockTestObjectNameIsRequiredKey = "mock_test_object_name_is_required";
            public const string MockTestObjectNameIsExists = "mock_test_object_name_is_exists";
            public const string MockTestObjectNameInvalidFormat = "mock_test_object_name_must_be_less_than_255_characters";
            public const string MockTestObjectSortOrderIsRequiredKey = "mock_test_object_sort_order_is_required";
            public const string MockTestObjectCannotBeDeleted = "mock_test_object_cannot_be_deleted";
        }

        public static class ScoreCommentMessage
        {
            public const string ScoreCommentRankingScoreIdIsRequiredKey = "score_comment_ranking_score_id_is_required_key";
            public const string ScoreCommentRankingScoreIdIsNotCorrectKey = "score_comment_ranking_score_id_is_not_correct_key";
            public const string ScoreCommentSortOrderMustBeGreaterThanOrEqualToZeroKey = "score_comment_sort_order_must_be_greater_than_or_equal_to_0_key";
            public const string ScoreCommentToScoreMustBeGreaterThanZeroKey = "score_comment_to_score_must_be_greater_than_0_key";
            public const string ScoreCommentSortOrderIsNotCorrectKey = "score_comment_sort_order_is_not_correct_key";
            public const string ScoreCommentFromScoreMaximumLengthKey = "score_comment_from_score_must_not_be_greater_than_3_characters_key";
            public const string ScoreCommentToScoreMaximumLengthKey = "score_comment_to_score_must_not_be_greater_than_3_characters_key";
            public const string ScoreCommentImageIdIsRequiredKey = "score_comment_image_id_is_required_key";
        }

        public static class MockTestMessage
        {
            public const string MockTestNotFound = "mock_test_not_found";
            public const string MockTestMockTestTypeIdIsRequiredKey = "mock_test_mock_test_type_id_is_required";
            public const string MockTestMockTestTypeIdCannotChangeInActiveOrStopWorkingStatus = "mock_test_mock_test_type_id_cannot_change";
            public const string MockTestMockTestObjectIdIsRequiredKey = "mock_test_mock_test_object_id_is_required";
            public const string MockTestScoreTypeIsRequiredKey = "mock_test_score_type_is_required";
            public const string MockTestTagsInvalidFormat = "tags_must_be_less_than_10_item";
            public const string MockTestNameIsRequiredKey = "mock_test_name_is_required";
            public const string MockTestNameInvalidFormat = "mock_test_name_must_be_less_than_255_characters";
            public const string MockTestLinkUrlViewMoreInvalidFormat = "mock_test_link_url_view_more_must_be_less_than_500_characters";
            public const string MockTestTextViewMoreInvalidFormat = "mock_test_text_view_more_must_be_less_than_225_characters";
            public const string TranslationIsRequired = "translation_is_required";
            public const string ActiveMockTestCannotBeDelete = "cannot_delete_active_mocktest";
            public const string MockTestStatusNotValid = "mock_test_status_is_not_valid";
            public const string MockTestStatusCannotChangeFromNotActiveToStopWorking = "mock_test_status_cannot_change_from_not_active_to_stop_working";
            public const string MockTestStatusCannotChangeFromStopWorkingToNotActive = "mock_test_status_cannot_change_from_stop_working_to_not_active";
            public const string MockTestStatusCannotChangeToStopWorkingAsBeingUsedInModules = "mock_test_status_cannot_change_to_stop_working_as_being_used_in_modules";
            public const string MockTestSectionIsRequired = "mock_test_section_is_required";
            public const string MockTestPartIsRequired = "mock_test_part_is_required";
            public const string MockTestAlreadyInExistingTool = "mock_test_already_in_existing_tool";
            public const string MockTestTotalQuestionsInvalidFormat = "mock_test_total_questions_incorrect";
            public const string MockTestTotalTimeInvalidFormat = "mock_test_total_time_cannot_add_more_or_less_than_10_minutes";
            public const string MockTestCannotDumpWithStatusNotActive = "mock_test_cannot_dump_with_status_not_active";
            public const string MockTestIsUsedInExamTool = "mock_test_is_used_in_exam_tool";
            public const string MockTestIsUsedWithKeyCode = "mock_test_is_used_with_key_code";

            /// <summary>
            /// Định dạng file phải là Excel
            /// </summary>
            public const string MockTestImportError1 = "mocktest_import_error_1"; //Định dạng file phải là Excel
            /// <summary>
            /// Mẫu nhập dữ liệu không đúng với quy định của dạng câu hỏi
            /// </summary>
            public const string MockTestImportError2 = "mocktest_import_error_2"; //Mẫu nhập dữ liệu không đúng với quy định của dạng câu hỏi
            /// <summary>
            /// Đây là thông tin bắt buộc
            /// </summary>
            public const string MockTestImportError3 = "mocktest_import_error_3"; //Đây là thông tin bắt buộc
            /// <summary>
            /// Vượt quá số ký tự quy định ([số ký tự])
            /// </summary>
            public const string MockTestImportError4 = "mocktest_import_error_4"; //Vượt quá số ký tự quy định ([số ký tự])
            /// <summary>
            /// Template ID bị trùng lặp
            /// </summary>
            public const string MockTestImportError5 = "mocktest_import_error_5"; //Template ID bị trùng lặp
            /// <summary>
            /// Dạng đề thi không tồn tại
            /// </summary>
            public const string MockTestImportError6 = "mocktest_import_error_6"; //Dạng đề thi không tồn tại
            /// <summary>
            /// Đối tượng dự thi không tồn tại
            /// </summary>
            public const string MockTestImportError7 = "mocktest_import_error_7"; //Đối tượng dự thi không tồn tại
            /// <summary>
            /// Hiển thị điểm chỉ bao gồm các giá trị sau: Điểm chính xác, Dải điểm, Dải điểm ITP
            /// </summary>
            public const string MockTestImportError8 = "mocktest_import_error_8"; //Hiển thị điểm chỉ bao gồm các giá trị sau: Điểm chính xác, Dải điểm, Dải điểm ITP
            /// <summary>
            /// STT là số tự nhiên
            /// </summary>
            public const string MockTestImportError9 = "mocktest_import_error_9"; //STT là số tự nhiên
            /// <summary>
            /// Template ID không tồn tại trong sheet “Thông tin đề thi”
            /// </summary>
            public const string MockTestImportError10 = "mocktest_import_error_10"; //Template ID không tồn tại trong sheet “Thông tin đề thi”
            /// <summary>
            /// Kiểu thi chỉ bao gồm các giá trị sau: Non-Stop, Freestyle
            /// </summary>
            public const string MockTestImportError11 = "mocktest_import_error_11"; //Kiểu thi chỉ bao gồm các giá trị sau: Non-Stop, Freestyle
            /// <summary>
            /// Số câu là số tự nhiên
            /// </summary>
            public const string MockTestImportError12 = "mocktest_import_error_12"; //Số câu là số tự nhiên
            /// <summary>
            /// Thời gian là số tự nhiên
            /// </summary>
            public const string MockTestImportError13 = "mocktest_import_error_13"; //Thời gian là số tự nhiên
            /// <summary>
            /// Thang điểm không tồn tại trong Module Quản lý thang điểm
            /// </summary>
            public const string MockTestImportError14 = "mocktest_import_error_14"; //Thang điểm không tồn tại trong Module Quản lý thang điểm
            /// <summary>
            /// Phần thi không tồn tại trong sheet “Nội dung chi tiết - Phần thi”
            /// </summary>
            public const string MockTestImportError15 = "mocktest_import_error_15"; //Phần thi không tồn tại trong sheet “Nội dung chi tiết - Phần thi”
            /// <summary>
            /// Part thi bị trùng lặp
            /// </summary>
            public const string MockTestImportError16 = "mocktest_import_error_16"; //Part thi bị trùng lặp
            /// <summary>
            /// File không tồn tại
            /// </summary>
            public const string MockTestImportError17 = "mocktest_import_error_17"; //File không tồn tại
            /// <summary>
            /// File không đúng định dạng quy định ([định dạng file])
            /// </summary>
            public const string MockTestImportError18 = "mocktest_import_error_18"; //File không đúng định dạng quy định ([định dạng file])
            /// <summary>
            /// Không thể nhập quá 10 tag
            /// </summary>
            public const string MockTestImportError19 = "mocktest_import_error_19"; //Không thể nhập quá 10 tag
            /// <summary>
            /// Thời gian nghỉ giữa các câu hỏi là số tự nhiên
            /// </summary>
            public const string MockTestImportError20 = "mocktest_import_error_20"; //Thời gian nghỉ giữa các câu hỏi là số tự nhiên
            /// <summary>
            /// Câu hỏi không tồn tại trong Ngân hàng câu hỏi
            /// </summary>
            public const string MockTestImportError21 = "mocktest_import_error_21"; //Câu hỏi không tồn tại trong Ngân hàng câu hỏi
            /// <summary>
            /// Câu hỏi bị trùng lặp
            /// </summary>
            public const string MockTestImportError22 = "mocktest_import_error_22"; //Câu hỏi bị trùng lặp
            /// <summary>
            /// Không đúng format
            /// </summary>
            public const string MockTestImportError23 = "mocktest_import_error_23";

            public const string MockTestIsPublishingKey = "draft_in_waiting_list"; // Có thí sinh đang làm mocktest => đưa vào hàng đợi
            public const string MockTestPublishSuccessfully = "mocktest_publish_successfully";

            public const string MockTestUsedInCourse = "cannot_delete_mocktest_used_in_course"; // Không thể xóa đề thi do đề thi đang được sử dụng cho khóa tự học!

            public const string MockTestUsedInKeyCode = "cannot_delete_mocktest_with_existing_users"; // Không thể xóa đề thi do có học viên còn hạn sử dụng đề thi

            public const string MockTestContestExistingWebUsersStarted = "cannot_delete_mocktest_with_existing_web_users_started"; // Không thể xóa đề thi khi đã có thí sinh thi
            public const string MockTestSectionTypeIsWritingNonStop_TotalPartTimePart_Or_TotalPartTimeQuestionnaire_HasValue = "MockTestSectionTypeIsWritingNonStop_TotalPartTimePart_Or_TotalPartTimeQuestionnaire_HasValue"; // Nếu kiểu thi là "Writing Non-Stop" thì TotalPartTime của Part hoặc Questionnaire phải có dữ liệu. Không có dữ liệu báo lỗi luôn. 

        }

        public static class MockTestSectionMessage
        {
            public const string MockTestSectionNotFound = "mock_test_section_not_found";
            public const string MockTestSectionRankingScoreIdIsRequiredKey = "mock_test_section_ranking_score_id_is_required_key";
            public const string MockTestSectionNameIsRequiredKey = "mock_test_section_name_is_required_key";
            public const string MockTestSectionNameInvalidFormat = "mock_test_section_name_must_be_less_than_255_characters";
            public const string MockTestSectionTypeIsRequiredKey = "mock_test_section_type_is_required_key";
            public const string MockTestSectionNumberOfQuestionsIsRequiredKey = "mock_test_section_number_of_questions_is_required_key";
            public const string MockTestSectionNumberOfTimeIsRequiredKey = "mock_test_section_number_of_time_is_required_key";
            public const string MockTestSectionNumberOfQuestionsInvalidFormat = "mock_test_section_number_of_questions_invalid_format";
            public const string MockTestSectionRankingScoreIsNotActive = "mock_test_section_ranking_score_is_not_active";
        }

        public static class MockTestPartMessage
        {
            public const string MockTestPartNotFound = "mock_test_part_not_found";
            public const string IntroAudioFileIsRequiredKey = "mock_test_part_intro_audio_file_is_required_key";
            public const string MockTestPartNameIsRequiredKey = "mock_test_part_name_is_required_key";
            public const string MockTestPartNameMaximumLengthKey = "mock_test_part_name_must_not_be_greater_than_255_characters_key";
            public const string MockTestPartMockTestSectionIdIsRequiredKey = "mock_test_part_mock_test_section_id_is_required_key";
            public const string MockTestPartFileIdIsNotCorrectKey = "mock_test_part_file_id_is_not_correct_key";
            public const string MockTestPartQuestionnaireGroupNoQuestionnaire = "mocktest_part_questionnaire_group_no_questionnaire";
        }

        public static class MockTestKeyCodeMessage
        {
            public const string MockTestKeyCodeNotFound = "mock_test_key_code_not_found";
            public const string MockTestKeyCodeStatusCannotDelete = "mock_test_key_code_status_cannot_delete";
        }

        public static class KeyCodeMessage
        {
            public const string MockTestIdMustBeRequired = "mocktest_id_must_be_required";
            public const string TypeMustBeRequired = "type_must_be_required";
            public const string StartDateMustBeRequired = "start_date_must_be_required";
            public const string EndDateMustBeRequired = "start_date_must_be_required";
            public const string ChallengeNameMustBeRequired = "challenge_name_must_be_required";
            public const string ChallengeNameMaximumLengthKey = "challenge_name_must_be_less_than_255_characters_key";
            public const string FileMustBeRequired = "file_must_be_required";

            public const int FileImportMaxSize = 100 * 1024; //Byte

            public const string ImportStudentList01 = "import_student_list_01"; //Kích thước file quá lớn. Kích thước tối đa là 100kb
            public const string ImportStudentList02 = "import_student_list_02"; //Cấu trúc file không đúng. Vui lòng sử dụng đúng mẫu file nhập dữ liệu
            public const string ImportStudentList03 = "import_student_list_03"; //Các dòng thiếu dữ liệu
            public const string ImportStudentList04 = "import_student_list_04"; //File import chứa dữ liệu trùng nhau, bạn có chắc chắn muốn nhập lên?
            public const string ImportStudentList10 = "import_student_list_10"; //MaxLength
        }

        public static class ExamToolMessage
        {
            public const string ExamToolIdAlreadyExistedKey = "exam_tool_id_already_existed_key";
            public const string ExamToolNotFoundKey = "exam_tool_not_found_key";
            public const string ExamToolIdIsRequiredKey = "exam_tool_id_is_required_key";
            public const string ExamToolIdInvalidFormatKey = "exam_tool_id_invalid_format_required_key";
            public const string ExamToolCodeIsRequiredKey = "exam_tool_code_is_required_key";
            public const string ExamToolCodeMaximumLengthKey = "exam_tool_code_must_be_less_than_128_characters_key";
            public const string ExamToolCategoryMaximumLengthKey = "exam_tool_category_must_be_less_than_128_characters_key";
            public const string ExamToolCategoryIsRequiredKey = "exam_tool_category_is_required_key";
            public const string ExamToolCodeAlreadyExistedKey = "exam_tool_code_already_existed_key";
            public const string ExamToolNameAlreadyExistedKey = "exam_tool_name_already_existed_key";
            public const string ExamToolNameIsRequiredKey = "exam_tool_name_is_required_key";
            public const string ExamToolNameMaximumLengthKey = "exam_tool_name_must_be_less_than_255_characters_key";
            public const string ExamToolDescriptionIsRequiredKey = "exam_tool_description_name_is_required_key";
            public const string ExamToolDescriptionMaximumLengthKey = "exam_tool_description_must_be_less_than_500_characters_key";
            public const string ExamToolCategoryIdInvalidKey = "exam_tool_category_id_invalid_key";
            public const string ExamToolNumberOfAssignmentMaximumLengthKey = "exam_tool_number_of_assignment_must_be_less_than_128_characters_key";
            public const string ExamToolNumberOfStudentMaximumLengthKey = "exam_tool_number_of_student_must_be_less_than_128_characters_key";
            public const string ExamToolNumberOfVideoMaximumLengthKey = "exam_tool_number_of_video_must_be_less_than_128_characters_key";
            public const string ExamToolUserGuideLinkMaximumLengthKey = "exam_tool_user_guide_link_must_be_less_than_255_characters_key";
            public const string ExamToolMetaKeywordsMaximumLengthKey = "exam_tool_meta_keywords_must_be_less_than_500_characters_key";
            public const string ExamToolStatusIsRequiredKey = "exam_tool_status_is_required_key";
            public const string ExamToolStatusInvalidFormatKey = "exam_tool_status_invalid_format_key";
            public const string ExamToolDetailTabNameExistedKey = "exam_tool_detail_tab_name_is_required_key";
            public const string ExamToolDetailTabNameMaximumLengthKey = "exam_tool_detail_tab_name_must_be_less_than_128_characters_key";
            public const string ExamToolDetailTitleMaximumLengthKey = "exam_tool_detail_title_must_be_less_than_128_characters_key";
            public const string ExamToolPriceNameIsRequiredKey = "exam_tool_price_name_is_required_key";
            public const string ExamToolPriceNameMaximumLengthKey = "exam_tool_price_name_must_be_less_than_128_characters_key";
            public const string ExamToolTeacherNameIsRequiredKey = "exam_tool_teacher_name_is_required_key";
            public const string ExamToolTeacherNameMaximumLengthKey = "exam_tool_teacher_name_must_be_less_than_128_characters_key";
            public const string ExamToolTeacherUniversityMaximumLengthKey = "exam_tool_teacher_university_must_be_less_than_255_characters_key";
            public const string ExamToolTeacherDescriptionMaximumLengthKey = "exam_tool_teacher_description_must_be_less_than_500_characters_key";
            public const string ExamToolCannotDelete = "course_cannot_delete";

            //ExamTool image
            public const string ExamToolImageFileNameInvalidKey = "exam_tool_image_file_name_invalid_key";

            public const string ExamToolImageExtensionInvalidKey = "exam_tool_image_extension_invalid_key";
            public const string ExamToolImageStorageLocationInvalidKey = "exam_tool_image_storage_location_invalid_key";
            public const string ExamToolImageFileTypeInvalidKey = "exam_tool_image_file_type_id_invalid_key";

            public const string ExamToolImageFileNotFoundKey = "exam_tool_image_file_not_found_key";
            public const string ExamToolImageFileIdIsNotCorrectKey = "exam_tool_image_file_id_is_not_correct_key";

            //status
            public const string ExamToolStatusCannotChangeToActive = "exam_tool_status_cannot_change_to_active";
            public const string ExamToolStatusCannotChangeToInActive = "exam_tool_status_cannot_change_to_inactive";
            public const string ExamToolInPauseForConfirmationOrder = "exam_tool_status_cannot_change_to_inactive_with_pending_payment";
            public const string ExamToolStatusCannotChangeToStopWorking = "exam_tool_status_cannot_change_to_stop_working";
            public const string ExamToolStatusNotValid = "exam_tool_status_is_not_valid";

            // translation
            public const string TranslationIsRequired = "exam_tool_translation_is_required";

            public const string NameIsRequired = "exam_tool_name_is_required";

            public const string ExamToolIsNotActiveKey = "exam_tool_is_not_active_key";
            public const string ExamToolCannotChangeType = "exam_tool_cannot_change_type";

            public const string ExamToolTypeIsRequiredKey = "exam_tool_type_is_required_key";
        }

        public static class LearnWithTeacherMessage
        {
            public const string LearnWithTeacherIdAlreadyExistedKey = "learn_with_teacher_id_already_existed_key";
            public const string LearnWithTeacherNotFoundKey = "learn_with_teacher_not_found_key";
            public const string LearnWithTeacherIdIsRequiredKey = "learn_with_teacher_id_is_required_key";
            public const string LearnWithTeacherIdInvalidFormatKey = "learn_with_teacher_id_invalid_format_required_key";
            public const string LearnWithTeacherCodeIsRequiredKey = "learn_with_teacher_code_is_required_key";
            public const string LearnWithTeacherCodeMaximumLengthKey = "learn_with_teacher_code_must_be_less_than_128_characters_key";
            public const string LearnWithTeacherCategoryMaximumLengthKey = "learn_with_teacher_category_must_be_less_than_128_characters_key";
            public const string LearnWithTeacherCategoryIsRequiredKey = "learn_with_teacher_category_is_required_key";
            public const string LearnWithTeacherCodeAlreadyExistedKey = "learn_with_teacher_code_already_existed_key";
            public const string LearnWithTeacherNameAlreadyExistedKey = "learn_with_teacher_name_already_existed_key";
            public const string LearnWithTeacherNameIsRequiredKey = "learn_with_teacher_name_is_required_key";
            public const string LearnWithTeacherNameMaximumLengthKey = "learn_with_teacher_name_must_be_less_than_255_characters_key";
            public const string LearnWithTeacherDescriptionIsRequiredKey = "learn_with_teacher_description_name_is_required_key";
            public const string LearnWithTeacherDescriptionMaximumLengthKey = "learn_with_teacher_description_must_be_less_than_500_characters_key";
            public const string LearnWithTeacherCategoryIdInvalidKey = "learn_with_teacher_category_id_invalid_key";
            public const string LearnWithTeacherNumberOfAssignmentMaximumLengthKey = "learn_with_teacher_number_of_assignment_must_be_less_than_128_characters_key";
            public const string LearnWithTeacherNumberOfStudentMaximumLengthKey = "learn_with_teacher_number_of_student_must_be_less_than_128_characters_key";
            public const string LearnWithTeacherNumberOfVideoMaximumLengthKey = "learn_with_teacher_number_of_video_must_be_less_than_128_characters_key";
            public const string LearnWithTeacherUserGuideLinkMaximumLengthKey = "learn_with_teacher_user_guide_link_must_be_less_than_255_characters_key";
            public const string LearnWithTeacherMetaKeywordsMaximumLengthKey = "learn_with_teacher_meta_keywords_must_be_less_than_500_characters_key";
            public const string LearnWithTeacherStatusIsRequiredKey = "learn_with_teacher_status_is_required_key";
            public const string LearnWithTeacherStatusInvalidFormatKey = "learn_with_teacher_status_invalid_format_key";
            public const string LearnWithTeacherDetailTabNameExistedKey = "learn_with_teacher_detail_tab_name_is_required_key";
            public const string LearnWithTeacherDetailTabNameMaximumLengthKey = "learn_with_teacher_detail_tab_name_must_be_less_than_128_characters_key";
            public const string LearnWithTeacherDetailTitleMaximumLengthKey = "learn_with_teacher_detail_title_must_be_less_than_128_characters_key";
            public const string LearnWithTeacherPriceNameIsRequiredKey = "learn_with_teacher_price_name_is_required_key";
            public const string LearnWithTeacherPriceNameMaximumLengthKey = "learn_with_teacher_price_name_must_be_less_than_128_characters_key";
            public const string LearnWithTeacherTeacherNameIsRequiredKey = "learn_with_teacher_teacher_name_is_required_key";
            public const string LearnWithTeacherTeacherNameMaximumLengthKey = "learn_with_teacher_teacher_name_must_be_less_than_128_characters_key";
            public const string LearnWithTeacherTeacherUniversityMaximumLengthKey = "learn_with_teacher_teacher_university_must_be_less_than_255_characters_key";
            public const string LearnWithTeacherTeacherDescriptionMaximumLengthKey = "learn_with_teacher_teacher_description_must_be_less_than_500_characters_key";
            public const string LearnWithTeacherCannotDelete = "course_cannot_delete";

            //LearnWithTeacher image
            public const string LearnWithTeacherImageFileNameInvalidKey = "learn_with_teacher_image_file_name_invalid_key";

            public const string LearnWithTeacherImageExtensionInvalidKey = "learn_with_teacher_image_extension_invalid_key";
            public const string LearnWithTeacherImageStorageLocationInvalidKey = "learn_with_teacher_image_storage_location_invalid_key";
            public const string LearnWithTeacherImageFileTypeInvalidKey = "learn_with_teacher_image_file_type_id_invalid_key";

            public const string LearnWithTeacherImageFileNotFoundKey = "learn_with_teacher_image_file_not_found_key";
            public const string LearnWithTeacherImageFileIdIsNotCorrectKey = "learn_with_teacher_image_file_id_is_not_correct_key";

            //status
            public const string LearnWithTeacherStatusCannotChangeToActive = "learn_with_teacher_status_cannot_change_to_active";
            public const string LearnWithTeacherStatusCannotChangeToInActive = "learn_with_teacher_status_cannot_change_to_inactive";
            public const string LearnWithTeacherInPendingPaymentOrder = "course_status_cannot_change_to_inactive_with_pending_payment";
            public const string LearnWithTeacherStatusCannotChangeToStopWorking = "learn_with_teacher_status_cannot_change_to_stop_working";
            public const string LearnWithTeacherStatusNotValid = "learn_with_teacher_status_is_not_valid";

            // translation
            public const string TranslationIsRequired = "learn_with_teacher_translation_is_required";

            public const string NameIsRequired = "learn_with_teacher_name_is_required";

            public const string LearnWithTeacherIsNotActiveKey = "learn_with_teacher_is_not_active_key";
            public const string LearnWithTeacherCannotChangeType = "learn_with_teacher_cannot_change_type";
        }

        public static class ExamToolCategoryMessage
        {
            public const string ExamToolCategoryNotFound = "exam_tool_category_not_found";
            public const string ExamToolCategoryNameIsRequiredKey = "exam_tool_category_name_is_required";
            public const string ExamToolCategoryNameMustBeLessThan255 = "exam_tool_category_name_must_be_less_than_255";
            public const string ExamToolCategoryUrlLearningMustBeLessThan1000 = "exam_tool_category_url_learning_must_be_less_than_1000";
            public const string ExamToolCategoryNameExisted = "add_learning_tool_type_01"; //Phân loại công cụ ôn thi đã tồn tại
            public const string ExamToolCategoryCannotDelete = "exam_tool_category_cannot_delete";
        }

        public static class AccountBankMessage
        {
            public const int FileImportMaxSize = 100 * 1024; //Byte

            public const string ImportAccount01 = "import_account_01"; //Phải nhập thời gian lớn hơn 0
            public const string ImportAccount02 = "import_account_02"; //Kích thước file không được vượt quá 100kb
            public const string ImportAccount03 = "import_account_03"; //Cấu trúc file không đúng. Vui lòng sử dụng đúng mẫu file nhập dữ liệu
            public const string ImportAccount04 = "import_account_04"; //Keycode bị trùng
            public const string ImportAccount05 = "import_account_05"; //Keycode đã tồn tại
            public const string ImportAccount06 = "import_account_06"; //Tài khoản bị trùng
            public const string ImportAccount07 = "import_account_07"; //Tài khoản đã tồn tại
            public const string ImportAccount08 = "import_account_08"; //Các dòng thiếu dữ liệu
            public const string ImportAccount09 = "import_account_09"; //Các dòng không đúng định dạng

            public const string KeycodeDuplicated = "keycode_duplicated";
            public const string UsernameDuplicated = "username_duplicated";
            public const string KeycodeExisted = "keycode_existed";
            public const string UsernameExisted = "username_existed";

            public const string PasswordIsRequired = "password_is_required";
            public const string UsernameIsRequired = "username_is_required";
            public const string KeycodeIsRequired = "keycode_is_required";

            public const string AccountBankNotFound = "account_bank_not_found";
            public const string AccountBankHasBeenSentEnough = "account_bank_has_been_sent_enough";
        }

        public static class ReviewMessage
        {
            public const int MaxOfTopComments = 5;
            public const string CourseNotFoundKey = "course_not_found_key";
            public const string ReviewIdIsRequiredKey = "review_id_is_required_key";
            public const string ReviewEmailInvalidFormatKey = "review_email_invalid_format_key";
            public const string ReviewEmailMaximumLengthKey = "review_email_must_be_less_than_or_equal_255_characters_key";

            public const string ReviewFullNameIsRequiredKey = "review_fullname_is_required_key";
            public const string ReviewFullNameMaximumLengthKey = "review_fullname_must_be_less_than_or_equal_255_characters_key";

            public const string ReviewPhoneNumberIsRequiredKey = "review_phone_number_is_required_key";
            public const string ReviewPhoneNumberMaximumLengthKey = "review_phone_number_must_be_less_than_or_equal_15_characters_key";
            public const string ReviewPhoneInvalidFormatKey = "review_phone_invalid_format_key";

            public const string ReviewContentIsRequiredKey = "review_content_is_required_key";
            public const string ReviewContentMinimumLengthKey = "review_content_must_be_greater_than_or_equal_20_characters_key";
            public const string ReviewContentMaximumLengthKey = "review_content_must_be_less_than_or_equal_255_characters_key";
            public const string ReviewRateIsRequiredKey = "review_rate_is_required_key";
            public const string ReviewRateMustBeGreaterThan0AndLessThanOrEqualTo5Key = "review_rate_must_be_greater_than_0_and_less_than_or_equal_to_5_key";
            public const string ReviewNotFoundKey = "review_not_found_key";
            public const string ReviewCourseIdIsRequiredKey = "review_course_id_is_required_key";
            public const string ReviewStatusInvalidKey = "review_status_invalid_key";
            public const string ReviewCreatedDateInvalidKey = "review_created_date_invalid_key";
            public const string ReviewNumberOfTopCommentHasBeenExceededKey = "review_number_of_top_comment_has_been_exceeded_key";
        }

        public static class CourseTestMessage
        {
            public const string CourseTestNotFoundKey = "course_test_not_found_key";
            public const string CourseTestIdIsRequiredKey = "course_test_course_id_is_required_key";
            public const string CourseTestTitleMaximumLengthKey = "course_test_title_must_be_less_than_128_characters_key";
            public const string CourseTestDescriptionMaximumLengthKey = "course_test_description_must_be_less_than_500_characters_key";
            public const string CourseTestTagNameMaximumLengthKey = "course_test_tag_name_must_be_less_than_128_characters_key";
            public const string CourseIdMustBeRequired = "course_id_must_be_required";
            public const string MockTestIdMustBeRequired = "mocktest_id_must_be_required";
        }

        public static class PolicyMockTestSettingMessage
        {
            public const string PolicyMockTestSettingNotFoundKey = "policy_mocktest_setting_not_found_key";
            public const string PolicyMockTestSettingFileIdIsNotCorrectKey = "policy_mocktest_setting_file_id_is_not_correct_key";

            public const string TranslationIsRequired = "translation_is_required";

            // translation
            public const string PolicyMockTestSettingPolicyNameIdIsRequiredKey = "policy_mocktest_setting_policy_name_is_required_key";
            public const string PolicyMockTestSettingGuidingNameIdIsRequiredKey = "policy_mocktest_setting_guiding_name_is_required_key";
            public const string PolicyMockTestSettingSoundNameIdIsRequiredKey = "policy_mocktest_setting_sound_name_is_required_key";
            public const string PolicyMockTestSettingPolicyNameMaximumLengthKey = "policy_mocktest_setting_policy_name_must_be_less_than_255_characters_key";
            public const string PolicyMockTestSettingGuidingNameMaximumLengthKey = "policy_mocktest_setting_guiding_name_must_be_less_than_255_characters_key";
            public const string PolicyMockTestSettingSoundNameMaximumLengthKey = "policy_mocktest_setting_sound_name_must_be_less_than_255_characters_key";
        }

        public static class GuidingMockTestMessage
        {
            public const string GuidingMockTestNotFoundKey = "guiding_mocktest_not_found_key";
            public const string GuidingMockTestMockTestTypeIdIsRequiredKey = "guiding_mocktest_mocktest_type_id_is_required_key";
            public const string GuidingMockTestMockTestObjectIdIsRequiredKey = "guiding_mocktest_mocktest_object_id_is_required_key";
            public const string GuidingMockTestCategoryIdIsRequiredKey = "guiding_mocktest_category_id_is_required_key";
            public const string GuidingMockTestCategoryTypeNotValidKey = "guiding_mocktest_category_type_not_valid_key";
            public const string GuidingMockTestCategoryIdNotValidKey = "guiding_mocktest_category_id_not_valid_key";
            public const string GuidingMockTestIsMockExamAlreadyExistKey = "guiding_mocktest_is_mock_exam_already_exist_key";
            public const string TranslationIsRequired = "translation_is_required";

            // translation
            public const string GuidingMockTestNameIsRequiredKey = "guiding_mocktest_name_is_required_key";
            public const string GuidingMockTestContentIsRequiredKey = "guiding_mocktest_content_is_required_key";
            public const string GuidingMockTestNameMaximumLengthKey = "guiding_mocktest_name_must_be_less_than_255_characters_key";
        }

        public static class FeedbackMessage
        {
            public const string FeedbackIdIsRequired = "feedback_id_is_required";
            public const string FeedbackNotFoundKey = "feedback_not_found_key";
            public const string FeedbackNameIsRequired = "feedback_name_is_required";
            public const string FeedbackNameInvalidFormat = "feedback_name_must_be_less_than_128";
            public const string FeedbackImageInvalidKey = "feedback_image_invalid_key";
            public const string FeedbackImageFileNameInvalidKey = "feedback_image_file_name_invalid_key";
            public const string FeedbackImageExtensionInvalidKey = "feedback_image_extension_invalid_key";
            public const string FeedbackImageStorageLocationInvalidKey = "feedback_image_storage_location_invalid_key";
            public const string FeedbackImageFileTypeInvalidKey = "feedbacks_image_file_type_id_invalid_key";
            public const string FeedbackPositionInvalidFormat = "feedback_position_must_be_less_than_128";

            public const string FeedbackNumberInvalidNumber = "add_new_review_homepage_03"; // Không thể tạo thêm do đã đủ 10 đánh giá
        }

        public static class BannerMessage
        {
            public const string BannerIdIsRequired = "Banner_id_is_required";
            public const string BannerNotFoundKey = "banner_not_found_key";
            public const string BannerNameIsRequired = "banner_name_is_required";
            public const string BannerNameInvalidFormat = "banner_name_must_be_less_than_128";
            public const string BannerLinkUrlInvalidFormat = "banner_link_url_must_be_less_than_500";
            public const string BannerImageInvalidKey = "banner_image_invalid_key";
            public const string BannerImageFileNameInvalidKey = "banner_image_file_name_invalid_key";
            public const string BannerImageExtensionInvalidKey = "banner_image_extension_invalid_key";
            public const string BannerImageStorageLocationInvalidKey = "banner_image_storage_location_invalid_key";
            public const string BannerImageFileTypeInvalidKey = "banners_image_file_type_id_invalid_key";
            public const string BannerPositionInvalidFormat = "banner_position_must_be_less_than_128";

            public const string BannerHomeInvalidFormat = "manage_banner_slider_01"; // Không thể tạo thêm do đã đủ 10 banner
            public const string BannerIIGInvalidFormat = "manage_banner_slider_02"; // Không thể tạo thêm do đã đủ 1 banner
        }

        public static class SocialMediaMessage
        {
            public const string SocialMediaIdIsRequired = "social_media_id_is_required";
            public const string SocialMediaNotFoundKey = "social_media_not_found_key";
            public const string SocialMediaNameIsRequired = "social_media_name_is_required";
            public const string SocialMediaNameInvalidFormat = "social_media_name_must_be_less_than_128";
            public const string SocialMediaLinkUrlInvalidFormat = "social_media_link_url_must_be_less_than_500";
            public const string SocialMediaImageInvalidKey = "social_media_image_invalid_key";
            public const string SocialMediaImageFileNameInvalidKey = "social_media_image_file_name_invalid_key";
            public const string SocialMediaImageExtensionInvalidKey = "social_media_image_extension_invalid_key";
            public const string SocialMediaImageStorageLocationInvalidKey = "social_media_image_storage_location_invalid_key";
            public const string SocialMediaImageFileTypeInvalidKey = "social_medias_image_file_type_id_invalid_key";
            public const string SocialMediaPositionInvalidFormat = "social_media_position_must_be_less_than_128";
        }

        public static class AddressMessage
        {
            public const string AddressIdIsRequired = "address_id_is_required";
            public const string AddressNotFoundKey = "address_not_found_key";
            public const string AddressNameIsRequired = "address_name_is_required";
            public const string AddressNameInvalidFormat = "address_name_must_be_less_than_128";
            public const string AddressContentInvalidFormat = "address_content_must_be_less_than_255";
        }

        public static class SettingMessage
        {
            public const string SettingNotFoundKey = "setting_not_found_key";
            public const string SettingValueInvalidType = "setting_value_invalid_type";
            public const string SettingDataTypeInvalid = "setting_data_type_invalid";
        }

        public static class NotificationMessage
        {
            public const string NotificationIdIsRequired = "notification_id_is_required";
            public const string NotificationNotFoundKey = "notification_not_found_key";
            public const string NotificationDatetimeTriggerIsRequired = "notification_datetime_trigger_is_required";
            public const string NotificationDatetimeTriggerMustBeGreaterThanOrEqualToCurrentTime = "notification_datetime_trigger_must_be_greater_than_or_equal_to_current_time";
            public const string NotificationImageIsRequired = "notification_image_is_required";
            public const string NotificationTitleIsRequired = "notification_title_is_required";
            public const string NotificationTitleInvalidFormat = "notification_title_must_be_less_than_128";
            public const string NotificationDescriptionInvalidFormat = "notification_description_must_be_less_than_128";
            public const string NotificationImageInvalidKey = "notification_image_invalid_key";
            public const string NotificationImageFileNameInvalidKey = "notification_image_file_name_invalid_key";
            public const string NotificationImageExtensionInvalidKey = "notification_image_extension_invalid_key";
            public const string NotificationImageStorageLocationInvalidKey = "notification_image_storage_location_invalid_key";
            public const string NotificationImageFileTypeInvalidKey = "notifications_image_file_type_id_invalid_key";
            public const string NotificationWasSent = "notification_was_sent";
        }

        public static class LiveClassDetailMessage
        {
            public const string LiveClassDetailNotFoundKey = "live_class_detail_not_found";
            public const string LiveClassDetailStatusNotValid = "live_class_detail_status_is_not_valid";
            public const string LiveClassDetailCannotDelete = "live_class_detail_cannot_delete";
            public const string LiveClassDetailIdInvalidFormatKey = "live_class_detail_id_invalid_key";
            public const string LiveClassDetailIdAlreadyExistedKey = "live_class_detail_id_already_existed";
            public const string LiveClassDetailNameAlreadyExistedKey = "class_name_already_existed_key";
            public const string LiveClassDetailEndDateInvalidFormatKey = "live_class_detail_end_date_invalid_format";
            public const string LiveClassDetailFileIdIsNotCorrectKey = "live_class_detail_image_file_id_is_not_correct_key";
            public const string LiveClassDetailCannotUpdate = "live_class_detail_cannot_update";

            public const int LiveClassDetailSuccessNo = 1;
            public const string LiveClassDetailCannotCreateClassinCourse = "live_class_detail_cannot_create_classin_course";
            public const string LiveClassDetailClassinCourseIdNotFound = "live_class_lesson_classin_course_id_not_found";
            public const string LiveClassDetailCannotEditClassinCourse = "live_class_detail_cannot_update_classin_course";
            public const string LiveClassDetailCannotEndClassinCourse = "live_class_detail_cannot_end_classin_course";
            public const string LiveClassDetailNameIsRequired = "live_class_detail_name_is_required";
            public const string LiveClassDetailMaximumLengthKey = "live_class_detail_maximum_length_key";

            public const string LiveClassTypeNameIsRequired = "live_class_type_name_is_required";
            public const string LiveClassDetailNumberOfTACannotExceed6 = "number_of_ta_cannot_exceed_6";

            public const string LiveClassIsStopWorking = "live_class_is_stop_working";

            public const string LiveClassDetailLiveClassTypeIdCannotUpdate = "live_class_detail_live_class_type_id_cannot_update";
            public const string LiveClassDetailCannotChangeStatusInActiveToStopWorking = "live_class_detail_cannot_change_from_inactive_to_stop_working";
            public const string LiveClassDetailCannotChangeStatusInActiveToActive = "live_class_detail_cannot_change_from_inactive_to_active";
            public const string LiveClassDetailCannotChangeStatusActiveToStopWorking = "live_class_detail_cannot_change_from_active_to_stop_working";
            public const string LiveClassDetailCannotChangeStatusActiveToInActive = "live_class_detail_cannot_change_from_active_to_inactive";
            public const string LiveClassDetailCannotUpdateStartDate = "live_class_detail_cannot_update_start_date";
            public const string UserNotHaveTeacherRole = "user_not_have_teacher_role"; // Bạn không có vai trò giáo viên
        }

        public static class LiveClassDetailTestMessage
        {
            public const string LiveClassDetailTestNotFoundKey = "live_class_detail_test_not_found";
            public const string LiveClassDetailTestNameIsRequired = "live_class_detail_test_name_is_required";
            public const string LiveClassDetailTestNameInvalidFormat = "live_class_detail_test_name_must_be_less_than_128_characters_key";
            public const string LiveClassDetailTestLiveClassDetailIdIsRequired = "live_class_detail_test_live_class_detail_id_is_required";
            public const string LiveClassDetailTestMockTestIdIsRequired = "live_class_detail_test_mock_test_id_is_required";
            public const string LiveClassDetailTestStartDateIsRequired = "live_class_detail_test_start_date_is_required";
            public const string LiveClassDetailTestStartDateInvalidFormat = "live_class_detail_test_start_date_invalid_format";
            public const string LiveClassDetailTestEndDateIsRequired = "live_class_detail_test_end_date_is_required";
            public const string LiveClassDetailTestEndDateInvalidFormat = "live_class_detail_test_end_date_invalid_format";
            public const string LiveClassDetailTestCannotUpdate = "live_class_detail_test_cannot_update";
            public const string LiveClassDetailTestCannotDelete = "live_class_detail_test_cannot_delete";
        }

        public static class LiveClassTypeMessage
        {
            public const string LiveClassTypeIdIsRequired = "live_class_type_id_is_required";
            public const string LiveClassTypeNotFoundKey = "live_class_type_not_found_key";
            public const string LiveClassTypeNameIsRequired = "live_class_type_name_is_required";
            public const string LiveClassTypeNameInvalidFormat = "live_class_type_name_must_be_less_than_255_characters_key";
            public const string LiveClassTypeStudentNumberIsRequired = "live_class_type_student_number_is_required";
            public const string LiveClassTypeTotalLessonIsRequired = "live_class_type_total_lesson_is_required";
            public const string LiveClassTypeTimeLessonIsRequired = "live_class_type_time_lesson_is_required";
            public const string LiveClassTypeCannotUpdateOrDelete = "live_class_type_cannot_update_or_delete";
            public const string LiveClassTypeStatusNotValid = "live_class_type_status_is_not_valid";
            public static string LiveClassTypeCannotChangeStatusInActiveToStopWoring = "live_class_type_cannot_change_from_inactive_to_stop_working";
            public static string LiveClassTypeUsedInCourse = "live_class_type_used_in_course";
            public static string LiveClassTypeUsedInLiveClass = "live_class_type_used_in_live_class";
            public static string LiveClassTypeNameMaximumLengthKey = "live_class_type_name_maximum_length_key";
        }

        public static class LiveClassBookingMessage
        {
            public const string LiveClassBookingNotFoundKey = "live_class_booking_not_found_key";
            public const string LiveClassBookingStoppedDateIsRequiredKey = "live_class_booking_stopped_date_is_required_key";
            public const string LiveClassBookingIdsIsRequiredKey = "live_class_booking_ids_is_required_key";
            public const string LiveClassBookingNumberOfStudentsSurpassLimitWarningKey = "number_of_students_surpass_limit_warning";
            public const string LiveClassBookingIncorrectLiveClassTypeKey = "incorrect_live_class_type";
            public const string SomeStudentsHaveBeenAddedToAnotherClassKey = "some_students_have_been_added_to_another_class_key";
            public const string LiveClassBookingDuplicateStudentKey = "live_class_booking_duplicate_student_key";
            public const string LiveClassTeacherEmailsExistedAsStudentEmailsMessage = "email_used_for_student_role";
            public const string LiveClassStudentEmailsExistedAsTeacherEmailsMessage = "email_used_for_teacher_role";
        }

        public static class LiveLessonDetailMessage
        {
            public const string LiveLessonDetailCannotCreateClassinCourseClass = "live_class_lesson_cannot_create_classin_course_class";
            public const string LiveLessonDetailCannotUpdateClassinCourseClass = "live_class_lesson_cannot_update_classin_course_class";
            public const string LiveLessonDetailNameIsRequired = "live_lesson_detail_name_is_required";
            public const string LiveLessonDetailNameMaximumLengthKey = "live_lesson_detail_name_maximum_length_key";
            public const string LiveLessonDetailClassDetailIdIsRequired = "live_lesson_detail_class_detail_id_is_required";
            public const string LiveLessonDetailTeacherIdIsRequired = "live_lesson_detail_teacher_id_is_required";
            public const string LiveLessonDetailLearningDateIsRequired = "live_lesson_detail_learning_date_is_required";
            public const string LiveLessonDetailLearningDateInvalidFormat = "live_lesson_detail_learning_date_invalid_format";
            public const string LiveLessonDetailClassDetailNotFound = "live_lesson_detail_class_detail_not_found";
            public const string LessonTimeInvalid = "lesson_time_invalid";
            public const string LiveLessonDetailNotFoundKey = "live_lesson_detail_not_found_key";
            public const string LiveLessonDetailTeacherCannotBeSetAsAdviser = "live_class_lesson_teacher_cannot_be_set_as_adviser";
            public const string LiveLessonDetailClassinCourseIdNotFound = "live_class_lesson_classin_course_id_not_found";
            public const string LiveLessonDetailClassinLessonIdNotFound = "live_class_lesson_classin_lesson_id_not_found";
            public const string LiveLessonDetailClassinTeacherIdNotFoundKey = "live_lesson_detail_classin_teacher_id_not_found_key";

            public const string AccountDoesNotExistInClassin = "account_does_not_exist_in_classin";
            public const string TeacherCanOnlyEnterClassroom20MinutesBeforeLessonStartTime = "teacher_can_only_enter_classroom_20_minutes_before_lesson_start_time";
            public const int NumberOfMinuteCanEnter = 20;

            public const string AdminUserNotFound = "admin_user_not_found";
            public const string LiveClassDetailNotFound = "live_class_detail_not_found";
            public const string LiveLessonDetailNotFound = "live_lesson_detail_not_found";
            public const string LiveLessonDetailHasEnded = "live_lesson_detail_has_ended";
            public const string LiveLessonDetailCannotDeleteLesson = "live_lesson_detail_cannot_delete_lesson";
            public const string LiveLessonDetailCannotDeleteClassinCourseClass = "live_lesson_detail_cannot_delete_classin_course_class";
            public const string LiveLessonDetailCannotUpdateLesson = "live_lesson_detail_cannot_update_lesson";
        }

        public static class LiveLessonMissionMessage
        {
            public const string LiveLessonMissionLiveLessonDetailIdIsRequiredKey = "live_lesson_mission_live_lesson_detail_id_is_required_key";
            public const string LiveLessonMissionNameIsRequiredKey = "live_lesson_mission_name_is_required_key";
            public const string LiveLessonMissionNameInvalidFormat = "live_lesson_mission_name_must_be_less_than_128_characters_key";
            public const string LiveLessonMissionStartDateIsRequiredKey = "live_lesson_mission_start_date_is_required_key";
            public const string LiveLessonMissionStartDateInvalidFormat = "live_lesson_mission_start_date_invalid_format";
            public const string LiveLessonMissionEndDateIsRequiredKey = "live_lesson_mission_end_date_is_required_key";
            public const string LiveLessonMissionEndDateInvalidFormat = "live_lesson_mission_end_date_invalid_format";
            public const string LiveLessonMissionStatusIsRequiredKey = "live_lesson_mission_status_is_required_key";
            public const string LiveLessonMissionNotFoundKey = "live_lesson_mission_not_found_key";
            public const string LiveLessonMissionCannotUpdate = "live_lesson_mission_cannot_update";

            // questionnaire
            public const string LiveLessonMissionQuestionnaireLLMIdIsRequiredKey = "llmq_live_lesson_mission_id_is_required_key";
            public const string LiveLessonMissionQuestionnaireLLDIdIsRequiredKey = "llmq_live_lesson_detail_id_is_required_key";
            public const string LiveLessonMissionQuestionnaireGroupIdIsRequiredKey = "llmq_questionnaire_group_id_is_required_key";
            public const string LiveLessonMissionQuestionnaireQuestionnaireInfoIsRequiredKey = "llmq_questionnaires_and_types_is_required_key";
            public const string LiveLessonMissionQuestionnaireQuestionnaireIdIsRequiredKey = "llmq_questionnaire_id_is_required_key";
        }

        public static class QuestionnaireImportMessage
        {
            /// <summary>
            /// First sheet in template (hidden), use for get Template type
            /// </summary>
            public const string QuestionnaireTypeInfoImport = "QuestionnaireTypeInfo";
            /// <summary>
            ///  Định dạng file phải là Excel
            /// </summary>
            public const string PracticeImportError1 = "practice_import_error_1";
            /// <summary>
            /// Mẫu nhập dữ liệu không đúng với quy định của dạng câu hỏi
            /// </summary>
            public const string PracticeImportError2 = "practice_import_error_2";
            /// <summary>
            /// Đây là thông tin bắt buộc
            /// </summary>
            public const string PracticeImportError3 = "practice_import_error_3";
            /// <summary>
            /// Đường dẫn không tồn tại
            /// </summary>
            public const string PracticeImportError4 = "practice_import_error_4"; //Đường dẫn không tồn tại
            /// <summary>
            /// Vượt quá số ký tự quy định
            /// </summary>
            public const string PracticeImportError5 = "practice_import_error_5"; //Vượt quá số ký tự quy định
            /// <summary>
            /// File không đúng định dạng quy định
            /// </summary>
            public const string PracticeImportError6 = "practice_import_error_6"; //File không đúng định dạng quy định
            /// <summary>
            /// Thời lượng audio phải là định dạng kiểu số và >=0
            /// </summary>
            public const string PracticeImportError7 = "practice_import_error_7"; //Thời lượng audio phải là định dạng kiểu số và >=0
            /// <summary>
            /// Tên câu hỏi đã tồn tại trong hệ thống
            /// </summary>
            public const string PracticeImportError8 = "practice_import_error_8"; //Tên câu hỏi đã tồn tại trong hệ thống
            /// <summary>
            /// Tên câu hỏi bị trùng lặp
            /// </summary>
            public const string PracticeImportError9 = "practice_import_error_9"; //Tên câu hỏi bị trùng lặp
            /// <summary>
            /// Nhóm câu hỏi không tồn tại
            /// </summary>
            public const string PracticeImportError10 = "practice_import_error_10"; //Nhóm câu hỏi không tồn tại
            /// <summary>
            /// Tên câu hỏi không tồn tại trong sheet "Thông tin câu hỏi"
            /// </summary>
            public const string PracticeImportError11 = "practice_import_error_11"; //Tên câu hỏi không tồn tại trong sheet "Thông tin câu hỏi"
            /// <summary>
            /// Câu hỏi bị trùng lặp
            /// </summary>
            public const string PracticeImportError12 = "practice_import_error_12"; //Câu hỏi bị trùng lặp
            /// <summary>
            /// Câu trả lời bị trùng lặp
            /// </summary>
            public const string PracticeImportError13 = "practice_import_error_13"; //Câu trả lời bị trùng lặp
            /// <summary>
            /// STT phải là kiểu số và >0
            /// </summary>
            public const string PracticeImportError14 = "practice_import_error_14"; //STT phải là kiểu số và >0
            /// <summary>
            /// Nội dung bên trái bị trùng lặp
            /// </summary>
            public const string PracticeImportError15 = "practice_import_error_15"; //Nội dung bên trái bị trùng lặp
            /// <summary>
            /// STT câu hỏi không tồn tại trong sheet  “Câu hỏi”
            /// </summary>
            public const string PracticeImportError17 = "practice_import_error_17"; //STT câu hỏi không tồn tại trong sheet  “Câu hỏi”
            /// <summary>
            /// Vượt quá dung lượng quy định
            /// </summary>
            public const string PracticeImportError18 = "practice_import_error_18"; //Vượt quá dung lượng quy định

            public const string PracticeImportError19 = "practice_import_error_19"; //Tags không tồn tại trong hệ thống
            /// <summary>
            /// Định dạng Timestamp phải là hh:mm:ss
            /// </summary>
            public const string PracticeType1ImportError1 = "practice_type_1_import_error_1"; //Định dạng Timestamp phải là hh:mm:ss
            /// <summary>
            /// Loại từ chỉ bao gồm các giá trị sau: Noun, Verb, Adjective, Adverb, Noun phrase, Phrasal verb, Adjective phrase, Idiom
            /// </summary>
            public const string PracticeType57ImportError1 = "practice_type_5_7_import_error_1"; //Loại từ chỉ bao gồm các giá trị sau: Noun, Verb, Adjective, Adverb, Noun phrase, Phrasal verb, Adjective phrase, Idiom
            /// <summary>
            /// Số lượng câu hỏi và đáp án phải bằng nhau
            /// </summary>
            public const string PracticeType4610ImportError1 = "practice_type_4_6_10_import_error_1"; //Số lượng câu hỏi và đáp án phải bằng nhau
            /// <summary>
            /// Đáp án đúng không thuộc bộ đáp án
            /// </summary>
            public const string PracticeType6ImportError1 = "practice_type_6_import_error_1"; //Đáp án đúng không thuộc bộ đáp án
            /// <summary>
            /// Chỉ có thể chọn TRUE hoặc FALSE
            /// </summary>
            public const string PracticeType9ImportError1 = "practice_type_9_import_error_1"; //Chỉ có thể chọn TRUE hoặc FALSE
            /// <summary>
            /// Bắt buộc phải có 1 đáp án đúng
            /// </summary>
            public const string PracticeType38ImportError1 = "practice_type_3_8_import_error_1"; //Bắt buộc phải có 1 đáp án đúng
            /// <summary>
            /// Chỉ có thể chọn 1 đáp án đúng
            /// </summary>
            public const string PracticeType38ImportError2 = "practice_type_3_8_import_error_2"; //Chỉ có thể chọn 1 đáp án đúng

        }

        public static class ReceiceInvoice
        {
            public const string ReceiceInvoiceCannotUpdate = "invoice_cannot_be_update";
            public const string ReceiceInvoiceNotFound = "invoice_not_found_key";
        }

        public static class ToeflChallengeContestMessage
        {
            public const string ToeflChallengeContestTemplateIdRequiredKey = "toefl_challenge_template_id_is_required_key";
            public const string ToeflChallengeContestTemplateIdMaximumLengthKey = "template_id_must_be_less_than_128_characters_key";
            public const string ToeflChallengeContestNameRequiredKey = "toefl_challenge_contest_name_is_required_key";
            public const string ToeflChallengeContestNameMaximumLengthKey = "contest_name_must_be_less_than_128_characters_key";
            public const string ToeflChallengeContestNameExist = "contest_name_exist_key";
            public const string ToeflChallengeContestProvinceRequiredKey = "toefl_challenge_contest_province_is_required_key";
            public const string ToeflChallengeContestProvinceMaximumLengthKey = "province_must_be_less_than_128_characters_key";
            public const string ToeflChallengeContestProvinceNotExist = "province_exist_key";
            public const string ToeflChallengeContestIconNotFoundKey = "icon_not_found_key";
            public const string ToeflChallengeContestIconUrlMaximumLengthKey = "icon_url_must_be_less_than_128_characters_key";
            public const string ToeflChallengeContestIconInvalidFileExtension = "icon_invalid_file_extension_key";
            public const string ToeflChallengeContestStartDateRequiredKey = "toefl_challenge_contest_start_date_is_required_key";
            public const string InvalidStartDate = "invalid_start_date_key";
            public const string ToeflChallengeContestEndDateRequiredKey = "toefl_challenge_contest_end_date_is_required_key";
            public const string InvalidEndDate = "invalid_end_date_key";
            public const string ToeflChallengeContestDisplayRequiredKey = "toefl_challenge_contest_display_is_required_key";
            public const string InvalidDisplayValue = "invalid_display_value_key";
            public const string ToeflChallengeContestNotFound = "toefl_challenge_contest_not_found";
            public const string ToeflChallengeContestExistName = "toefl_challenge_contest_name_have_existed";
            public const string ToeflChallengeContestTemplateIdDuplicateKey = "toefl_challenge_contest_template_id_duplicate_key";
            public const string ToeflChallengeContestMocktestNameRequiredKey = "mocktest_name_is_required_key";
            public const string ToeflChallengeContestMocktestIdNotFound = "mocktest_id_not_found";
            public const string DuplicateTemplateId = "duplicate_template_id";
            public const string DuplicateMocktest = "duplicate_mocktest";
            public const string ToeflChallengeContestMocktestNameMaximumLengthKey = "mocktest_name_must_be_less_than_128_characters_key";
            public const string ToeflChallengeContestMocktestDuplicateKey = "toefl_challenge_contest_mocktest_duplicate_key";
            public const string ToeflChallengeContestTemplateIdMustExistInFirstSheet = "toefl_challenge_template_id_must_exist_in_first_sheet";
            public const string ToeflChallengeContestImportFileIsNotCorrectFormat = "toefl_challenge_area_impport_file_is_not_correct_format"; //Mẫu nhập dữ liệu không đúng với quy định",
            public const string ToeflChallengeContestStartDateMustBeLessThanEndDate = "toefl_challenge_contest_start_date_must_be_less_than_end_date";
            public const string ToeflChallengeContestExistWebUsersStartedMockTest = "toefl_challenge_contest_exist_web_users_started_mocktest";
        }

        public static class ToeflChallengeSchoolMessage
        {
            public const string ToeflChallengeSchoolNameRequiredKey = "toefl_challenge_school_name_is_required_key";
            public const string ToeflChallengeSchoolNameMaximumLengthKey = "school_name_must_be_less_than_450_characters_key";
            public const string ToeflChallengeSchoolProvinceRequiredKey = "toefl_challenge_school_province_is_required_key";
            public const string ToeflChallengeSchoolDistrictRequiredKey = "toefl_challenge_school_district_is_required_key";
        }

        public static class ToeflChallengeMessage
        {
            public const string MockTestScoreTypeIsNotValid = "mocktest_score_type_is_not_valid";
        }
        public static class ToeflChallengeResultMessage
        {
            public const string KeyCodeNotFound = "keycode_not_found_key";
            public const string UserNotFound = "user_not_found_key";
            public const string KeyCodeHasNotSubmited = "keycode_has_not_submited";
            public const string MocktestNotFound = "mocktest_not_found_key";
            public const string ContestNotFound = "contest_not_found_key";
        }

        public static class Scoring
        {
            public const string RequestUpdateGroupsRequired = "groups_not_valid";
        }

        public static class Tag
        {
            public const string NameAlreadyExisted = "tag_name_already_existed";
            public const string IdNotFound = "tag_id_not_found";
            public const string TagAlreadyUsed = "tag_already_used";
        }

    }

    public static class CategoryValidate
    {
        public const int CourseCategoryLevel = 3;
        public const string CourseCategoryLevelMustBe4 = "course_category_level_must_be_4";

        public const int ExamToolCategoryLevel = 3;
        public const string ExamToolCategoryLevelMustBe4 = "exam_tool_cateogry_level_must_be_4";

        public const int LearnWithTeacherCategoryLevel = 3;
        public const string LearnWithTeacherCategoryLevelMustBe4 = "learn_with_teacher_category_level_must_be_4";

        public const int GuidingMockTestCategoryLevel = 2;
        public const string GuidingMockTestCategoryLevelMustBe3 = "guiding_mocktest_category_level_must_be_3";

        public const int NewsCategoryLevel = 1;
        public const string NewsCategoryLevelMustBe2 = "news_category_level_must_be_2";
    }
    public static class PhonemeType
    {
        public const string SortVowel = "SortVowel";
        public const string LongVowel = "LongVowel";
        public const string VoicelessConsonant = "VoicelessConsonant";
        public const string VoicedConsonant = "VoicedConsonant";
        public const string NasalConsonant = "NasalConsonant";
    }
}