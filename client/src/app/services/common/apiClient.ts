import { TimeloggerApiClient } from "../generated";

const client = new TimeloggerApiClient({BASE: "http://localhost:3001/api/v1"})

export default client.default;
