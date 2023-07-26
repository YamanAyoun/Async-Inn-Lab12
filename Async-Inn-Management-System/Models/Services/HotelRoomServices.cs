﻿using Async_Inn_Management_System.Data;
using Async_Inn_Management_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn_Management_System.Models.Services
{
    public class HotelRoomServices : IHotelRoom
    {
        private AsyncInnDbContext _context;

        public HotelRoomServices(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<HotelRoom> Create(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            var hotelrooms = await _context.HotelRooms
                .Include(x => x.Room)
                .ThenInclude(x => x.HotelRooms)
                .ToListAsync();
            return hotelrooms;
        }

        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomId)
        {
            return await _context.HotelRooms
                .Include(x => x.Hotel)
                .ThenInclude(x => x.HotelRooms)
                .FirstOrDefaultAsync(x => x.HotelID == hotelId && x.RoomID == roomId);
        }

        public async Task<HotelRoom> UpdateHotelRoom(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await GetHotelRoom(hotelId, roomNumber);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<HotelRoom> AddRoomToHotel(int hotelId, HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task DeleteRoomFromHotel(int roomId, int hotelId)
        {
            var hotelRoom = await _context.HotelRooms
                .Where(h => h.HotelID == hotelId && h.RoomID == roomId)
            .FirstAsync();

            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}