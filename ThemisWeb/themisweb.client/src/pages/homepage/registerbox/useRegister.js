import useFetch from "../../../shared/hooks/useFetch"

function useRegister(Password, Email) {

    return useFetch("/register", "POST", {email: Email, password: Password})
}
export default useRegister