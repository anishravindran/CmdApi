using System.Collections.Generic;
using System.Threading.Tasks;
using CmdApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace CmdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private CommandContext _context;

        public CommandsController(CommandContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            return _context.CommandItems;
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItem(int id)
        {
            var command = _context.CommandItems.Find(id);
            if (command == null)
            {
                return NotFound();
            }
            return command;
        }

        [HttpPost]
        public async Task<ActionResult<Command>> CreateCommand(Command command)
        {
            if (command != null)
            {
                _context.CommandItems.Add(command);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetCommandItem", new Command { Id = command.Id }, command);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, Command command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            _context.Entry(command).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var command = _context.CommandItems.Find(id);
            if (command == null)
            {
                return NotFound();
            }
            _context.Entry(command).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
