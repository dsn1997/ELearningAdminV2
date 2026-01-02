using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IIG.Core.Enums;

public static partial class WebConstants
{
    public const string RedisUserAccessCourseKey = "{0}_{1}"; // 0-userId, 1-courseId

    public static class RateLimiterPolicy
    {
        public const string FixedRateLimiting = "FixedRateLimiting";
    }

    public static class ShowingArea
    {
        public const string MenuTop = "menu_top";
        public const string MenuHome = "menu_home";
        public const string MenuBottom = "menu_bottom";
    }

    public static partial class ValidationMessages
    {
        public static class RateLimiterPolicyMessage
        {
            public const string TooManyRequestsWithTotalMinutes = "Too many requests. Please try again after {0} minute(s).";
            public const string TooManyRequests = "Too many requests. Please try again later.";
        }

        public static class InvoiceMessage
        {
            public const string NameIsRequired = "name_is_required";
            public const string NameInvalidFormat = "name_must_be_less_than_128_characters";
            public const string TaxCodeInvalidFormat = "tax_code_must_be_less_than_50_characters";
            public const string AddressInvalidFormat = "address_must_be_less_than_500_characters";
            public const string EmailLengthRequired = "email_must_be_less_than_255_characters";
            public const string EmailInvalidFormat = "email_invalid_format";
            public const string DescriptionInvalidFormat = "description_must_be_less_than_500_characters";
            public const string InvoiceCannotUpdate = "invoice_cannot_be_update";
            public const string InvoiceCannotAccessOrder = "invoice_cannot_access_order";
            public const string InvoiceAlreadyExistsForOrder = "invoice_already_exists_for_order";
        }

        public static class MyCourseMessage
        {
            public const string UserCannotAccessCourseKey = "user_cannot_access_course_key";
            public const string CourseNotFoundKey = "course_not_found_key";
            public const string CourseExpiredFoundKey = "course_expired_key";
            public const string CourseIdIsRequiredKey = "course_id_is_required_key";
            public const string StartDateIsRequiredKey = "start_date_is_required_key";
            public const string DaysPerWeekIsRequiredKey = "days_per_week_is_required_key";
            public const string DaysPerWeekMustBeBetween1And7Key = "days_per_week_must_be_between_1_and_7_key";
            public const string MyCourseYouHaveNotPurchaseThisCourseKey = "review_you_have_not_purchase_this_course_key";
            public const string UserIdRequiredKey = "user_id_is_required_key";
            public const string QuestionnaireTypeRequired = "questionnaire_type_is_required_key";
            public const string QuestionnaireTypeNotValidFormat = "questionnaire_type_is_invalid_format_key";
        }

        public static class MyDictionaryMessage
        {
            public const string StepIdRequiredKey = "step_id_is_required_key";
            public const string QuestionnaireIdRequiredKey = "questionnaire_id_is_required_key";
            public const string QuestionIdRequiredKey = "question_id_is_required_key";
            public const string WordExistedInDictionary = "word_existed_in_dictionary";
        }

        public static class WebQuestionnaireMessage
        {
            public const string StepQuestionnaireNotFoundKey = "step_questionnaire_not_found_key";
            public const string QuestionnaireNotFoundKey = "questionnaire_not_found_key";
            public const string QuestionNotFoundKey = "question_not_found_key";
        }

        public static class UnitMessage
        {
            public const string UnitNotFoundKey = "unit_not_found_key";
        }

        public static class UnitTestMessage
        {
            public const string UnitTestNotFoundKey = "unit_test_result_not_found_key";
        }

        public static class AssessmentMessage
        {
            public const string QuestionnaireTypeIsNotValid = "questionnaire_type_is_not_valid";
            public const string StepIdIsRequiredKey = "step_id_is_required";
            public const string QuestionnaireIdIsRequiredKey = "quesionnaire_id_is_required";
            public const string AnswerIdIsRequiredKey = "answer_id_is_required";
            public const string QuestionIdIsRequiredKey = "question_id_is_required";
            public const string AnswerTextRequiredKey = "answer_text_is_required";

            public const string FileIsRequiredKey = "file_is_required";
            public const string FileCannotBeEmpty = "file_cannot_be_empty";

            public const string PublicFilePathNotFound = "public_file_path_not_found";
        }

        public static class PaymentMessage
        {
            public const string VnpDataQueryStringIsNotNullOrEmpty = "data_query_string_is_not_null_or_empty";
            public const string OrderCodeIsNotNullOrEmpty = "order_is_not_null_or_empty";
            public const string VnpDataQueryStringIsNotValid = "vnp_data_query_string_is_not_valid";
            public const string InvalidSignature = "invalid_signature";
        }

        public static class DiscountMessage
        {
            public const string DiscountCodeNotFound = "discount_code_not_found";
            public const string DiscountScopeApplyIsNotValid = "discount_scope_apply_is_not_valid";
            public const string DiscountTypeUseIsNotValid = "discount_type_use_is_not_valid";
            public const string DiscountCodeNotAvailable = "discount_code_not_available";
            public const string DiscountStatusNotActive = "discount_status_is_not_active";
            public const string DiscountNotValid = "discount_not_valid";
        }

        public static class ShoppingCartMessage
        {
            public const string OrderNotFound = "order_not_found";
            public const string OrderStatusNotValid = "order_status_not_valid";
            public const string TransactionPaymentNotFound = "transaction_payment_of_order_cannot_found";
        }

        public static class OrderMessage
        {
            public const string OrderIdNotFound = "order_id_not_found";
        }

        public static class ReviewMessage
        {
            public const string CourseNotFoundKey = "review_course_not_found_key";
            public const string ReviewContentIsRequiredKey = "review_content_is_required_key";
            public const string ReviewRateIsRequiredKey = "review_rate_is_required_key";
            public const string ReviewRateMustBeGreaterThan0AndLessThanOrEqualTo5Key = "review_rate_must_be_greater_than_0_and_less_than_or_equal_to_5_key";
            public const string ReviewContentMinimumLengthKey = "review_content_must_be_greater_than_or_equal_20_characters_key";
            public const string ReviewContentMaximumLengthKey = "review_content_must_be_less_than_or_equal_500_characters_key";
            public const string ReviewYouCannotCommentOnThisCourseKey = "review_you_cannot_comment_on_this_course_key";
            public const string ReviewYouCannotModifyThisReviewKey = "review_you_cannot_modify_this_review_key";
        }

        public static class CoursePriceMessage
        {
            public const string CoursePriceNotFoundKey = "course_price_not_found_key";
        }

        public static class MockTestMessage
        {
            public const string MockTestNotFound = "mock_test_not_found";
            public const string KeyCodeNotFoundKey = "key_code_not_found_key";
            public const string KeyCodeIsNotEmptyKey = "key_code_is_not_empty_key";
            public const string MocktestObjectIdIsNotEmptyKey = "mocktest_object_id_is_not_empty_key";
            public const string MocktestTypeIdIsNotEmptyKey = "mocktest_type_id_is_not_empty_key";
            public const string InvalidKeyCodeKey = "invalid_key_code_key";
            public const string MockTestCannotDumpWithStatusNotActive = "mock_test_cannot_dump_with_status_not_active";
            public const string UserNeedToFinishCurrentSectionMocktest = "user_need_to_finish_current_section_mocktest";
            public const string MockTestTimeRemainingHasRunOut = "mock_test_time_remaining_has_run_out";
            public const string InvalidKeyCodeStartDateKey = "invalid_key_code_start_date_key";
        }

        public static class MockTestKeyCodeMessage
        {
            public const string KeyCodeHaveNotSubmittedYet = "key_code_{0}_have_not_submitted_yet";
            public const string KeyCodeAlreadySubmitted = "key_code_{0}_already_submitted";
            public const string MockTestScoreTypeIsNotValid = "mocktest_score_type_is_not_valid";
            public const string MockTestNotValid = "mocktest_not_valid";
            public const string KeyCodeInUseKey = "keycode_in_use";
            public const string BrowserIsNotCorrectKey = "browser_is_not_correct_key";
            public const string TimeRemainingIsNotCorrectKey = "time_remaining_is_not_correct_key";
            public const string MockTestKeyCodeMockTestSectionIdIsRequiredKey = "mock_test_key_code_mock_test_section_id_is_required_key";
            public const string MockTestKeyCodeMockTestPartIdIsRequiredKey = "mock_test_key_code_mock_test_part_id_is_required_key";
            public const string MockTestKeyCodeQuestionnaireIdIsRequiredKey = "mock_test_key_code_questionnaire_id_is_required";
            public const string MockTestKeyCodeQuestionIdIsRequiredKey = "mock_test_key_code_question_id_is_required";
        }

        public static class CourseTestMessage
        {
            public const string CourseTestNotFound = "course_test_not_found";
            public const string CannotAccessCourseTest = "cannot_access_course_test";
            public const string CourseTestHasNotSubmittedYet = "course_test_has_not_submitted_yet";
        }
        public static class CourseTestResultMessage
        {
            public const string CourseTestResultNotFound = "course_test_result_not_found";
           
        }

        public static class LiveCustomerSupportMessage
        {
            public const string CourseIdNotFound = "course_id_not_found";
            public const string CoursePriceIdNotFound = "course_price_id_not_found";
            public const string FullNameMaximumLengthKey = "full_name_must_be_less_than_or_equal_255_characters_key";
            public const string PhoneNumberMaximumLengthKey = "phone_number_must_be_less_than_or_equal_32_characters_key";
            public const string EmailMaximumLengthKey = "email_must_be_less_than_or_equal_255_characters_key";
        }

        public static class LiveClassDetailMessage
        {
            public const string LiveClassDetailNotFoundKey = "live_class_detail_not_found_key";
            public const string ClassinCourseIsNotCreatedKey = "classin_course_is_not_created_key";
            public const string UserCannotAccessLiveClassKey = "user_cannot_access_live_class_key";
            public const string LiveLessonNotFoundKey = "live_lesson_not_found_key";
            public const string ClassinLessonIsNotCreatedKey = "classin_lesson_is_not_created_key";
            public const string LiveLessonInvalidKey = "live_lesson_invalid_key";
            public const string ClassinUserIsNotRegisteredKey = "classin_user_is_not_registered_key";
        }

        public static class LiveClassTestMessage
        {
            public const string LiveClassTestNotFoundKey = "live_class_test_not_found_key";
            public const string LiveClassTestHasExpiredKey = "live_class_test_has_expired_key";
        }

        public static class LiveLessonMissionMessage
        {
            public const string LiveLessonMissionNotFoundKey = "live_lesson_mission_not_found_key";
            public const string LiveLessonMissionHasExpiredKey = "live_lesson_mission_has_expired_key";
            public const string LiveLessonMissionCanNotRedoKey = "live_lesson_mission_can_not_redo_key";
            public const string LiveLessonMissionHasNotBeenTakenKey = "live_lesson_mission_has_not_been_taken_key";
        }
        public static class LiveClassLessonMessage
        {
            public const int NumberOfMinuteCanEnter = 10;
            public static string StudentCanONlyEnterClassroom10MinutesBeforeLessonStartTime = "student_can_only_enter_classroom_10_minutes_before_lesson_start_time";
            public static string LiveLessonDetailHasEnded = "live_lesson_detail_has_ended";
        }

        public static class NotificationMessage
        {
            public const string NotificationDatetimeTriggerIsRequired = "notification_datetime_trigger_is_required";
            public const string NotificationDatetimeTriggerMustBeGreaterThanOrEqualToCurrentTime = "notification_datetime_trigger_must_be_greater_than_or_equal_to_current_time";
            public const string NotificationTitleIsRequired = "notification_title_is_required";
            public const string NotificationTitleInvalidFormat = "notification_title_must_be_less_than_128";
        }

        public static class Scoring
        {
            public const string TypeIsNotDefined = "type_not_defined";
            public const string NotFoundScoring = "scoring_not_found";
            public const string NotFoundScoringByAI = "scoring_by_ai_not_found";
        }
    }
    public static class ToeflChallenge
    {
        public const string OtherSchool = "Trường học khác";
    }

    public static class VngStorage
    {
        public const string ErrorVngCloud = "error_vng_cloud";
    }
}