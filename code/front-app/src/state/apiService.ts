import axios from "axios";

const API_URL: string = "https://pokemon-api.ricardomelo.dev";
export const api = axios.create({
  baseURL: API_URL,
});