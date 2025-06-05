using AutoMapper;
using QuanLyNhaHang.Dtos.Booking;
using QuanLyNhaHang.Dtos.BookingDetail;
using QuanLyNhaHang.Dtos.BookingDetailDto;
using QuanLyNhaHang.Dtos.BookingServiceDetail;
using QuanLyNhaHang.Dtos.BookingServiceDetailDto;
using QuanLyNhaHang.Dtos.Customer;
using QuanLyNhaHang.Dtos.Room;
using QuanLyNhaHang.Dtos.Service;
using QuanLyNhaHang.Dtos.User;
using QuanLyNhaHang.Entities;
using static QuanLyNhaHang.Entities.RoomEntity;

namespace QuanLyNhaHang.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
            CreateMap<UserCreateRequest, UserEntity>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Mã hóa thủ công
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // tránh null ghi đè

            CreateMap<UserRegisterRequest, UserEntity>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(_ => UserEntity.UserRole.Staff)) // default role
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<RoomEntity, RoomDto>().ReverseMap();
            CreateMap<RoomCreateRequestDto, RoomEntity>();

            CreateMap<CustomerEntity, CustomerDto>().ReverseMap();
            CreateMap<CustomerEntity, CustomerCreateDto>().ReverseMap();
            CreateMap<CustomerEntity, CustomerUpdateDto>().ReverseMap();

            CreateMap<BookingEntity, BookingDto>().ReverseMap();
            CreateMap<BookingCreateRequestDto, BookingEntity>();
            CreateMap<BookingUpdateRequest, BookingEntity>();

            CreateMap<BookingDetailEntity, BookingDetailDto>().ReverseMap();
            CreateMap<BookingDetailCreateDto, BookingDetailEntity>();

            CreateMap<ServiceHotelEntity, ServiceHotelDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
            CreateMap<ServiceHotelCreateDto, ServiceHotelEntity>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<ServiceType>(src.Type)));

            CreateMap<BookingServiceDetailEntity,BookingDetailServiceDto>();
            CreateMap<BookingDetailServiceCreateDto, BookingServiceDetailEntity>();
            CreateMap<BookingDetailServiceUpdateDto, BookingServiceDetailEntity>();

            CreateMap<BookingUpdateRequest, BookingEntity>()
                .ForMember(dest => dest.CheckOut, opt => opt.MapFrom(src => src.CheckOutDate));
            
            CreateMap<BookingDetailDto, BookingDetailDto>().ReverseMap();
            CreateMap<BookingDetailCreateDto, BookingDetailEntity>();
            CreateMap<BookingSummaryDto, BookingDetailEntity>();
            //booking
            CreateMap<BookingCreateRequestDto, BookingEntity>();
            CreateMap<BookingEntity, BookingDto>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.CheckInByUser, opt => opt.MapFrom(src => src.CheckInByUser.FullName))
                .ForMember(dest => dest.CheckOutByUser, opt => opt.MapFrom(src => src.CheckOutByUser.FullName));
            
            //bookingdetail
            CreateMap<BookingDetailEntity, BookingDetailDto>()
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer.FullName));
        }
        
    }
}
