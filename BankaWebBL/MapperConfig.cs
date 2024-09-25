using AutoMapper;
using BankaWebEL.DTOs;
using BankaWebEL.Entities;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Account, AccountDTO>()
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType.TypeName))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.CurrencyName))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));
       
        CreateMap<AccountDTO, Account>()
            .ForMember(dest => dest.AccountType, opt => opt.Ignore())
            .ForMember(dest => dest.Currency, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
        CreateMap<AccountType, AccountTypeDTO>().ReverseMap();      
        CreateMap<Currency, CurrencyDTO>().ReverseMap();
        CreateMap<Customer, CustomerDTO>().ReverseMap();
        CreateMap<ExchangeRate, ExchangeRateDTO>()
            .ForMember(dest => dest.FromCurrency, opt => opt.MapFrom(src => src.FromCurrency.CurrencyName))
            .ForMember(dest => dest.ToCurrency, opt => opt.MapFrom(src => src.ToCurrency.CurrencyName));
        CreateMap<Transaction, TransactionDTO>()
            .ForMember(dest => dest.AccountIBAN, opt => opt.MapFrom(src => src.Account.IBAN))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.CurrencyName));
        CreateMap<Transfer, TransferDTO>()
            .ForMember(dest => dest.SenderAccountIBAN, opt => opt.MapFrom(src => src.SenderAccount.IBAN))
            .ForMember(dest => dest.ReceiverAccountIBAN, opt => opt.MapFrom(src => src.ReceiverAccount.IBAN));

    }
}
