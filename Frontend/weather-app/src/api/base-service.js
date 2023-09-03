import axios from "axios";


const getOptions = (customOptions) => {
  const options = {
    headers: { contentType: "application/json" },
  };

  return options;
};

export default class BaseService {
  constructor(baseController, defaultVersion) {
    this.baseApiUrl = "http://localhost:18823";
    this.baseController = baseController;
    this.defaultVersion = defaultVersion ?? "v1";
  }

  _Get(url, config) {
    const options = getOptions(config);
    return axios.get(url, options);
  }

  _GetCancelToken() {
    return axios.CancelToken.source();
  }
}
