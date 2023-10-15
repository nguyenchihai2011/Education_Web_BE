using AutoMapper;
using EducationAPI.Data;
using EducationAPI.DTOs;
using EducationAPI.Entities;
using EducationAPI.Models;

namespace EducationAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
            CreateMap<LectureEntity, LectureDTO>().ReverseMap();
            CreateMap<AdminEntity, AdminDTO>().ReverseMap();
            CreateMap<StudentEntity, StudentDTO>().ReverseMap();
            CreateMap<CourseEntity, CourseDTO>().ReverseMap();
            CreateMap<SectionEntity, SectionDTO>().ReverseMap();
            CreateMap<LessonEntity, LessonDTO>().ReverseMap();
            CreateMap<QuizEntity, QuizDTO>().ReverseMap();
            CreateMap<AnswerEntity, AnswerDTO>().ReverseMap();
            CreateMap<PromotionEntity, PromotionDTO>().ReverseMap();
            CreateMap<NotifycationEntity, NotifycationDTO>().ReverseMap();
            CreateMap<CommentEntity, CommentDTO>().ReverseMap();
            CreateMap<RatingEntity, RatingDTO>().ReverseMap();
            CreateMap<CartEntity, CartDTO>().ReverseMap();
        }
    }
}
