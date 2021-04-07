class RaceService {
    @Log()
    getRaces() {
        // call API
    }
}

/*
• target: the method targeted by our decorator
• name: the name of the targeted method
• descriptor: a descriptor of the targeted method (is the method enumerable, writable, etc.)
*/
const Log = () => {
    return (target: any, name: string, descriptor: any) => {
        console.log(`Method ${name} called`);
        return descriptor;
    };
};