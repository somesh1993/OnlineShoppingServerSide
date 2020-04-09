using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using OnlineShoppingDataAccess;

namespace OnlineShopping.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OnlineShoppingCardsController : ApiController
    {
        [HttpGet]
        public List<ShoppingCard> Get(string id)
        {
            using(OnlineDatabaseEntities entites = new OnlineDatabaseEntities())
            {
                if (!string.IsNullOrEmpty(id))
                {
                    return entites.ShoppingCards.Where(card => card.CardNumber == id).ToList();
                }
                else
                {
                    return entites.ShoppingCards.ToList();
                }
            }
        }

        [HttpGet]
        public List<ShoppingCard> Search(string id)
        {
            using (OnlineDatabaseEntities entites = new OnlineDatabaseEntities())
            {
                if (string.IsNullOrEmpty(id))
                {
                    return entites.ShoppingCards.ToList();
                }
                return entites.ShoppingCards.Where(item => item.CardNumber.Contains(id)).ToList();
            }
        }

        [HttpPost]
        public string Post([FromBody]ShoppingCard dto)
        {
            using (OnlineDatabaseEntities entites = new OnlineDatabaseEntities())
            {
                if (entites.ShoppingCards.ToList().Find(existingCard => existingCard.CardNumber == (dto.CardNumber)) == null)
                {
                    entites.ShoppingCards.Add(dto);
                    entites.SaveChanges();
                    return "";
                }
                else
                {
                    return "Card Number already exists";
                }
            }
        }

        [HttpPut]
        public string Put([FromBody]ShoppingCard dto)
        {
            using (OnlineDatabaseEntities entites = new OnlineDatabaseEntities())
            {
                var existingCard = entites.ShoppingCards.ToList().Find(oldCard => oldCard.CardNumber.Equals(dto.CardNumber));
                if (existingCard != null)
                {
                    existingCard.BankName = dto.BankName;
                    existingCard.FirstName = dto.FirstName;
                    existingCard.LastName = dto.LastName;
                    existingCard.ValidDate = dto.ValidDate;
                    existingCard.Price = dto.Price;
                    entites.SaveChanges();
                    return "";
                }
                else
                {
                    return "Card cannot be modified";
                }
            }
        }

        [HttpDelete]
        public void Delete(string id)
        {
            using (OnlineDatabaseEntities entites = new OnlineDatabaseEntities())
            {
                var existingCard = entites.ShoppingCards.ToList().Find(oldCard => oldCard.CardNumber.Equals(id));
                if (existingCard != null)
                {
                    entites.ShoppingCards.Remove(existingCard);
                    entites.SaveChanges();
                }
            }
        }
    }
}