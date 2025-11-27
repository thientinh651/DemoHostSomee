var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
builder.Services.AddDistributedMemoryCache();
// Đăng ký dịch vụ Session
builder.Services.AddSession(cfg => {
	// Đặt tên Session - tên này sử dụng ở Browser (Cookie)
	cfg.Cookie.Name = "QLBH";
	// Thời gian tồn tại của Session
	cfg.IdleTimeout = new TimeSpan(0, 60, 0);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	//pattern: "{controller=NhanVien}/{action=Index}/{id?}");
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
