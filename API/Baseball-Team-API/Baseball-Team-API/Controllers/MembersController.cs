using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Baseball_Team_API.Models;
using System.Text;
using System.Security.Cryptography;

namespace Baseball_Team_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly TestDBContext _context;

        public MembersController(TestDBContext context)
        {
            _context = context;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            return await _context.Member.ToListAsync();
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(string id)
        {
            var member = await _context.Member.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(string id, Member member)
        {
            if (id != member.Username)
            {
                return BadRequest();
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Members
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(SignUp signUp)
        {
            var member = GenMemberSaltandHash(signUp);

            _context.Member.Add(member);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MemberExists(member.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMember", new { id = member.Username }, member);
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Member>> DeleteMember(string id)
        {
            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Member.Remove(member);
            await _context.SaveChangesAsync();

            return member;
        }

        private bool MemberExists(string id)
        {
            return _context.Member.Any(e => e.Username == id);
        }

        //returns a member object from a login with a generated salt and a hashed password + unauthed authlevel
        private Member GenMemberSaltandHash(SignUp signUp)
        {
            //create salt with a max length of 32
            int saltMaxLength = 32;

            byte[] salt = new byte[saltMaxLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetBytes(salt);
            }

            var saltString = Convert.ToBase64String(salt);

            //hash password
            var passwordHash = SHA512.Create();
            var password = passwordHash.ComputeHash(Encoding.UTF8.GetBytes(signUp.Password + saltString + Environment.GetEnvironmentVariable("pepper")));

            //create and return member
            var member = new Member();
            member.Username = signUp.Username;
            member.Firstname = signUp.Firstname;
            member.Surname = signUp.Surname;
            member.Password = password;
            member.Salt = saltString;
            member.AuthLevel = "Player";

            //return our new member
            return member;
        }
    }
}
