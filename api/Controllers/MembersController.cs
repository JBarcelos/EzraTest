using System;
using System.Collections.Generic;

using EzraTest.DB;
using EzraTest.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EzraTest.Controllers
{
    [ApiController]
    [Route("members")]
    public class MembersController : ControllerBase
    {
        private readonly IMembersRepository _membersRepository;

        public MembersController()
        {
            _membersRepository = new MembersRepository("app.db");
        }

        [HttpGet]
        public IEnumerable<Member> GetAllMembers()
        {
            return _membersRepository.GetMembers();
        }

        [HttpGet("{id}")]
        public Member GetMember(Guid id)
        {
            return _membersRepository.GetMember(id);
        }

        [HttpPost("test")]
        public void AddMember(Member member)
        {
            _membersRepository.AddMember(member);
        }

        [HttpPut("{id}")]
        public void UpdateMember(Guid id, Member member)
        {
            _membersRepository.UpdateMember(id, member);
        }

        [HttpDelete("{id}")]
        public void DeleteMember(Guid id)
        {
            _membersRepository.DeleteMember(id);
        }
    }
}
