using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.Mappers
{
    public class Mapper : Profile
    {
      public Mapper()
        {
            CreateMap<Database.Grad, Model.Grad>().ReverseMap();
            CreateMap<Database.Drzava, Model.Drzava>().ReverseMap();
            CreateMap<Database.Biblioteka, Model.Biblioteka>().ReverseMap();
            CreateMap<Database.Izdavac, Model.Izdavac>().ReverseMap();
            CreateMap<Database.Pisac, Model.Pisac>().ReverseMap();
            CreateMap<Database.Pisac, Model.Requests.PisacInsertRequest>().ReverseMap();
            CreateMap<Database.Biblioteka, Model.Requests.BibliotekaInsertRequest>().ReverseMap();
            CreateMap<Database.Izdavac, Model.Requests.IzdavacInsertRequest>().ReverseMap();
            CreateMap<Database.Tip, Model.Tip>().ReverseMap();
            CreateMap<Database.Grad, Model.Requests.GradUpsertRequest>().ReverseMap();
            CreateMap<Database.Korisnik, Model.Korisnik>().ReverseMap();
        }

    }
}
