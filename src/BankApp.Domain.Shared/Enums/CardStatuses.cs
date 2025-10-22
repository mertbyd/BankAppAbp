namespace BankApp.Enums;

public enum CardStatuses
{
    Active = 1,   /// Aktif - Kart kullanılabilir
    Passive = 2,  /// Pasif - Geçici olarak kullanılamaz
    Blocked = 3   /// Bloke - Güvenlik nedeniyle engellenmiş
}