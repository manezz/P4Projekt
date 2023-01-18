export function randomString(length: number): string{
    var randomChars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
    var result = '';
    for (var i = 0; i < length; i++){
        result += randomChars.charAt(Math.floor(Math.random() * randomChars.length));
    }
    return result;
}