import axios from "axios";
import config from "config";

const routePrefix = config.apiUrl + "api/tag/";
const returnData = (r) => r.data;

export default class NoteService {
  static create(model) {
    return axios.post(`${routePrefix}create`, model).then(returnData);
  }

  static get(id) {
    return axios.get(routePrefix + id).then(returnData);
  }

  static update(model) {
    return axios.post(`${routePrefix}update`, model).then(returnData);
  }

  static delete(id) {
    return axios.delete(`${routePrefix}delete/${id}`).then(returnData);
  }

  static getAll() {
    return axios.get(routePrefix + "list").then(returnData);
  }

  static search(model) {
    return axios.post(routePrefix + "search", model).then(returnData);
  }
}
