#import "gameSDK.h"
#include <sys/socket.h>
#include <netdb.h>
#include <arpa/inet.h>
#include <err.h>

extern "C"
{
	#define MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

	const char* getIPv6(const char *mHost)
	{
		if( nil == mHost )
			return NULL;
		const char *newChar = "No";
		const char *cause = NULL;
		struct addrinfo* res0;
		struct addrinfo hints;
		struct addrinfo* res;
		int n, s;
	
		memset(&hints, 0, sizeof(hints));
	
		hints.ai_flags = AI_DEFAULT;
		hints.ai_family = PF_UNSPEC;
		hints.ai_socktype = SOCK_STREAM;
	
		if((n=getaddrinfo(mHost, "http", &hints, &res0))!=0)
		{
			printf("getaddrinfo error: %s\n",gai_strerror(n));
			return NULL;
		}
	
		struct sockaddr_in6* addr6;
		struct sockaddr_in* addr;
		NSString * NewStr = NULL;
		char ipbuf[32];
		s = -1;
		for(res = res0; res; res = res->ai_next)
		{
			if (res->ai_family == AF_INET6)
			{
				addr6 =( struct sockaddr_in6*)res->ai_addr;
				newChar = inet_ntop(AF_INET6, &addr6->sin6_addr, ipbuf, sizeof(ipbuf));
				NSString * TempA = [[NSString alloc] initWithCString:(const char*)newChar encoding:NSASCIIStringEncoding];
				NSString * TempB = [NSString stringWithUTF8String:"&&ipv6"];
			
				NewStr = [TempA stringByAppendingString: TempB];
				printf("%s\n", newChar);
			}
			else
			{
				addr =( struct sockaddr_in*)res->ai_addr;
				newChar = inet_ntop(AF_INET, &addr->sin_addr, ipbuf, sizeof(ipbuf));
				NSString * TempA = [[NSString alloc] initWithCString:(const char*)newChar encoding:NSASCIIStringEncoding];
				NSString * TempB = [NSString stringWithUTF8String:"&&ipv4"];
			
				NewStr = [TempA stringByAppendingString: TempB];			
				printf("%s\n", newChar);
			}
			break;
		}
	
	
		freeaddrinfo(res0);
	
		printf("getaddrinfo OK");
	
		NSString * mIPaddr = NewStr;
		return MakeStringCopy(mIPaddr);
	}
}

/*
//获取磁盘大小
-(BOOL)IsIOSDiskLowMemory
{
    NSDictionary *systemAttributes = [[NSFileManager defaultManager] fileSystemAttributesAtPath:NSHomeDirectory()];
    long freeDiskMemory = [[systemAttributes objectForKey:@"NSFileSystemFreeSize"] longValue];
    return abs(freeDiskMemory) < 1024 * 1024 * 3;
}

//ram低内存
bool isLowRamMemory(){
	vm_statistics_data_t vmStats;
	mach_msg_type_number_t infoCount = HOST_VM_INFO_COUNT;
	kern_return_t kernReturn = host_statistics(mach_host_self(),
											   HOST_VM_INFO,
											   (host_info_t)&vmStats,
											   &infoCount);
	
	if (kernReturn != KERN_SUCCESS) {
		return NSNotFound;
	}
	
	if (((vm_page_size *vmStats.free_count) / 1024.0) / 1024.0 > 130) {
		return false;
	};
	return true;
}


//获取网络状态（0：无网络 1：wifi 其他：4G）
char * GetIOSNetInfo(){
	NSString *netconnType = @"";
	// 状态栏是由当前控制器控制的，首先获取当前app。
	UIApplication *app = [UIApplication sharedApplication];
	
	// 遍历状态栏上的前景视图
	NSArray *children = [[[app valueForKeyPath:@"statusBar"] valueForKeyPath:@"foregroundView"] subviews];
	
	int type = 0;
	for (id child in children) {
		if ([child isKindOfClass:NSClassFromString(@"UIStatusBarDataNetworkItemView")]) {
			type = [[child valueForKeyPath:@"dataNetworkType"] intValue];
		}
	}
	switch (type) {
		case 0:
			netconnType = @"0";
			break;
		case 1:
			netconnType = @"2";
			break;
		case 2:
			netconnType = @"2";
			break;
		case 3:
			netconnType = @"2";
			break;
		case 5:
			netconnType = @"1";
			break;
		default:
			break;
	}
	// type数字对应的网络状态依次是：0：无网络；1：2G网络；2：3G网络；3：4G网络；5：WIFI信号
	return MakeStringCopy(netconnType);
}

//设置屏幕亮度
void setScreenBrightness(float point){
	[[UIScreen mainScreen] setBrightness:point];
}

//获取屏幕亮度
float getScreenBrightness(){
	return [[UIScreen mainScreen] brightness];
}

@interface BatteryInfo()
@property(nonatomic,assign)BOOL isChange;
@property(nonatomic,assign)int batteryLevel;
@end

@implementation BatteryInfo

+(instancetype)shareInstance{
    static BatteryInfo * info = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        info = [[BatteryInfo alloc] init];
        
        [[NSNotificationCenter defaultCenter] addObserver:info selector:@selector(didChangeBatteryLevel:) name:UIDeviceBatteryLevelDidChangeNotification object:nil];
    });
    return info;
}

// 当前电量
-(int)GetCurrentBattery{
    UIDevice * device = [UIDevice currentDevice];
    device.batteryMonitoringEnabled = YES;
    if (_isChange) {
        return self.batteryLevel;
    }else{
        return device.batteryLevel*100;
    }
}

- (void)didChangeBatteryLevel:(id)sender{
    _isChange = YES;
    //电池电量发生改变时调用
    UIDevice *myDevice = [UIDevice currentDevice];
    [myDevice setBatteryMonitoringEnabled:YES];
    float batteryLevel = [myDevice batteryLevel];
    self.batteryLevel = batteryLevel*100;
}
@end*/