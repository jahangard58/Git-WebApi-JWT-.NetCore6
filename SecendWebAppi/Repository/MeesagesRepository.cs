using Microsoft.EntityFrameworkCore;
using SecendWebAppi.DataBaseContextModel;
using SecendWebAppi.Models;
using SecendWebAppi.ViewModels;

namespace SecendWebAppi.Repository
{
    public class MeesagesRepository
    {
        private readonly dbContextEF _ContextEF;

        public MeesagesRepository(dbContextEF contextEF)
        {
            this._ContextEF = contextEF;
        }


        //Use Repository undelin Code Copy in Programs file Class
        //////Step 5
        //builder.Services.AddScoped<MeesagesRepository, MeesagesRepository>();

        public async Task<List<Message>> GetAllMessage()
        {
            return await _ContextEF.Messages.ToListAsync();

        }
        public async Task<Message> GetMessage(int Id)
        {

            var message =await _ContextEF.Messages.FirstOrDefaultAsync(f => f.Id == Id);
           
            return message;


        }
        public async Task<Message> Add(MessageViewModel message)
        {
            var tbl = new Message
            {
                Text = message.Text,
            };
            _ContextEF.Messages.Add(tbl);
            await _ContextEF.SaveChangesAsync();
            return tbl;

        }

        public async Task<bool> Edit(Message message)
        {
            if (message != null)
            {
                if (message.Id != 0)
                {
                    //_ContextEF.Entry(message).State = EntityState.Modified;
                    //_ContextEF.Messages.Update(message);
                    var tblMessage =await  _ContextEF.Messages.FirstOrDefaultAsync(f => f.Id == message.Id);
                    if (tblMessage != null)
                    {
                        tblMessage.Text = message.Text;
                       await _ContextEF.SaveChangesAsync();
                        return true;
                    }
                    
                }

            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var Result =await GetMessage(id);
            if (Result != null)
            {
                _ContextEF.Messages.Remove(Result);
                _ContextEF.SaveChanges();
                return true;


            }
            return false;
        }

    }
}
