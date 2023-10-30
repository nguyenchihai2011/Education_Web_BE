using AutoMapper;
using EducationAPI.Context;
using EducationAPI.DTOs;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EducationAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext context;
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;
        public ChatHub(ApplicationDbContext context, ICommentRepository commentRepository, IMapper mapper)
        {
            this.context = context;
            this.commentRepository = commentRepository;
            this.mapper = mapper;
        }
        public async Task SendComment(CommentDTO commentData)
        {
            await commentRepository.Add(mapper.Map<Comment>(commentData));
            await Clients.All.SendAsync("ReceiveComment");
        }
    }
}
