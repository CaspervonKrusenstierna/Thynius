import useFetch from "../../../../shared/hooks/useFetch"

 function useLogin(Email, Password) {

     return useFetch("/login", "POST", { "email": Email, "password": Password})
}
export default useLogin