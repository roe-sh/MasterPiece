using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[]? PasswordHash { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public string? Role { get; set; }

    public int? LoyaltyPoints { get; set; }

    public bool? IsAdmin { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<Bmi> Bmis { get; set; } = new List<Bmi>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<ContactU> ContactUs { get; set; } = new List<ContactU>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Gtpal> Gtpals { get; set; } = new List<Gtpal>();

    public virtual ICollection<HealthTip> HealthTips { get; set; } = new List<HealthTip>();

    public virtual ICollection<Kid> Kids { get; set; } = new List<Kid>();

    public virtual ICollection<LoyaltyPoint> LoyaltyPointsNavigation { get; set; } = new List<LoyaltyPoint>();

    public virtual ICollection<MilkFormula> MilkFormulas { get; set; } = new List<MilkFormula>();

    public virtual ICollection<News> News { get; set; } = new List<News>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
