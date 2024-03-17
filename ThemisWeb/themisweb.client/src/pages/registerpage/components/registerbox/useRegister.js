import useFetch from "../../../../shared/hooks/useFetch"

function useRegister(Username, Password, Email) {

    return useFetch("/register", "POST", { "email": Email, "password": Password, "username": Username })
}
export default useRegister