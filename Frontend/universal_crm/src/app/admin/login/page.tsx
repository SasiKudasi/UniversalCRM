"use client";
import { api } from "@/lib/api";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { Button, Form } from "react-bootstrap";

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
      const response = await api.post(
        "admin/login",
        new URLSearchParams({ userName, pwd }),
        {
          headers: {
            "Content-Type": "application/x-www-form-urlencoded",
          },
        }
      );

      if (response.status === 200) {
        router.push("/admin/dashboard/");
      }
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
    } catch (error: any) {
      setError(error.message);
    } finally {
      setLoading(false);
    }
  }

  return (
    <div style={{ maxWidth: 400, margin: "auto", padding: 20 }}>
      <h1>Вход в админку</h1>
      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>Login</Form.Label>
          <Form.Control
            type="text"
            placeholder="Enter login"
            value={userName}
            onChange={(e) => setUserName(e.target.value)} />
        </Form.Group>

        <Form.Group className="mb-3" controlId="formBasicPassword">
          <Form.Label>Password</Form.Label>
          <Form.Control
            type="password"
            placeholder="Password"
            value={pwd}
            onChange={(e) => setPwd(e.target.value)}
            required />
        </Form.Group>
        <Button variant="primary" type="submit">
          {loading ? "Вход..." : "Войти"}
        </Button>
        {error && <p style={{ color: "red" }}>{error}</p>}
      </Form>
    </div>
  );
}