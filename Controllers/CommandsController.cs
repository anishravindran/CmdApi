using System.Collections.Generic;
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
    }
}
