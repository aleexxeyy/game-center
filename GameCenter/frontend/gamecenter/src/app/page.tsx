import { Card, CardContent } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";

export default function HomePage() {
  return (
    <div className="flex items-center justify-center min-h-screen bg-gradient-to-br from-blue-500 to-purple-600 p-4">
      <Card className="w-full max-w-md p-6 bg-white shadow-xl rounded-xl">
        <CardContent>
          <h1 className="text-2xl font-bold text-center mb-6">Регистрация</h1>
          <form className="flex flex-col space-y-4">
            <div>
              <Label htmlFor="email">Email</Label>
              <Input id="email" type="email" placeholder="Введите email" />
            </div>
            <div>
              <Label htmlFor="password">Пароль</Label>
              <Input id="password" type="password" placeholder="Введите пароль" />
            </div>
            <div>
              <Label htmlFor="confirm-password">Подтвердите пароль</Label>
              <Input id="confirm-password" type="password" placeholder="Повторите пароль" />
            </div>
            <Button type="submit" className="w-full">
              Зарегистрироваться
            </Button>
          </form>
        </CardContent>
      </Card>
    </div>
  );
}
