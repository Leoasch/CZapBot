export class ApiError {
  data = null;
  originalError;
  message;

  constructor (error: any) {
    this.originalError = error
    if (error?.data) {
      this.data = error.data
      if (error.data.message) {
        if (Array.isArray(error.data.message)) {
          this.message = error.data.message[0]
        } else {
          this.message = error.data.message
        }
      }
      return
    }
    this.message = error?.message ?? null
  }
}