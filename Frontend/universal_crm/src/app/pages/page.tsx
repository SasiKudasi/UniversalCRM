import { useState } from "react";
import { useRouter } from "next/router";
import axios from "axios";

export default function LoginPage() {
  const router = useRouter();

  const [userName, setUserName] = useState("");
  const [pwd, setPwd] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      const response = await axios.post(
        "/api/login",
        new URLSearchParams({ userName, pwd }),
        {
          headers: {
            "Content-Type": "application/x-www-form-urlencoded",
          },
        }
      );

      if (response.status === 200) {
        router.push("/admin"); // редирект после успешного входа
      }
    } catch (error: any) {
      if (error.response?.data) {
        setError(error.response.data);
      } else {
        setError("Ошибка сети");
      }
    } finally {
      setLoading(false);
    }
  }

  return (
    <div style={{ maxWidth: 400, margin: "auto", padding: 20 }}>
      <h1>Вход в админку</h1>
      <form onSubmit={handleSubmit}>
        <label>
          Имя пользователя:
          <input
            type="text"
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
            required
            autoFocus
          />
        </label>
        <br />
        <label>
          Пароль:
          <input
            type="password"
            value={pwd}
            onChange={(e) => setPwd(e.target.value)}
            required
          />
        </label>
        <br />
        <button type="submit" disabled={loading}>
          {loading ? "Вход..." : "Войти"}
        </button>
      </form>
      {error && <p style={{ color: "red" }}>{error}</p>}
    </div>
  );
}
